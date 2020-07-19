Shader "Custom/Terrain"
{
    Properties
    {
		_TopTexAlbedo ("Top Tex Albedo", 2D) = "white" {}
		_TopTexAO("Top Tex Ambient Occlusion", 2D) = "white" {}
		_TopTexNM("Top Tex NM", 2D) = "white" {}
		_TopTexMTL ("Top Tex Metallic", 2D) = "white" {}
		_TopSmoothness("Top Smoothness", Range(0,1)) = 0.5
		_TopTexTiling("Top Tiling", Range(0,20)) = 1

		_SideTexAlbedo("Side Tex Albedo", 2D) = "white" {}
		_SideTexAO("Side Tex Ambient Occlusion", 2D) = "white" {}
		_SideTexNM("Side Tex NM", 2D) = "white" {}
		_SideTexMTL("Side Tex Metallic", 2D) = "white" {}
		_SideTexTiling("Side Tiling", Range(0,20)) = 1

		_Sharpness("Sharpness", Range(0,10)) = 0.5
		_WaterHeight ("Water Height", Range(0, 100)) = 0.2
		_WaterDeepColor("Water Deep Color", Color) = (0.0320, 0.42, 0.97, 0.69)

		_GIAlbedoColor("Color Albedo (GI)", Color) = (1, 1, 1, 1)
		_GIAlbedoTex("Albedo (GI)", 2D) = "white"{}
    }
		

    SubShader
    {
		Pass
		{
			Name "META"
			Tags {"LightMode" = "Meta"}
			Cull Off
			CGPROGRAM

			#include"UnityStandardMeta.cginc"

			sampler2D _GIAlbedoTex;
			fixed4 _GIAlbedoColor;
			float4 frag_meta2(v2f_meta i) : SV_Target
			{
				// We're interested in diffuse & specular colors
				// and surface roughness to produce final albedo.

				FragmentCommonData data = UNITY_SETUP_BRDF_INPUT(i.uv);
				UnityMetaInput o;
				UNITY_INITIALIZE_OUTPUT(UnityMetaInput, o);
				fixed4 c = tex2D(_GIAlbedoTex, i.uv);
				o.Albedo = fixed3(c.rgb * _GIAlbedoColor.rgb);
				o.Emission = Emission(i.uv.xy);
				return UnityMetaFragment(o);
			}

			#pragma vertex vert_meta
			#pragma fragment frag_meta2
			#pragma shader_feature _EMISSION
			#pragma shader_feature _METALLICGLOSSMAP
			#pragma shader_feature ___ _DETAIL_MULX2
			ENDCG
		}

        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _TopTexAlbedo;
		sampler2D _TopTexAO;
		sampler2D _TopTexNM;
		sampler2D _TopTexMTL;
		float _TopSmoothness;

		sampler2D _SideTexAlbedo;
		sampler2D _SideTexAO;
		sampler2D _SideTexNM;
		sampler2D _SideTexMTL;
		float _SideSmoothness;

		float _SideTexTiling;
		float _TopTexTiling;

        struct Input
        {
			float3 worldPos;
        };

		float _Sharpness;
		float _WaterHeight;

		float4 _WaterDeepColor;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)



        void surf (Input IN, inout SurfaceOutputStandard o)
        {
			float2 topuv = IN.worldPos.xz * _TopTexTiling;
			float2 side1uv = IN.worldPos.zy * _SideTexTiling;
			float2 side2uv = IN.worldPos.xy * _SideTexTiling;

			float4 topAlbedo = tex2D(_TopTexAlbedo, topuv);
			float topAO = tex2D(_TopTexAO, topuv);
			float3 topNM = tex2D(_SideTexNM, topuv);
			float topMTL = tex2D(_TopTexMTL, topuv);

			float4 side1Albedo = tex2D(_SideTexAlbedo, side1uv);
			float side1AO = tex2D(_SideTexAO, side1uv);
			float3 side1NM = tex2D(_SideTexNM, side1uv);
			float side1MTL = tex2D(_SideTexMTL, side1uv);

			float4 side2Albedo = tex2D(_SideTexAlbedo, side2uv);
			float side2AO = tex2D(_SideTexAO, side2uv);
			float3 side2NM = tex2D(_SideTexNM, side2uv);
			float side2MTL = tex2D(_SideTexMTL, side2uv);
			
			float3 weights = o.Normal;
			weights = abs(weights);
			weights = pow(weights, _Sharpness);
			weights = normalize(weights);

			float4 albedo = weights.x * side1Albedo + weights.y * topAlbedo + weights.z * side2Albedo;
			float smoothness = weights.x * _SideSmoothness + weights.y * _TopSmoothness + weights.z * _SideSmoothness;
			float metallic = weights.x * side1MTL + weights.y * topAlbedo + weights.z * side2MTL;
			float ao = weights.x * side1AO + weights.y * topAO + weights.z * side2AO;
			
			float waterDepthFactor = min(5., max(0., (_WaterHeight - IN.worldPos.y))) / 5.;
			waterDepthFactor = pow(waterDepthFactor, 1);
			albedo = (1. - waterDepthFactor) * albedo + waterDepthFactor * _WaterDeepColor;

            // Albedo comes from a texture tinted by color
            o.Albedo = clamp(albedo.rgb, 0, 1);
            // Metallic and smoothness come from slider variables
            o.Metallic = clamp(metallic, 0 ,1);
            o.Smoothness = clamp(smoothness, 0, .9);
            o.Alpha = 1.;
			o.Occlusion = clamp(ao, 0, .9);
        }
        ENDCG
    }
    FallBack "Diffuse"
}
