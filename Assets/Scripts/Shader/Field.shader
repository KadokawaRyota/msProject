Shader "Custom/Field" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
	}
	SubShader{
		Tags{ "RenderType" = "Opaque"}

		Stencil{
			Ref 1 // リファレンス値
			Comp Always  // 常にステンシルを成功
			Pass Replace
		}

		LOD 200

		/*CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		float4 pos;

		UNITY_INSTANCING_CBUFFER_START(Props)
		// put more per-instance properties here
		UNITY_INSTANCING_CBUFFER_END

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG*/

		Pass {

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			fixed4 _Color;
			float ScrollX;
			float ScrollY;

			struct appdata {
				float4 vertex   : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f {
				float4 vertex : SV_POSITION;
				float2 uv  : TEXCOORD0;
				fixed4 color : COLOR0;
			};

			//頂点シェーダ
			v2f vert(appdata v) {
				v2f o;
				UNITY_INITIALIZE_OUTPUT(v2f, o);
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}

			//フラグメントシェーダ
			fixed4 frag(v2f v) : SV_Target{

				v.color = tex2D(_MainTex, v.uv) * _Color;
			return v.color;
			}
				ENDCG
		}
	}
	FallBack "Diffuse"
}
