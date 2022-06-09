Shader "Test/CellShader_2_2"
{
    Properties
    {
        _Color("Main color", Color) = (1,1,1,1)
        _MainTex ("Texture", 2D) = "white" {}
        _ShadowPosition ("Shadow position", Range(0.0, 0.5)) = 0.5
        _ShadowIntencity ("Shadow intenctity", Range(0.0, 1.0)) = 0
        _Smootheness ("Smoothness", Range(0.0, 1.0)) = 1
        _RimIntencity ("Rim intentiy", Range(0.0, 1.0)) = 1
    }

    CGINCLUDE


    #include "UnityCG.cginc"
    #include "Lighting.cginc"
    #include "AutoLight.cginc"

    half4 _Color;
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

    
    struct v2fShadow {
        V2F_SHADOW_CASTER;
        UNITY_VERTEX_OUTPUT_STEREO
    };

    // Тень cel shading
    float ShadowCast(float3 normal, float3 lightDir, float shadow) 
    {
        float intencity = max(0.0, (dot(normal, lightDir) * shadow));
        float shadowIntencity = 1 - _ShadowIntencity;
        intencity = (intencity + 1) / 2;
        intencity = smoothstep(_ShadowPosition, _ShadowPosition + 0.01, intencity);
        intencity = max(shadowIntencity, intencity);
        
        return intencity;
    }

    // Спекулярка cel shading
    float SpecularCast(float3 viewDir, float3 lightDir, float3 normal)
    {
        float4 o;

        float3 halfVector = normalize(viewDir + lightDir);
        float intencity = dot(halfVector, normal);
        intencity = pow(intencity, _Smootheness * 100);
        intencity = smoothstep(0.1, 0.11,  intencity);
        intencity = intencity * _Smootheness;
        return intencity;
    }

    // Зеркальность
    float RimCast(float3 viewDir, float3 lightDir, float3 normal) 
    {
        float intencity = 1;
        float lightIntencity = (dot(normal, lightDir) + 1) / 2;
        intencity *= lightIntencity;
        intencity *= 1 - dot(viewDir, normal);
        intencity = pow(intencity, _RimIntencity * 5);
        intencity = smoothstep(0.1, 0.11, intencity);
        intencity *= _RimIntencity;
        intencity = min(intencity, 1.0);

        return intencity;
    }

    // Обработка cel шейдера
    float Toon(float3 normal, float3 viewDir, float3 lightDir, float shadow) 
    {
        float intencity;
        intencity = ShadowCast(normal, lightDir, shadow);
        intencity += SpecularCast(viewDir, lightDir, normal);
        intencity += RimCast(viewDir, lightDir, normal);
        return intencity;
    }

    

    

    v2fShadow vertShadow(appdata_base v)
    {
        v2fShadow o;
        UNITY_SETUP_INSTANCE_ID(v);
        UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
        TRANSFER_SHADOW_CASTER_NORMALOFFSET(o);
        return o;
    }

    float4 fragShadow(v2fShadow i) : SV_Target
    {
        SHADOW_CASTER_FRAGMENT(i);
    }

    ENDCG

    SubShader 
    {
        Pass 
        {
            Tags 
            { 
                "LightMode"="ForwardBase"
                "PassFlags"="OnlyDirectional"    
            }
            LOD 100

            CGPROGRAM
            #include "Lighting.cginc"
            #include "AutoLight.cginc"
            #pragma target 3.0
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fwdbase
            

            struct v2f 
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
                float3 viewDir : TEXCOORD1;
                SHADOW_COORDS(2)
            };


            v2f vert (appdata v) 
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX((v.uv), _MainTex);
                o.normal = UnityObjectToWorldNormal(v.normal);
                o.viewDir = WorldSpaceViewDir(v.vertex);
                TRANSFER_SHADOW(o)
                return o; 
            }

            half4 frag (v2f i) : SV_Target
            {
                float3 viewDir = normalize(i.viewDir);
                float3 lightDir = _WorldSpaceLightPos0;
                float shadow = SHADOW_ATTENUATION(i);


                half4 col = tex2D(_MainTex, i.uv) * Toon(i.normal, viewDir, lightDir, shadow) * _Color;
                return col;
            }
            
            ENDCG



        }

        Pass 
        {
            Name "ShadowCaster"
            Tags {"LightMode"="ShadowCaster"}
            LOD 80

            CGPROGRAM
            #pragma vertex vertShadow
            #pragma fragment fragShadow
            #pragma target 3.0
            #pragma multi_compile_shadowcaster
            ENDCG
        }

    }
}
