Shader "Test/CelShading"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Layers ("Number of Layers", Range(0.0, 50.0)) = 3
        _LayerThickness ("Thickness of the gradient layer", Range(0.0, 1.0)) = 1
        _ShadowStrenght ("ShadowStreng", Range(0.0, 1.0)) = 0
        _MaxShadowIntencity ("Max shadow intencity", Range(0.0, 1.0)) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

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
                float3 worldNormal: NORMAL;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Layers;
            float _MaxShadowIntencity;
            float _ShadowStrenght;
            float _LayerThickness;

            float Toon(float3 normal, float3 lightDirection) {

                float dotRes =  max(0.0, dot(normalize(normal), normalize(lightDirection)));
                
                dotRes += (1 - dotRes) * _ShadowStrenght;
                dotRes = floor(dotRes/_LayerThickness) / _Layers;
                dotRes += (1 - dotRes) * _MaxShadowIntencity;
                return dotRes;
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.worldNormal = UnityObjectToWorldNormal(v.normal); 
                
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                col *= Toon(i.worldNormal, _WorldSpaceLightPos0.xyz);
                return col;
            }
            ENDCG
        }
    }
}
