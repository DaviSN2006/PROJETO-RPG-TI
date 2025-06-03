void AdditionalLights_float(float3 WorldPosition, float3 WorldNormal, float2 CutoffThresholds, out float3 LightColor)
{
    LightColor = 0;

    #ifndef SHADERGRAPH_PREVIEW
    for(int i = 0; i < _LightDataCount; ++i)
    {
        LightData light = _LightDatas[i];
        
        // Calculate light direction and distance
        float3 lightVector = light.positionRWS - WorldPosition;
        float distanceSqr = max(dot(lightVector, lightVector), 0.0001);
        float3 lightDir = lightVector * rsqrt(distanceSqr);
        
        // Diffuse calculation
        float NdotL = saturate(dot(WorldNormal, lightDir));
        float diffuse = smoothstep(CutoffThresholds.x, CutoffThresholds.y, NdotL);
        
        // Distance attenuation
        float attenuation = 1.0 / distanceSqr;
        float rangeAtten = saturate(1.0 - (distanceSqr * light.rangeAttenuationScale + light.rangeAttenuationBias));
        attenuation *= rangeAtten * rangeAtten;
        
        // Angular attenuation for spot lights
        if(light.lightType == GPULIGHTTYPE_SPOT)
        {
            float cosAngle = dot(lightDir, -light.forward);
            float angleAtten = saturate((cosAngle - light.iesCut) * light.angleScale + light.angleOffset);
            attenuation *= angleAtten * angleAtten;
        }
        
        // Combine all factors
        float3 color = light.color * diffuse * attenuation;
        color *= light.diffuseDimmer * light.lightDimmer;
        
        LightColor += color;
    }
    #endif
}