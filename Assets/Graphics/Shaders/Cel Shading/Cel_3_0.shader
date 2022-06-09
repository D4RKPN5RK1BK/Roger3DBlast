Shader "Test/CelShader_3_0"
{
    Properties
    {
        _Color("Main color", Color) = (1,1,1,1)
        _AmbientColor("Ambient color", Color) = (1,1,1,1)
        _ShineColor("Light color", Color) = (1,1,1,1)
        _MainTex ("Texture", 2D) = "white" {}
        _ShadowPosition ("Shadow position", Range(0.0, 0.5)) = 0.5
        _ShadowIntencity ("Shadow intenctity", Range(0.0, 1.0)) = 0
        _Smootheness ("Smoothness", Range(1.0, 0.01)) = 1
        _RimIntencity ("Rim intentiy", Range(0.0, 1.0)) = 0.2
    }

    CGINCLUDE

    #include "UnityCG.cginc"

    half4 _Color;
    half4 _AmbientColor;
    half4 _ShineColor;
    sampler2D _MainTex;
    float4 _MainTex_ST;
    float _ShadowPosition;
    float _ShadowIntencity;
    float _Smootheness;
    float _RimIntencity;

    struct appdata 
    {
        float4 vertex : POSITION;
        float2 uv : TEXCOORD0;
        float3 normal : NORMAL;
    };

    struct v2f 
    {
        float4 vertex : SV_POSITION;
        float2 uv : TEXCOORD0;
        float3 normal : NORMAL;
        float3 viewDir : TEXCOORD1;
    };

    struct v2fShadow {
        V2F_SHADOW_CASTER;
        UNITY_VERTEX_OUTPUT_STEREO
    };

    // Тень cel shading
    float4 ShadowCast(float3 normal, float3 lightDir, float4 color) 
    {
        float4 o;
        float4 negative = 1 - color;
        float shadowIntencity = 1 - _ShadowIntencity;
        float intencity = dot(normal, lightDir);
        intencity = (intencity + 1) / 2;
        intencity = smoothstep(_ShadowPosition, _ShadowPosition + 0.01, intencity);
        intencity = max(shadowIntencity, intencity);
        o = (intencity * color) + ((1 - intencity) * _AmbientColor);

        return o;
    }

    // Спекулярка cel shading
    float4 SpecularCast(float3 viewDir, float3 lightDir, float3 normal, float4 color)
    {
        float4 o;
        
        float4 negative = 1 - color;

        float3 halfVector = normalize(viewDir + lightDir);
        float intencity = dot(halfVector, normal);
        intencity = pow(intencity, 1 / _Smootheness);
        intencity = intencity / _Smootheness;
        intencity = smoothstep(0.1, 0.11,  intencity);
        o = intencity * _ShineColor * negative;
        o *= -1;
        return o;
    }

    // Зеркальность
    float4 RimCast(float3 viewDir, float3 lightDir, float3 normal) 
    {
        float4 o = (1,1,1,1);



        return o;
    }

    // Обработка cel шейдера
    float4 Toon(float3 normal, float3 viewDir, float3 lightDir) 
    {
        float4 o;
        o = _Color;
        o += SpecularCast(viewDir, lightDir, normal, o);
        o += ShadowCast(normal, lightDir, o);
        // o += RimCast(viewDir, lightDir, normal);
        return o;
    }

    v2f vert (appdata v) 
    {
        v2f o;
        o.vertex = UnityObjectToClipPos(v.vertex);
        o.uv = TRANSFORM_TEX((v.uv), _MainTex);
        o.normal = UnityObjectToWorldNormal(v.normal);
        o.viewDir = WorldSpaceViewDir(v.vertex);
        return o;
    }

    v2fShadow vertShadow(appdata_base v)
    {
        v2fShadow o;
        UNITY_SETUP_INSTANCE_ID(v);
        UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
        TRANSFER_SHADOW_CASTER_NORMALOFFSET(o);
        return o;
    }

    half4 frag (v2f i) : SV_TARGET 
    {
        float3 viewDir = normalize(i.viewDir);
        float3 lightDir = _WorldSpaceLightPos0;

        half4 col = tex2D(_MainTex, i.uv) * Toon(i.normal, viewDir, lightDir);
        return col;
    }

    ENDCG

    SubShader 
    {
        Pass 
        {
            Tags { "RenderType"="Opaque"}
            LOD 100

            CGPROGRAM
            #pragma target 3.0
            #pragma vertex vert
            #pragma fragment frag
            ENDCG
        }
    }
}
