Shader "Custom/ChatImage" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
	}
	SubShader{
		Tags{ "Queue" = "Transparent+100" "RenderType" = "Transparent" }
		LOD 200

		Pass{
			Blend SrcAlpha OneMinusSrcAlpha		//アルファブレンディング有効

			ZTest Off

			CGPROGRAM

			#include "UnityCG.cginc"	//インクルード

			#pragma vertex vert			//頂点シェーダ宣言
			#pragma fragment frag		//フラグメントシェーダ宣言

			sampler2D _MainTex;
			fixed4 _Color;

			//取得情報
			struct appdata {
				float4 position : POSITION;		//頂点座標
				float2 uv		: TEXCOORD0;
			};

			//出力情報
			struct v2f {
				float4 position : SV_POSITION;	//頂点座標
				fixed4 color	: COLOR0;		//カラー
				float2 uv		:TEXCOORD0;
			};
		
			//頂点シェーダ
			v2f vert(appdata i)
			{
				v2f o;
				UNITY_INITIALIZE_OUTPUT(v2f, o);
				o.position = UnityObjectToClipPos(i.position);
				o.uv = i.uv;
				return o;
			}

			//フラグメントシェーダ
			fixed4 frag(v2f o) : SV_Target
			{
				o.color = tex2D(_MainTex,o.uv) * _Color;	//カラー代入

				
				return o.color;
			}
			ENDCG
		}
	}
	FallBack "Diffuse"
}
