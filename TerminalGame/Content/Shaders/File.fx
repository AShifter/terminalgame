#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

#define distortion 0.08

Texture2D SpriteTexture;
sampler2D source;
float4 sourceSize;

sampler2D SpriteTextureSampler = sampler_state
{
	Texture = <SpriteTexture>;
};

struct VertexShaderOutput
{
	float4 Position : SV_POSITION;
	float4 Color : COLOR0;
	float2 TextureCoordinates : TEXCOORD0;
};

float2 radialDistortion(float2 coord)
{
	float2 cc = TextureCoordinates - float2(0.5);
	float2 dist = dot(cc, cc) * distortion;
	return coord + cc * (1.0 - dist) * dist;
}

float4 MainPS(VertexShaderOutput input) : COLOR
{
	fragColor = texture(source, radialDistortion(TextureCoordinates))
}

technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};