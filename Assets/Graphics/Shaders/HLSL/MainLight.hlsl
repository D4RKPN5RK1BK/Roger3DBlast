/**
*   Функция для вычесления базавого освещения
*   
*   Входные параметры:
*       WorldPos                        - Позиция относительно мировых координат
* 
*   Выходные параметры:
*       Direction                       - Направление освещения
*       Color                           - Цвет освещения
*       DistanceAtten                   - Сила расстояния ???
*       ShadowAtten                     - Сила тени
*  
*   Внешние функции:
*       TransformViewToProjection       -
*       GetMainLight                    - Возвращает объект Light в качестве параметров принимает ???
*       GetMainShadowsStrenght          - Возвращает основную силу тени
*       SampleShadowmap                 - 
*       GetMainLenghtShadowSamplingData -
*
*   Внешние переменные:
* 
*   Классы:
*       ShadowSamplinData               -
* 
*   GetMainLight дает возможность исопльзовать
*   направление теней, карты теней, цвет итд
**/

void Main_float(float3 WorldPos, out float3 Direction, out float3 Color, out float DistanceAtten, out float ShadowAtten) {
    
    // Что за 
    #ifdef SHADERGRAPH_PREVIEW
        Direction = float3(0.5, 0.5, 0);
        Color = float3(1, 1, 1);
        DistanceAtten = 1;
        ShadowAtten = 1;
    #else
        float4 shadowCoord = TransformViewToProjection(WorldPos);
        Light mainLight = GetMainLight(shadowCoord);
        Direction = mainLight.direction;
        Color = mainLight.color;
        DistanceAtten = mainLight.DistanceAttention

        #if !defined(_MAIN_LIGHT_SHADOWS) || defined(_RECEIVE_SHADOWS_OFF)
            ShadowAtten = 1.0h;
        #else
            ShadowSamplinData shadowSamplingData = GetMainLenghtShadowSamplingData();
            float shadowStrenght = GetMainShadowsStrenght();
            ShadowAtten = SampleShadowmap(shadowCoord, Texture2D_ARGS(_MainLightShadowmapTexture, sampler_MainLightShadowmapTexture), shadowSamplingData, shadowStrenght, false);
        #endif

    #endif


}