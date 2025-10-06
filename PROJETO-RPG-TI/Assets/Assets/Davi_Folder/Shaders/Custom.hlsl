#ifndef ADDITIONAL_LIGHTING_INCLUDED
#define ADDITIONAL_LIGHTING_INCLUDED
void MainLight_float(float3 WorldPos, out float3 Direction, out float3 Color)
{
#ifdef SHADERGRAPH_PREVIEW
    Direction = normalize(float3(1.0, 1.0, 0.0));
    Color = float3(1.0, 1.0, 1.0);
#else
    DirectionalLightData light = _DirectionalLightDatas[0];
    Direction = -light.forward;
    Color = light.color * light.lightDimmer;
#endif
}
void AllAdditionalLights_float(float3 WorldPos, float3 WorldNormal, float2 CutoffThresholds, out float3 LightColor)
{
    LightColor = 0.0f;
    float totalWeight = 0.0f; // Adicionamos um acumulador de peso

#ifndef SHADERGRAPH_PREVIEW
    uint lightCount = _PunctualLightCount;
    
    // Pré-calcula valores para o cutoff
    float minThreshold = CutoffThresholds.x;
    float maxThreshold = CutoffThresholds.y;
    float thresholdRange = max(maxThreshold - minThreshold, 0.0001); // Evita divisão por zero
    
    for (uint i = 0; i < lightCount; ++i)
    {
        LightData light = _LightDatas[i];
        float3 lightToPixel = WorldPos - light.positionRWS;
        float distanceSqr = dot(lightToPixel, lightToPixel);
        
        if (distanceSqr > light.range * light.range)
            continue;
            
        float3 lightDirection = normalize(-lightToPixel);
        float NdotL = dot(WorldNormal, lightDirection);
        
        // Aplica cutoff antes de qualquer outra coisa
        float cutoffFactor = smoothstep(minThreshold, maxThreshold, NdotL);
        if (cutoffFactor <= 0.0)
            continue;
        
        float distance = sqrt(distanceSqr);
        float attenuation = saturate(1.0 - distance / light.range);
        
        if (light.lightType == GPULIGHTTYPE_SPOT)
        {
            float cosAngle = dot(light.forward, -lightDirection);
            float spotAttenuation = saturate(cosAngle * light.angleScale + light.angleOffset);
            attenuation *= spotAttenuation * spotAttenuation;
        }
        
        float3 colorContribution = light.color * light.lightDimmer * attenuation * cutoffFactor;
        LightColor += colorContribution;
        totalWeight += max(max(colorContribution.r, colorContribution.g), colorContribution.b);
    }
    
    // Normalização preservando o cutoff
    if (totalWeight > 0.0)
    {
        LightColor /= totalWeight;
        // Re-aplica a intensidade média para manter o brilho
        LightColor *= (totalWeight / lightCount);
    }
#endif
}
#endif