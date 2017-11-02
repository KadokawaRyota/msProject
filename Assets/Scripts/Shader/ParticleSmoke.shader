// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Test1" {
	Properties{
		_MainTex("main", 2D) = "white" {}
	_Color("Color", Color) = (1,1,1,1)

	}
		SubShader{
		Pass{
		Tags{ "RenderType" = "Transparent" "Queue" = "Transparent" }
		Blend SrcAlpha OneMinusSrcAlpha
		//ZWrite off
		CGPROGRAM
#pragma vertex vert
#pragma fragment frag


		uniform sampler2D _MainTex;
	fixed4 _Color;
	struct v2f {
		float4 pos : SV_POSITION;
		float2 uv : TEXCOORD0;
	};

	v2f vert(float4 pos : POSITION , float2 uv : TEXCOORD0) {
		v2f o;
		o.pos = UnityObjectToClipPos(pos);
		o.uv = uv;

		return o;
	}


	float4 frag(v2f i) :COLOR{
		return tex2D(_MainTex,i.uv) * _Color;
	}


		ENDCG
	}
	}
		FallBack "Diffuse"
}