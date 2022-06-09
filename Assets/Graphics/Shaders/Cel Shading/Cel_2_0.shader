/**
*   Cel shader с настройкой количества слоев и отбрасыванием тени
**/

Shader "Test/CelShader_2_0"
{
    Properties
    {
        [HDR]_Color("Albedo", Color) = (1,1,1,1)
        _MainTex ("Texture", 2D) = "white" {}

        // CelShading
        _Layers ("Number of Layers", Range(0.0, 50.0)) = 3
        _LayerThickness ("Thickness of the gradient layer", Range(0.0, 1.0)) = 1
        _ShadowStrenght ("ShadowStreng", Range(0.0, 1.0)) = 0
        _MaxShadowIntencity ("Max shadow intencity", Range(0.0, 1.0)) = 0

        // Параметры теней
        [Header(Stencil)]
        _Stencil ("Stencil ID [0;255]", Float) = 0
        _ReadMask ("ReadMask [0;255]", Int) = 255
        _WriteMask ("WriteMask [0;255]", Int) = 255
        [Enum(UnityEngine.Rendering.CompareFunction)] _StencilComp ("Stencil Comparison", Int) = 0
        [Enum(UnityEngine.Rendering.StencilOp)] _StencilOp ("Stencil Operation", Int) = 0
        [Enum(UnityEngine.Rendering.StencilOp)] _StencilFail ("Stencil Fail", Int) = 0
        [Enum(UnityEngine.Rendering.StencilOp)] _StencilZFail ("Stencil ZFail", Int) = 0
        
        [Header(Rendering)]
        _Offset("Offset", float) = 0
        [Enum(UnityEngine.Rendering.CullMode)] _Culling ("Cull Mode", Int) = 2
        [Enum(Off,0,On,1)] _ZWrite("ZWrite", Int) = 1
        [Enum(UnityEngine.Rendering.CompareFunction)] _ZTest ("ZTest", Int) = 4
        [Enum(None,0,Alpha,1,Red,8,Green,4,Blue,2,RGB,14,RGBA,15)] _ColorMask("Color Mask", Int) = 15
    }

    CGINCLUDE
    #include "UnityCG.cginc"
    #include "AutoLight.cginc"

    half4 _Color;
    sampler2D _MainTex;
    float4 _MainTex_ST;

    // Параметры шейдинга
    float _Layers;
    float _MaxShadowIntencity;
    float _ShadowStrenght;
    float _LayerThickness;

    struct appdata
    {
        float4 vertex : POSITION;
        float2 uv : TEXCOORD0;
        float3 normal : NORMAL;
    };
    
    struct v2f
    {
        float2 uv : TEXCOORD0;
        float4 vertex : SV_POSITION;
        float3 worldNormal : NORMAL;
    };

    // Рендеринг цел шейдинга
    float Toon(float3 normal, float3 lightDirection) {

        float dotRes =  max(0.0, dot(normalize(normal), normalize(lightDirection)));
        
        dotRes += (1 - dotRes) * _ShadowStrenght;
        dotRes = floor(dotRes/_LayerThickness) / _Layers;
        dotRes += (1 - dotRes) * _MaxShadowIntencity;
        return dotRes;
    }
    
    // Вертекс функция
    v2f vert (appdata v)
    {
        v2f o;
        o.vertex = UnityObjectToClipPos(v.vertex);
        o.uv = TRANSFORM_TEX(v.uv, _MainTex);
        o.worldNormal = UnityObjectToWorldNormal(v.normal);
        // TRANFER_SHADOW(o);
        // TRANSFER_VERTEX_TO_FRAGMENT(o);
        return o;
    }   
    
    // Пиксельная функция
    half4 frag (v2f i) : SV_Target
    {
        // float atten = LIGHT_ATTENUATION(i);
        return tex2D(_MainTex, i.uv) * _Color * Toon(i.worldNormal, _WorldSpaceLightPos0.xyz);
    }

    

    struct v2fShadow {
        V2F_SHADOW_CASTER;
        UNITY_VERTEX_OUTPUT_STEREO
    };
    
    v2fShadow vertShadow( appdata_base v )
    {
        v2fShadow o;
        UNITY_SETUP_INSTANCE_ID(v);
        UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
        TRANSFER_SHADOW_CASTER_NORMALOFFSET(o)
        return o;
    }
    
    float4 fragShadow( v2fShadow i ) : SV_Target
    {
        SHADOW_CASTER_FRAGMENT(i)
    }

    ENDCG


    SubShader
    {
        Stencil
        {
            Ref [_Stencil]
            ReadMask [_ReadMask]
            WriteMask [_WriteMask]
            Comp [_StencilComp]
            Pass [_StencilOp]
            Fail [_StencilFail]
            ZFail [_StencilZFail]
        }
        
        // Рендеринг
        Pass
        {
            Tags { "RenderType"="Opaque" "Queue" = "Geometry" }
            LOD 100
            Cull [_Culling]
            Offset [_Offset], [_Offset]
            ZWrite [_ZWrite]
            ZTest [_ZTest]
            ColorMask [_ColorMask]
            
            CGPROGRAM
            #pragma target 3.0
            #pragma vertex vert
            #pragma fragment frag
            ENDCG
        }
        
        // Pass to render object as a shadow caster
        Pass
        {
            Name "ShadowCaster"
            Tags { "LightMode" = "ShadowCaster" }
            LOD 80
            Cull [_Culling]
            Offset [_Offset], [_Offset]
            ZWrite [_ZWrite]
            ZTest [_ZTest]
            
            CGPROGRAM
            #pragma vertex vertShadow
            #pragma fragment fragShadow
            #pragma target 2.0
            #pragma multi_compile_shadowcaster
            ENDCG
        }
    }
}
