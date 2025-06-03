#ifndef ADDITIONAL_LIGHT_INCLUDED
#define ADDITIONAL_LIGHT_INCLUDED
#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Lighting/LightDefinition.cs.hlsl"
void MainLight_float(float3 WorldPos, out float3 Direction, out float3 Color)
{
#ifdef SHADERGRAPH_PREVIEW
    Direction = normalize(float3(1.0, 1.0, 0.0));
    Color = 1.0;
#else
    // In HDRP, directional lights are handled separately
    if (_DirectionalLightCount > 0)
    {
        DirectionalLightData light = _DirectionalLightDatas[0];
        Direction = -light.forward; // HDRP stores forward direction
        Color = light.color * light.lightDimmer;
    }
    else
    {
        Direction = float3(0, 1, 0);
        Color = 0;
    }
#endif
}
void AllAdditionalLights_float(float3 WorldPos, float3 WorldNormal, out float3 LightColor)
{
    LightColor = 0.0f;
#ifndef SHADERGRAPH_PREVIEW
    uint lightCount = _PunctualLightCount;
    [loop] for (uint i = 0; i < lightCount; ++i)
    {
        LightData light = _LightDatas[i];
        float3 lightToPixel = WorldPos - light.positionRWS;
        float distanceSqr = dot(lightToPixel, lightToPixel);
        float rangeSqr = light.range * light.range;
        // Early exit se estiver fora do range
        if (distanceSqr > rangeSqr)
            continue;
        float3 lightDirection = normalize(lightToPixel);
        float NdotL = saturate(dot(WorldNormal, -lightDirection));
        // Cálculo direto sem smoothstep
        float3 color = light.color * light.lightDimmer * NdotL;
        // Cálculo de atenuação otimizado
        float distance = sqrt(distanceSqr);
        float attenuation = saturate(1.0 - distance / light.range);
        if (light.lightType == GPULIGHTTYPE_SPOT)
        {
            float cosAngle = dot(light.forward, lightDirection);
            float spotAttenuation = saturate(cosAngle * light.angleScale + light.angleOffset);
            attenuation *= spotAttenuation * spotAttenuation;
        }
        // Aplica atenuação e acumula
        LightColor += color * attenuation;
    }
#endif
}
#endif // ADDITIONAL_LIGHT_INCLUDED