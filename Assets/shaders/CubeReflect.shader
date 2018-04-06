Shader "Unlit/CubeReflect"
{
	Properties
	{
		_Skybox ("Cubemap", Cube) = "white" {}
		_ReflectCoefficent ("ReflectCoefficent", Range(0,1)) = 1
		_BaseColor ("BaseColor", Color) = (1, 1, 1, 1)
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 200

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
			};

			struct v2f
			{
				float3 reflexDir : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			samplerCUBE _Skybox;
			float4 _MainTex_ST;
			float4 _CameraPosition;
			float _ReflectCoefficent;
			float4 _BaseColor;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				float3 incidentDir = -normalize(WorldSpaceViewDir(v.vertex));
				float3 normal = UnityObjectToWorldNormal(v.normal);
				o.reflexDir = reflect(incidentDir,normal);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 reflectColor = texCUBE(_Skybox, i.reflexDir);
				fixed4 output = lerp(_BaseColor,reflectColor,_ReflectCoefficent);
				return output;
			}
			ENDCG
		}
	}
}
