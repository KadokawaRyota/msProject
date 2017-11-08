Shader "Custom/Grass" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Power("Power",Range(-0.05,0.05)) = 0.02
		_Frame("Time",Range(0,100)) = 100
	}
	SubShader{
		Tags{ "Queue" = "Transparent" "RenderType" = "Transparent"}
		LOD 200

		Blend SrcAlpha OneMinusSrcAlpha		//アルファブレンディング有効

		Cull Off	//両面描画
		
		//ステンシル
		Stencil{
			Ref 1
			Comp Always
			Pass Replace
		}

		CGPROGRAM
		#pragma surface surf Lambert vertex:vert alpha

		#pragma target 3.0

		sampler2D _MainTex;
		fixed4 _Color;
		float _Power;
		int _Frame;

		struct Input {
			float2 uv_MainTex;
		};

		//頂点シェーダ
		void vert(inout appdata_full v, out Input o)
		{
			//サーフェースシェーダへ送る情報を設定
			UNITY_INITIALIZE_OUTPUT(Input, o);

			//揺らす力と間隔を設定
			float power = _Power * sin(_Time * _Frame + v.vertex.z * 100);

			//頂点の設定
			v.vertex.xyz = float3(v.vertex.x + power * (v.texcoord.y), v.vertex.y, v.vertex.z);

		}

		//サーフェースシェーダ
		void surf(Input IN, inout SurfaceOutput o) {
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;;
		}
		ENDCG
	}
	FallBack "Diffuse"
}

