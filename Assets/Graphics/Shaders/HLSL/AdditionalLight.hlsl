/**
*   Функция для вычесления всех дополнительных источников света.
*   
*   Входные параметры:
*       SpecColor                   - Входной точечный свет 
*       Smoothness                  - Шершавость объекта
*       WorldPosition               - Позиция в мировом пространстве
*       WorldNormal                 - Вектор нормали мирового пространства???
*       WorldView                   - ???
*
*   Выходные параметры:
*       Diffuse                     - Итоговое рассеяное освещение
*       Specular                    - Итоговое точечное освещение
*   
*   
*   Используемые внешние функции:
*       SafeNormalaze               - Безопасная нормализация вектора
*       GetAdditionalLightCount     - Возвращает количество дополнительных источников освещения
*       GetAdditionalLight          - Возворащает объект Light, в качестве параметров принимает
*                                     индекс и позицию относительно мировых координат
*       LightingLambert             -
*       LightingSpecular            -
*       exp2                        - 
*
*   Используемые внешние переменные:
*
*   Классы:   
*       Light                       - Класс используемый для хранения информации об освещении
*                                     
**/

void Main_float(float3 SpecularColor, float Smoothness, float3 WorldPosition, float3 WorldNormal, float3 WorldView, out float3 Diffuse, out float3 Specular) {
    
    
    float3 diffuseColor = 0;
    float3 specularColor = 0;

    #ifndef SHADERGRAPH_PREVIEW

        Smoothness = exp2(10 * Smoothness + 1);
        WorldNormal = normalize(WorldNormal);
        WorldView = SafeNormalize(WorldView);

        int pixelLightCount = GetAdditionalLightsCount();

        for (int i = 0; i < pixelLightCount; i++) {
            Light light = GetAdditionalLight(i, WorldPosition);
            half3 attenuatedLightColor = light.color * (light.distanceAttenuation * light.ShadowAttenuation);
            diffuseColor += LightingLambert(attenuatedLightColor, light.direction, worldNormal);
            specularColor += LightingSpeculat(attenuatedLightColor, light.direction, WorldNormal, WorldView, float4(SpecularColor, 0), Smoothness);
        }

    #endif

    Diffuse = diffuseColor;
    Specular = specularColor;
}