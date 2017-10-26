//****************************************************
// ステンシルシェーダ
//****************************************************
Shader "Custom/Stencil" {
	Properties{
		_Color("Color", Color) = (1,1,1,1)
		_StencilColor("StencilColor",Color) = (0,0,0,1)
		_MainTex ("Texture", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		//デフォルトのサーフェースシェーダ
		CGPROGRAM

		#pragma surface surf Standard fullforwardshadows	//サーフェースシェーダ宣言

		#pragma target 3.0


		//取得情報
		struct Input {
			float2 uv_MainTex;
		};

		//プロパティ宣言
		sampler2D _MainTex;
		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		//サーフェースシェーダ
		void surf(Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG

		//プレイヤーモデル透過処理（オブジェクトにプレイヤーのシルエットを描画）
		Pass
		{
			Blend SrcAlpha OneMinusSrcAlpha		//アルファブレンディング有効

			Cull Back	//カリングOff
			ZTest off
			Stencil{
				Ref 0		//ステンシル値
				Comp NotEqual	//ステンシル値が同じとき
			}

			CGPROGRAM

			#pragma vertex vert			// 頂点シェーダー宣言


			#pragma fragment frag		// フラグメントシェーダー宣言

			//プロパティ宣言
			fixed4 _StencilColor;

			//取得情報
			struct appdata {
				float4 vertex : POSITION;
			};

			//出力情報
			struct v2f {
				float4 pos : SV_POSITION;
			};

			//頂点シェーダ
			v2f vert(appdata v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				return o;
			}

			//フラグメントシェーダ
			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 col = _StencilColor;

				return col;
			}
			ENDCG
		}


	}
	//FallBack "Diffuse"
}
