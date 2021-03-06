﻿// Unlit shader. Simplest possible colored shader.
// - no lighting
// - no lightmap support
// - no texture

Shader "Robart/Wave" {
	Properties{
		_PlayerX("Player X", Range(0,1)) = 0.05 // sliders
		_Ampli("Ampli", Range(-1,1)) = 0.05 // sliders
		_Length("Length", Range(0,1)) = 0.05 // sliders
	}

		SubShader{
		Tags{ "RenderType" = "Opaque" }
		LOD 100

		Pass{
		CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma target 2.0
#pragma multi_compile_fog

#include "UnityCG.cginc"

		struct appdata_t {
		float4 vertex : POSITION;
		float4 scrPos : TEXCOORD1;
		UNITY_VERTEX_INPUT_INSTANCE_ID
	};

	struct v2f {
		float4 vertex : SV_POSITION;
		float4 scrPos : TEXCOORD1;
		UNITY_FOG_COORDS(0)
			UNITY_VERTEX_OUTPUT_STEREO
	};

	float _PlayerX;
	float _Length;
	float _Ampli;

	v2f vert(appdata_t v)
	{
		v2f o;
		UNITY_SETUP_INSTANCE_ID(v);
		UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
		o.vertex = UnityObjectToClipPos(v.vertex);
		o.scrPos = ComputeScreenPos(o.vertex);
		UNITY_TRANSFER_FOG(o,o.vertex);
		return o;
	}

	fixed4 frag(v2f i) : COLOR
	{
		float x = (1 - abs(i.scrPos.x - _PlayerX) * 5) *0.2;
		//x = 0;
		float thing = x - _Length * 0.2 ;
		float y = (i.scrPos.y - 0.5) * 200;
		float otherY = (1 - (i.scrPos.y - 0.5)) * 0.5f;
		//if (i.scrPos.y > 0.5)
//			y *= -1;
		y *= _Ampli;
		fixed4 col = float4(0.2 + thing + y, 0.1 + thing - y, 0.1 + thing + otherY, 1);
		//col = float4(x, 0, 0, 0);
		UNITY_APPLY_FOG(i.fogCoord, col);
		UNITY_OPAQUE_ALPHA(col.a);
		return col;
	}
		ENDCG
	}
	}

}