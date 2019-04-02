﻿Shader "Unlit/banbao"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_Color("_Color", Color) = (1,1,1,1)
	}
	SubShader
	{
		Cull Front ZWrite Off
		Tags{"Queue" = "Transparent -100"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"}

		Pass
		{
			 Blend SrcAlpha One

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4 _Color;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
				// apply fog
				//UNITY_APPLY_FOG(i.fogCoord, col);
				col.rbg *= _Color.rbg;
				col.a *=abs(_SinTime.y*0.5);
				return col;
			}
			ENDCG
		}
	}
}