// Unlit shader. Simplest possible colored shader.
// - no lighting
// - no lightmap support
// - no texture

Shader "Robart/Oscilloscope" {
	Properties{
		_Color("Main Color", Color) = (1,1,1,1)
		_PlayerP("Player Position", Vector) = (1,1,1,1)
		_Raster("Raster Line", Range(0,1)) = 0.05 // sliders
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

	float4  _PlayerP;
	float _Raster;

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
		float x = 1 - abs(i.scrPos.x - _Raster) * 20;
		fixed4 col = float4(0.2 + x * 0.5, 1 - x * 0.5, 0.2 + x * 0.5, 1);
		UNITY_APPLY_FOG(i.fogCoord, col);
		UNITY_OPAQUE_ALPHA(col.a);
		return col;
	}
		ENDCG
	}
	}

}