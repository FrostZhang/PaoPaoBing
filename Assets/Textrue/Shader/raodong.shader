Shader "Hidden/raodong"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_Noise ("Noise", 2D) = "white" {}
	}
	SubShader
	{
		Cull Off ZWrite Off ZTest Always
		Tags{"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"}
		Pass
		{
			Blend SrcAlpha OneMinusSrcAlpha
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;
			sampler2D _Noise;

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);
				float2 uv = i.uv;
				uv.x += _Time.y;
				fixed4 col2 = tex2D(_Noise, uv);
				col.rgb = lerp(col.rgb, col2.rgb,0.5);
				// just invert the colors
				return col;
			}
			ENDCG
		}
	}
}
