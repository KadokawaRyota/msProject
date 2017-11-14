//****************************************************
// ステンシルシェーダ
//****************************************************
Shader "Custom/Stencil" {
	Properties{
		_Color("Color", Color) = (1,1,1,1)
		_StencilColor("StencilColor",Color) = (0,0,0,1)
		_MainTex ("Texture", 2D) = "white" {}
		_RampTex ("Ramp", 2D) = "white"{}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}
	SubShader {
		Tags { "RenderType"="Transparent" 
				"Queue" = "Transparent+1" }
		LOD 200

		Blend SrcAlpha OneMinusSrcAlpha		//アルファブレンディング有効
		//デフォルトのサーフェースシェーダ
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

		//プレイヤーモデル透過処理（オブジェクトにプレイヤーのシルエットを描画）
		Pass
		{
			Blend SrcAlpha OneMinusSrcAlpha		//アルファブレンディング有効

			Stencil{
				Ref 3		//ステンシル値
				Comp Equal	//ステンシル値が同じとき
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
	FallBack "Diffuse"
}
