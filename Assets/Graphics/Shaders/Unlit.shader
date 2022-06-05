/**
 *  Unlit shader c подробными комментариями для понимашия основ написания
 *  шейдеров в Unity.
 *  
 *  Данный шейдер не встроен в пайплайн unity.
**/

// Расположение шейдера во вкладках
Shader "Test/UnlitExample"
{
    // Свойства отображаемые в инспекторе юнити
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }

    // Основной код шейдера
    SubShader
    {
        // Свойства рендеринга
        Tags { "RenderType"="Opaque" }

        // Уровень детализации шейдера (Level Of Detail)
        LOD 100

        Pass
        {
            // Вычесления шейдера проходят внутри данного указателя
            CGPROGRAM

            // Указывают компелятору наличие функций для чтоения vertex и fragment шейдера соответственно
            #pragma vertex vert
            #pragma fragment frag
            
            // Подключенные библиотеки
            #include "UnityCG.cginc"

            /**
             *  Структура входящих данных передаваемая самой unity
             *  Определяется следующими свойствами:
             *      POSITION: позиция точки в пространстве (float3, float4)
             *      NORMAL: вектор нормали (float3)
             *      TEXTCOORD0: координаты первой текстуры
             *      TEXTCOORD1 (2,3,4): координаты вторых, третих итд текстур
             *      TANGENT: касательный вектор, используется для карт нормалей 
             *      COLOR: цвет передоваемый точке (float4)
             **/
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            // Структура для отрисовки точек в пространстве
            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };


            sampler2D _MainTex;
            float4 _MainTex_ST;

            // Функция применяемая к каждой точке переданной в шейдер
            v2f vert (appdata v)
            {
                v2f output;
                output.vertex = UnityObjectToClipPos(v.vertex);
                output.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return output;
            }

            // Функция применяемая к каждому пикселю на экране
            fixed4 frag (v2f input) : SV_Target
            {
                // Наложение текстурки
                fixed4 col = tex2D(_MainTex, input.uv);
                return col;
            }
            ENDCG
        }
    }
}
