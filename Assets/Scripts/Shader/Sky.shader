Shader "Custom/Sky" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		Pass {    

		    Cull Front	//裏面描画
		        
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
                float2 texcoord : TEXCOORD0;
            };

            struct v2f {
                float4 vertex : SV_POSITION;
                float2 uv  : TEXCOORD0;
                fixed4 color : COLOR0;
            };

            //頂点シェーダ
            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.texcoord.xy;
                return o;
            }

            //フラグメントシェーダ
            fixed4 frag(v2f v) : SV_Target {

                v.color = tex2D(_MainTex, v.uv) * _Color;
                return v.color;
            }
            ENDCG
        }
	}
	FallBack "Diffuse"
}
