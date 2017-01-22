// Unlit shader. Simplest possible textured shader.
// - no lighting
// - no lightmap support
// - no per-material color

Shader "Robart/Menu" {
	Properties{
		_MainTex("Base (RGBA)", 2D) = "white" {}
		_MouseX("Mouse X", Range(0,1)) = 0.05 // sliders
		_Length("Length", Range(0,1)) = 0.05 // sliders
	}

		SubShader{
			Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
		LOD 100

			ZWrite Off
			Blend SrcAlpha OneMinusSrcAlpha


		Pass{
		CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma target 2.0
#pragma multi_compile_fog

#include "UnityCG.cginc"

		struct appdata_t {
		float4 vertex : POSITION;
		float2 texcoord : TEXCOORD0;
		UNITY_VERTEX_INPUT_INSTANCE_ID
	};

	struct v2f {
		float4 vertex : SV_POSITION;
		float2 texcoord : TEXCOORD0;
		float4 scrPos : TEXCOORD1;
		UNITY_FOG_COORDS(1)
			UNITY_VERTEX_OUTPUT_STEREO
	};

	sampler2D _MainTex;

	float4 _MainTex_ST;
	float _MouseX;
	float _Length;

	v2f vert(appdata_t v)
	{
		v2f o;
		UNITY_SETUP_INSTANCE_ID(v);
		UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
		o.vertex = UnityObjectToClipPos(v.vertex);
		o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
		o.scrPos = ComputeScreenPos(o.vertex);
		UNITY_TRANSFER_FOG(o,o.vertex);
		return o;
	}

	fixed4 frag(v2f i) : SV_Target
	{
		fixed4 col = tex2D(_MainTex, i.texcoord);

		float x = _Length + 1 - abs(i.scrPos.x - _MouseX) * 10;
		col *= x;
		UNITY_APPLY_FOG(i.fogCoord, col);
		//UNITY_OPAQUE_ALPHA(col.a);
		return col;
	}
		ENDCG
	}
	}

}
