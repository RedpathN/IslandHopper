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

		_DepthTex ("Water Depth Texture", 2D) = "black" {}
		_NormalMapTex1 ("Normal Map", 2D) = "white" {}
		_NormalMapTex2 ("Normal Map2", 2D) = "white" {}

		_N1Scale("Normal_1 Scale", Range(0.01,20)) = 1
		_N2Scale("Normal_2 Scale", Range(0.01,20)) = 1
		_N1Str("Normal Strength", Range(0.1,1.5)) = 1

		_SpeedMulti("Speed Multiplier", Range(0.01,30)) = 1
		_Offset("Speed Offset", Range(-1,1)) = 1
		

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

		sampler2D _DepthTex;
		sampler2D _NormalMapTex1;
		sampler2D _NormalMapTex2;

        struct Input
        {
			float2 uv_MainTex;
			float3 worldPos;
        };

        half _Glossiness;
        half _Metallic;
		fixed4 _Color;
		fixed4 _DeepColor;
		float _WaterHeight;
		float _HeightScale;
		float _N1Scale;
		float _N2Scale;
		float _N1Str;
		float _SpeedMulti;
		float _Offset;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
			//float2 uvflipped = float2(IN.uv_MainTex.x, 1. - IN.uv_MainTex.y);
			fixed4 white = fixed4(1., 1., 1., 1.);
			float oceanFloorHeight = tex2D(_DepthTex, IN.uv_MainTex).x;
			float waterDepth = max(0.0, _WaterHeight - oceanFloorHeight * _HeightScale);
			float waterDepthFactor = min(waterDepth, 10.0) / 10.0;
            // Albedo comes from a texture tinted by color
			fixed4 c = waterDepthFactor *_DeepColor + (1. - waterDepthFactor) * _Color;
            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
			float2 nm1offset = float2(.02, .1) * _SpeedMulti * _Time.y;
			float2 nm2offset = float2(.01, -.1) * (_SpeedMulti*_Offset) * _Time.y ;
			o.Normal = abs(normalize(UnpackNormal((tex2D(_NormalMapTex1, ((IN.worldPos.xz + nm1offset) / _N1Scale)) * _N1Str) + (tex2D(_NormalMapTex2, ((IN.worldPos.xz + nm2offset) / _N2Scale)) * _N1Str))));
        }
        ENDCG
    }
    FallBack "Diffuse"
}
