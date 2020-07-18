Shader "Custom/Water"
{
    Properties
    {
		_HeightScale ("Map Height", Range(0,100)) = 65.0
		_WaterHeight ("Water Height", Range(0,100)) = 0.2
		_Color ("Shallow Color", Color) = (0.328, 0.795, 0.952, 0.690)
		_DeepColor ("Deep Color", Color) = (0.0320, 0.42, 0.97, 0.69)
        _Glossiness ("Smoothness", Range(0,1)) = 0.033
        _Metallic ("Metallic", Range(0,1)) = 0.042
		_MainTex ("Water Depth Texture", 2D) = "white" {}
    }
    SubShader
    {
		Tags {"Queue" = "Transparent" "RenderType" = "Transparent" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard alpha:fade

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
		fixed4 _Color;
		fixed4 _DeepColor;
		float _WaterHeight;
		float _HeightScale;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
			float2 uvflipped = float2(IN.uv_MainTex.x, 255. - IN.uv_MainTex.y);
			fixed4 white = fixed4(1., 1., 1., 1.);
			float oceanFloorHeight = tex2D(_MainTex, IN.uv_MainTex).x;
			float waterDepth = max(0.0, _WaterHeight - oceanFloorHeight * _HeightScale);
			float waterDepthFactor = min(waterDepth, 10.0) / 10.0;
            // Albedo comes from a texture tinted by color
			fixed4 c = waterDepthFactor *_DeepColor + (1. - waterDepthFactor) * _Color;
            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
