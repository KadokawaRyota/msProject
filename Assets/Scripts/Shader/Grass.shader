Shader "Custom/Grass" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
	}
	SubShader{
		Tags{ "Queue" = "Transparent" "RenderType" = "Transparent"}
		LOD 200

		Blend SrcAlpha OneMinusSrcAlpha		//アルファブレンディング有効

		Cull Off
		//ZWrite Off
		Stencil{
			Ref 1
			Comp Always
			Pass Replace
		}

		CGPROGRAM
		#pragma surface surf Lambert vertex:vert alpha

		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

		void vert(inout appdata_full v, out Input o)
		{
			float3 y = v.vertex.y;
			float power = 0.01f * sin(_Time * 100 + v.vertex.x * 100);
			UNITY_INITIALIZE_OUTPUT(Input, o);
			v.vertex.xyz = float3(v.vertex.x + power * (v.vertex.y * 0.5f + 0.5f), v.vertex.y, v.vertex.z);

		}

		void surf(Input IN, inout SurfaceOutput o) {
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Alpha = c.a;;
		}
		ENDCG
	}
	FallBack "Diffuse"
}

