Shader "Custom/ChatImage" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
	}
	SubShader {
		Tags { "RenderType"="Transparent" "Queue" = "Transparent"}
		LOD 200
		
		Pass{
			ZTest Off

			Blend SrcAlpha OneMinusSrcAlpha		//アルファブレンディング有効


			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			fixed4 _Color;

			struct appdata {
				float4 position	: POSITION;
				float2 uv		: TEXCOORD0;
			};

			struct v2f {
				float4 position	: POSITION;
				float2 uv		: TEXCOORD0;
				fixed4 color : COLOR0;
			};

			v2f vert(appdata i)
			{
				v2f o;
				o.position = UnityObjectToClipPos(i.position);	//座標変換

				o.uv = i.uv;
				return o;
			}

			fixed4 frag(v2f o):SV_Target
			{
				o.color = tex2D(_MainTex, o.uv) * _Color;

				return o.color;
			}
			ENDCG
		}
	}
	FallBack "Diffuse"
}
