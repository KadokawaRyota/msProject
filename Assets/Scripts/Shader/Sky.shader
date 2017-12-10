Shader "Custom/Sky" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("MainTexture", 2D) = "white" {}
		_SubTex ("Subtexture", 2D) = "white" {}
	}
	SubShader {
		Tags { "RenderType"="Transparent" 
				"Queue" = "Transparent"}
		LOD 200

		Pass {    

		    Cull Front	//裏面描画
		        
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            
            sampler2D _MainTex;
			sampler2D _SubTex;
            fixed4 _Color;
            float ScrollX;
            float ScrollY;

            struct appdata {
                float4 vertex   : POSITION;
                float2 uv0 : TEXCOORD0;
				float2 uv1 : TEXCOORD1;
            };

            struct v2f {
                float4 vertex : SV_POSITION;
                float2 uv0  : TEXCOORD0;
				float2 uv1  : TEXCOORD1;
                fixed4 color : COLOR0;
            };

            //頂点シェーダ
            v2f vert (appdata v) {
                v2f o;
				UNITY_INITIALIZE_OUTPUT(v2f, o);
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv0 = v.uv0;
				o.uv1 = v.uv1;

                return o;
            }

            //フラグメントシェーダ
            fixed4 frag(v2f v) : SV_Target {

				fixed2 uv = v.uv1;
				
				uv.x += 1.0f * _Time;
				fixed4 uv1 = tex2D(_SubTex, uv);

				

				v.color = tex2D(_MainTex, v.uv0) * uv1 * _Color;//tex2D(_MainTex, v.uv) * _Color;
                return v.color;
            }
            ENDCG
        }
	}
	FallBack "Diffuse"
}
