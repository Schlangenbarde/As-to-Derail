Shader "Custom/Blood"
{
	Properties
	{
		_MainTex("Main Texture", 2D) = "white" {}
		_OverlayTex("Overlay Texture", 2D) = "black" {}
		_Direction("Direction", Vector) = (0, 1, 0)
		_Intensity("Intensity", Range(0,1)) = 1
	}

	SubShader
	{
		Pass
		{
			CGPROGRAM
				#pragma vertex vertexFunc
				#pragma fragment fragFunc
				#include "UnityCG.cginc"

				struct v2f
				{
					float4 pos : SV_POSITION;
					float3 normal : NORMAL;
					float2 uv_Main : TEXCOORD0;
					float2 uv_Overlay : TEXCOORD1;
				};

				sampler2D _MainTex;
				float4 _MainTex_ST;
				sampler _OverlayTex;
				float4 _OverlayTex_ST;

				v2f vertexFunc(appdata_full v)
				{
					v2f o;
					o.pos = UnityObjectToClipPos(v.vertex);
					o.uv_Main = TRANSFORM_TEX(v.texcoord, _MainTex);
					o.uv_Overlay = TRANSFORM_TEX(v.texcoord, _OverlayTex);
					o.normal = mul(unity_ObjectToWorld, v.normal);
					return o;
				}

				float3 _Direction;
				fixed _Intensity;

				fixed4 fragFunc(v2f i) : COLOR
				{
					fixed dir = dot(normalize(i.normal), _Direction);

					if(dir < 1 - _Intensity)
					{
						dir = 0;
					}

					fixed4 tex1 = tex2D(_MainTex, i.uv_Main);
					fixed4 tex2 = tex2D(_OverlayTex, i.uv_Overlay);

					float4 a;
					a = lerp(tex1, tex2, dir);
					return a;
				}

			ENDCG
		}
	}
}
