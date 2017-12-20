Shader "Custom/Monitor" {
	Properties {
		_MainColor("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_SubColor("Color", Color) = (1,1,1,1)
		_SubTex("Albedo (RGB)", 2D) = "white" {}
	}
		SubShader{
		Tags{ "Queue" = "Opequ" "RenderType" = "Transparent" }
		LOD 200

		Pass{
		Blend SrcAlpha OneMinusSrcAlpha		//アルファブレンディング有効

		CGPROGRAM

		#include "UnityCG.cginc"	//インクルード

		#pragma vertex vert			//頂点シェーダ宣言
		#pragma fragment frag		//フラグメントシェーダ宣言

		sampler2D _MainTex;
		sampler2D _SubTex;
		fixed4 _MainColor;
		fixed4 _SubColor;

		//取得情報
		struct appdata {
			float4 position : POSITION;		//頂点座標
			float2 uv0		: TEXCOORD0;
			float2 uv1		: TEXCOORD1;
		};

		//出力情報
		struct v2f {
			float4 position : SV_POSITION;	//頂点座標
			fixed4 color : COLOR0;		//カラー
			float2 uv0		:TEXCOORD0;
			float2 uv1		:TEXCOORD1;
		};

		//頂点シェーダ
		v2f vert(appdata i)
		{
			v2f o;
			UNITY_INITIALIZE_OUTPUT(v2f, o);
			o.position = UnityObjectToClipPos(i.position);
			o.uv0 = i.uv0;
			o.uv1 = i.uv1;
			return o;
		}

		//フラグメントシェーダ
		fixed4 frag(v2f o) : SV_Target
		{
			float4 tex1 = tex2D(_MainTex,o.uv0) * _MainColor;
			float4 tex2 = tex2D(_SubTex, o.uv1) * _SubColor;

			//o.color = tex2D(_MainTex,o.uv) * _Color;	//カラー代入


			return tex1 + tex2;
		}
		ENDCG
	}
	}
		FallBack "Diffuse"
}
