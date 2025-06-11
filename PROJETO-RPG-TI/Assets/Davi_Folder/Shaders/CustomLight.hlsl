#ifndef CUSTOM_SHADOW_DETECTION_HDRP_INCLUDED
#define CUSTOM_SHADOW_DETECTION_HDRP_INCLUDED
#pragma multi_compile _ _MAIN_LIGHT_SHADOWS
#pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCADE
#pragma multi_compile _ _SHADOWS_SOFT // (Sombras suaves, opcional)

// Matrizes (mantida para compatibilidade, mas não usada diretamente)
float4x4 _DirectionalShadowMatrices[MAX_SHADOWED_DIRECTIONAL_LIGHT_COUNT];

void HDRPShadowCalculation_float(
    float3 WorldPos,
    float3 WorldNormal,
    float3 BaseColor,
    out float3 OutputColor,
    out float ShadowAttenuation)
{
    #ifdef SHADERGRAPH_PREVIEW
        // Modo preview - valores padrão
        OutputColor = BaseColor;
        ShadowAttenuation = 1.0;
    #else
        // Acessa dados da luz direcional principal
        DirectionalLightData light = _DirectionalLightDatas[0];
        HDDirectionalShadowData directionalData = _HDDirectionalShadowData[0];
        
        // Cálculo básico de iluminação (NdotL)
        float NdotL = saturate(dot(WorldNormal, -light.forward));
        
        // Inicialização do cálculo de sombras
        ShadowAttenuation = 1.0;
        
        if (light.shadowIndex >= 0 && light.shadowIndex < _HDShadowDatas.Length)
        {
            // Obtém dados específicos da sombra
            HDShadowData shadowData = _HDShadowDatas[light.shadowIndex];
            
            // Calcula coordenadas de sombra com normal bias
            float3 shadowPos = WorldPos + WorldNormal * shadowData.normalBias;
            float4 shadowCoord = mul(shadowData.shadowToWorld, float4(shadowPos, 1.0));
            
            // Evita divisão por zero
            if (shadowCoord.w != 0.0)
                shadowCoord.xyz /= shadowCoord.w;
            
            // Verifica se está dentro do frustum
            if (all(abs(shadowCoord.xyz) < 1.0))
            {
                // Converte para coordenadas do atlas
                float2 uv = shadowCoord.xy * 0.5 + 0.5;
                uv = uv * shadowData.shadowMapSize.zw + shadowData.atlasOffset;
                uv = saturate(uv); // Garante que não ultrapasse o atlas
                
                // Amostra o shadowmap
                float depth = SAMPLE_TEXTURE2D_LOD(_ShadowmapAtlas, s_linear_clamp_sampler, uv, 0).r;
                float lightDepth = shadowCoord.z;
                
                // Comparação de profundidade com bias
                ShadowAttenuation = (lightDepth < depth + shadowData.zBufferParam.x) ? 1.0 : 0.0;
                
                // Aplica filtro PCF (3x3)
                if (shadowData.shadowFilterParams0.x > 0)
                {
                    float2 filterSize = shadowData.shadowFilterParams0.yz;
                    float sum = 0.0;
                    int samples = 0;
                    
                    for (int i = -1; i <= 1; i++)
                    {
                        for (int j = -1; j <= 1; j++)
                        {
                            float2 offset = float2(i, j) * filterSize;
                            float2 sampleUV = uv + offset;
                            
                            if (all(sampleUV >= 0.0) && all(sampleUV <= 1.0))
                            {
                                depth = SAMPLE_TEXTURE2D_LOD(_ShadowmapAtlas, s_linear_clamp_sampler, sampleUV, 0).r;
                                sum += (lightDepth < depth + shadowData.zBufferParam.x) ? 1.0 : 0.0;
                                samples++;
                            }
                        }
                    }
                    ShadowAttenuation = (samples > 0) ? (sum / samples) : ShadowAttenuation;
                }
            }
            
            // Aplica fade entre cascatas
            if (directionalData.fadeScale > 0)
            {
                float fadeFactor = saturate(shadowCoord.z * directionalData.fadeScale + directionalData.fadeBias);
                ShadowAttenuation = lerp(ShadowAttenuation, 1.0, fadeFactor);
            }
            
            // Aplica shadowDimmer
            ShadowAttenuation = lerp(1.0, ShadowAttenuation, light.shadowDimmer);
        }
        
        // Cálculo final da iluminação
        OutputColor = BaseColor * NdotL * light.color * (light.diffuseDimmer * ShadowAttenuation);
    #endif
}
#endif