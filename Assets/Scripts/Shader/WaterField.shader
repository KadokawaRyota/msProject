Shader "Custom/WaterField" {
	Properties{
		_MainColor("MainColor", Color) = (1,1,1,1)
		_MainTex("MainTexture", 2D) = "white" {}
		_MainSpeedX("MainSpeed X",Range(-2.0,2.0)) = 0.5
		_MainSpeedY("MainSpeed Y",Range(-2.0,2.0)) = 0.5
		_SubColor("SubColor", Color) = (1,1,1,1)
		_SubTex("SubTexture", 2D) = "white" {}
		_SubSpeedX("SubSpeed X",Range(-2.0,2.0)) = 0.5
		_SubSpeedY("SubSpeed Y",Range(-2.0,2.0)) = 0.5
		_Power("Power",Range(0.0,0.05)) = 0.001

	}
	SubShader{
		Tags{ "RenderType" = "Opaque" }
		LOD 200


		//ステンシル
		Stencil{
			Ref 1
			Comp Always	//ステンシル値が同じところに描画
			Pass Replace
		}
		CGPROGRAM

		
		#pragma surface surf Lambert vertex:vert
		#pragma target 3.0

		//プロパティ宣言
		sampler2D _MainTex;
		fixed4 _MainColor;
		float _MainSpeedX;
		float _MainSpeedY;

		sampler2D _SubTex;
		fixed4 _SubColor;
		float _SubSpeedX;
		float _SubSpeedY;

		float _Power;


		struct Input {
			float2 uv_MainTex;
		};

		 void vert(inout appdata_full v, out Input o )
        {
            UNITY_INITIALIZE_OUTPUT(Input, o);
            float amp1 = _Power*sin(_Time*100 + v.vertex.x * 100);
            float amp2 = _Power*cos(_Time*100 + v.vertex.x * 100);
            v.vertex.xyz = float3(v.vertex.x, v.vertex.y + amp1, v.vertex.z + amp2);            
            //v.normal = normalize(float3(v.normal.x+offset_, v.normal.y, v.normal.z));
        }

		//サーフェースシェーダ	
		void surf(Input IN, inout SurfaceOutput  o) {

			fixed2 uv = IN.uv_MainTex;	//テクスチャ座標取得

			//メインテクスチャのUV座標をスクロール
			uv.x += _MainSpeedX * _Time;	//X座標
			uv.y += _MainSpeedY * _Time;	//Y座標
			fixed4 c1 = tex2D(_MainTex, uv) * _MainColor;	//メインカラーと合成

			//サブテクスチャのUV座標をスクロール
			uv.x += _SubSpeedX* _Time;		//X座標
			uv.y += _SubSpeedY * _Time;		//Y座標
			fixed4 c2 = tex2D(_SubTex,uv) * _SubColor;		//サブカラーと合成

			o.Albedo = c1 + c2;		//2枚のテクスチャを合成（*なら乗算合成、+なら加算合成）
		}
		ENDCG
	}
	FallBack "Diffuse"
}
