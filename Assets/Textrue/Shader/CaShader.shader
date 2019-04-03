Shader "Unlit/CaShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Brightness("Brightness",float) = 1
		_EdgeOnly("Edge Only", Float) = 1.0
		_EdgeColor("Edge Color", Color) = (0, 0, 0, 1)
		_BackgroundColor("Background Color", Color) = (1, 1, 1, 1)
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			ZTest Always Cull Off ZWrite Off

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
				float3 normal:NORMAL;
			};

			struct v2f
			{
				float2 uv[9] : TEXCOORD0;
				//float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			uniform half4 _MainTex_TexelSize;
			float4 _MainTex_ST;
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				float2 uv = TRANSFORM_TEX(v.uv, _MainTex);

				float3 norm = mul((float3x3)UNITY_MATRIX_IT_MV, v.normal);
				float2 offset = TransformViewToProjection(norm.xy);

				o.uv[0] = uv + _MainTex_TexelSize.xy *half2(-1, -1);
				o.uv[1] = uv + _MainTex_TexelSize.xy*half2(0, -1);
				o.uv[2] = uv + _MainTex_TexelSize.xy* half2(1, -1);
				o.uv[3] = uv + _MainTex_TexelSize.xy* half2(-1, 0);
				o.uv[4] = uv + _MainTex_TexelSize.xy* half2(0, 0);
				o.uv[5] = uv + _MainTex_TexelSize.xy* half2(1, 0);
				o.uv[6] = uv + _MainTex_TexelSize.xy* half2(-1, 1);
				o.uv[7] = uv + _MainTex_TexelSize.xy* half2(0, 1);
				o.uv[8] = uv + _MainTex_TexelSize.xy* half2(1, 1);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}

			fixed luminance(fixed4 color) {
				return  0.2125 * color.r + 0.7154 * color.g + 0.0721 * color.b;
			}

			half Sobel(v2f i) {
				//Sobe算子矩阵，分为X与Y两个方向
				const half Gx[9] = { -1,  0,  1,
										-2,  0,  2,
										-1,  0,  1 };
				const half Gy[9] = { -1, -2, -1,
										0,  0,  0,
										1,  2,  1 };

				half texColor;
				half edgeX = 0;
				half edgeY = 0;
				//累加周围采样点的梯度值
				for (int it = 0; it < 9; it++) {
					texColor = luminance(tex2D(_MainTex, i.uv[it]));
					edgeX += texColor * Gx[it];
					edgeY += texColor * Gy[it];
				}
				//用绝对值代替开根号
				half edge = 1 - abs(edgeX) - abs(edgeY);

				return edge;
			}

			fixed _EdgeOnly;
			fixed4 _EdgeColor;
			fixed4 _BackgroundColor;
			float _Brightness;

			fixed4 frag (v2f i) : SV_Target
			{
				//half edge = Sobel(i);
				//fixed4 withEdgeColor = lerp(_EdgeColor, tex2D(_MainTex, i.uv[4]), edge);
				//fixed4 onlyEdgeColor = lerp(_EdgeColor, _BackgroundColor, edge);
				//fixed4 col = lerp(withEdgeColor, onlyEdgeColor, _EdgeOnly);
				fixed4 col = tex2D(_MainTex, i.uv[4]);
				col.rbg *= _Brightness;
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
	}
}
