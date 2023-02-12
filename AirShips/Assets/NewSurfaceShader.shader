Shader "Custom/VertexWind" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
		_Normal("Normal Map", 2D) = "bump" {}

		_CutOff("Alpha Cutoff", Range(0, 1)) = 0.5

		_WindDirection("Wind Direction", Vector) = (1,0,0,0)
		_WindForce("Wind Force", Float) = 0.2

		_WindSpeed("Wind Speed", Float) = 10.0
		_AlphaWindSpeed("Wind Alpha Speed", Float) = 6.0

		_WindFrequency("Wind Frequency", Float) = 0.2
		_AlphaWindFrequency("Alpha Wind Frequency", Float) = 10
	}

	SubShader {
		Tags { "RenderType"="TransparentCutout" "Queue"="AlphaTest" }
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard addshadow vertex:vert

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
			float4 POSITION;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		sampler2D _Normal;
		half _Cutoff;

		float4 _WindDirection;
		float _WindForce;
		float _WindSpeed;
		float _AlphaWindSpeed;
		float _WindFrequency;
		float _AlphaWindFrequency;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		void vert(inout appdata_full v, out Input o) {

			UNITY_INITIALIZE_OUTPUT(Input, o);

			float4 vertexWorldPosition = mul(unity_ObjectToWorld, v.vertex);

			float4 windDistance = float4(vertexWorldPosition.xyz, 0) - _WindDirection;
			float4 windDirection = float4(_WindForce.xxx, 0) * normalize(windDistance);

			// Main Wind
			float windSpeed = _WindSpeed * _Time;
			float windAmplitude = length(windDistance) / _WindFrequency;
			float windStrength = sin(windSpeed + windAmplitude);

			// Alpha wind
			float alphaWindSpeed = _AlphaWindSpeed * _Time;
			float alphaWindAmplitude = length(windDistance) / _AlphaWindFrequency;
			float alphaWindStrength = sin(alphaWindSpeed + alphaWindAmplitude);

			// TODO: Give users access to alter windShift, possibly set it's frequency and phaseshift

			// Remap sin of time (-1 -> 1) to a value 0 -> 1
			float windShift = (((sin(_Time) - -1) * 1) / 2);

			float4 finalOffset = windDirection * lerp(windStrength, alphaWindStrength, windShift);

			finalOffset = v.color * finalOffset;
			finalOffset = v.vertex + mul(unity_WorldToObject, finalOffset);

			v.vertex = finalOffset;
		}

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			clip(c.a - _Cutoff);
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;

			o.Normal = UnpackNormal(tex2D(_Normal, IN.uv_MainTex));

			o.Alpha = 0;
		}
		ENDCG
	}
	FallBack "Diffuse"
}