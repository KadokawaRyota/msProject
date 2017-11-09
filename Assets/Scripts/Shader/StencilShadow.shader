//****************************************************
// ステンシルシャドウシェーダ
//****************************************************

Shader "Custom/StencilShadow" {
	Properties{
		_Color("Color", Color) = (1,1,1,1)
		_ShadowColor("ShadowColor", Color) = (1,1,1,1)
	}
	SubShader{
		Tags { "RenderType" = "Transparent" 
				"Queue" = "Transparent"}

		Blend SrcAlpha OneMinusSrcAlpha		//アルファブレンディング有効

		//裏側描画
		Pass
		{
			Cull Front
			ColorMask 0		//カラーを透過
			ZWrite Off

			//ステンシル設定
			Stencil{
				Ref 4			//バッファ値
				Comp Always		//常にステンシルを成功
				Pass Replace	//バッファ値をバッファに書き込み
			}

			CGPROGRAM	//スタート

			// 頂点シェーダー使う時用
			#pragma vertex vert

			// フラグメントシェーダー使う時用
			#pragma fragment frag

			fixed4 _Color;		//カラー
		
			//頂点シェーダ取得用
			struct appdata {
				float4 vertex : POSITION;
			};

			//フラグメントシェーダ取得用
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
			fixed4 frag (v2f i) : SV_Target
			{
				//色の設定
				fixed4 col = _Color;
				return col;
			}
			ENDCG
		}

		//表側描画
		Pass
		{

			Cull Back

			//ステンシル設定
			Stencil{
				Ref 4			//バッファ値
				Comp NotEqual	//バッファ値と違うバッファ値の時
			}

			CGPROGRAM		//スタート

			#pragma vertex vert			// 頂点シェーダー宣言

			#pragma fragment frag			// フラグメントシェーダー宣言


			fixed4 _ShadowColor;		//シャドウカラー

			//頂点シェーダ取得用
			struct appdata {
				float4 vertex : POSITION;
			};

			//フラグメントシェーダ取得用
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
				//色の設定
				fixed4 col = _ShadowColor;
				return col;
			}
			ENDCG
		}

	}
	FallBack "Diffuse"
}
