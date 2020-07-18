Shader "Custom/Terrain"
{
    Properties
    {
        _TopColor ("Top Color", Color) = (0.0,0.5,0.2,1)
		_SideColor ("Side Color", Color) = (0.8, 0.8, 0.8, 1)
		_Sharpness("Sharpness", Range(0,10)) = 0.5
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
		_WaterHeight ("Water Height", Range(0, 100)) = 0.2
		_WaterDeepColor("Water Deep Color", Color) = (0.0320, 0.42, 0.97, 0.69)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
			float3 worldPos;
        };

        half _Glossiness;
		half _Metallic;
		float _Sharpness;
		float _WaterHeight;
		fixed4 _TopColor;
		fixed4 _SideColor;
		fixed4 _WaterDeepColor;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {

			fixed3 weights = o.Normal;
			weights = abs(weights);
			weights = pow(weights, _Sharpness);
			weights = normalize(weights);
			fixed4 rightComponent = weights.x * _SideColor;
			fixed4 topComponent = weights.y * _TopColor;
			fixed4 forwardComponent = weights.z * _SideColor;
			fixed4 color = rightComponent + forwardComponent + topComponent;
			
			float waterDepthFactor = min(10., max(0., (_WaterHeight - IN.worldPos.y))) / 10.;
			waterDepthFactor = pow(waterDepthFactor, 3);
			color = (1. - waterDepthFactor) * color + waterDepthFactor * _WaterDeepColor;

            // Albedo comes from a texture tinted by color
            o.Albedo = color.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = 1.;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
