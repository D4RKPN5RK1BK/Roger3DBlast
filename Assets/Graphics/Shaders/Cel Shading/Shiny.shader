Shader "Test/Shiny"
{
    Properties
    {
        _Color("Albedo", Color) = (1,1,1,1)
        _MainTex ("Texture", 2D) = "white" {}
        _ShadowPosition ("Shadow position", Range(0.0, 1.0)) = 0.5
        _ShadowIntencity ("Shadow intenctity", Range(0.0, 1.0)) = 1
    }

    CGINCLUDE

    #include "UnityCG.cginc"

    half4 _Color;
    sampler2D _MainTex;
    float4 _MainTex_ST;
    float _ShadowPosition;
    float _ShadowIntencity;

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

    // Тень cel shading
    float ShadowCast(float3 normal, float3 lightDir) 
    {
        float o = dot(normal, lightDir);
        o = (o + 1) / 2;
        o = o > _ShadowPosition ? 1 : _ShadowIntencity;
        return o ;
    }

    // Спекулярка cel shading
    float SpecularCast(float3 viewDir, float3 lightDir, float3 normal)
    {
        // float3 reflection = normalize(normalize(viewDir) + normalize(normal));
        float3 r = reflect(viewDir, normal);
        float o =  1/ max(0.0, pow(dot(lightDir, r), 2));
        // o *= o;


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

    half4 frag (v2f i) : SV_TARGET 
    {
        float3 viewDir = normalize(i.viewDir);
        float3 lightDir = _WorldSpaceLightPos0.xyz;
        half4 col = tex2D(_MainTex, i.uv) * ShadowCast(i.normal, lightDir);
        col *= _Color;
        col += col * SpecularCast(viewDir, lightDir, i.normal);
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
