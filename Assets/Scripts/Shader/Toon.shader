//トゥーンシェーダ
Shader "Custom/Toon" {
	Properties {
	//使用宣言
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_RampTex ("Ramp", 2D) = "white"{}
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		//#pragma surface surf Standard fullforwardshadows

		#pragma surface surf ToonRamp

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _RampTex;

		uniform float _Outline;
		uniform float4 _OutlineColor;

		struct Input {
			float2 uv_MainTex;
		};

		fixed4 _Color;

		//ライティング用メソッド
		fixed4 LightingToonRamp(SurfaceOutput s, fixed3 lightDir, fixed atten)
		{
			//法線とライトの方向ベクトルの内積値(0.0f～1.0f)
			half d = dot(s.Normal, lightDir) * 0.5f + 0.5f;

			//値に応じた明度のUV座標に設定
			fixed3 ramp = tex2D(_RampTex, fixed2(d,0.5f)).rgb;

			//色の計算
			fixed4 c;
			c.rgb = s.Albedo * _LightColor0.rgb * ramp;
			c.a = 0;
			return c;
		}


		void surf (Input IN, inout SurfaceOutput o) {

			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
