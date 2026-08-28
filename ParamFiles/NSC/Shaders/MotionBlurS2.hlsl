// NSUNSC S2 motion blur (program 0x1100000C).
//
// The original S2 shader used a fixed velocity step of 1/112. At 60 FPS the
// velocity buffer contains roughly half the per-frame displacement it has at
// 30 FPS, so the blur lost half of its apparent depth. The API writes
// size1.x = measuredFPS / 30 immediately before this pass.

cbuffer perMaterialBuffer : register(b0)
{
    float2 size1;
};

SamplerState texColor : register(s0);
SamplerState texVelocity : register(s1);
Texture2D texColor_tex : register(t0);
Texture2D texVelocity_tex : register(t1);

struct PixelInput
{
    float4 position : SV_POSITION;
    float4 texcoord : TEXCOORD0;
};

float4 main(PixelInput input) : SV_Target
{
    static const float velocityCenter = 127.0f / 255.0f;
    static const float velocityStep30Fps = 1.0f / 112.0f;
    static const int sampleCount = 17;
    static const float gaussianSigma = 0.28f;

    float2 velocity = texVelocity_tex.Sample(texVelocity, input.texcoord.zw).xy;
    // Seven steps matches the reach of the original eight-tap S2 shader. The
    // API increases size1.x more aggressively above 30 FPS because the linear
    // 2x correction still looked too shallow at 60 FPS.
    float2 blurVector = (velocity - velocityCenter) *
        (velocityStep30Fps * size1.x * 7.0f);

    float4 accumulated = 0.0f;
    float accumulatedWeight = 0.0f;

    [loop]
    for (int sampleIndex = 0; sampleIndex < sampleCount; ++sampleIndex)
    {
        // Symmetric samples avoid the separate, repeated-frame appearance of
        // the old one-sided kernel. Gaussian weighting blends them into one
        // continuous streak while retaining a sharp center contribution.
        float position = ((float)sampleIndex / (sampleCount - 1)) - 0.5f;
        float gaussian = exp(-0.5f *
            (position * position) / (gaussianSigma * gaussianSigma));
        accumulated += texColor_tex.Sample(
            texColor, input.texcoord.xy + blurVector * position) * gaussian;
        accumulatedWeight += gaussian;
    }

    return accumulated / accumulatedWeight;
}
