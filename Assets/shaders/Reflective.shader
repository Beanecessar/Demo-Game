Shader "Unlit/Reflective"
{
	Properties
	{
		_Skybox ("Cubemap", Cube) = "white" {}
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
				float4 color : COLOR;
			};

			struct v2f
			{
				float3 normal : NORMAL;
				float3 position : TEXCOORD0;
				float4 vertex : SV_POSITION;
				float4 color : COLOR;
			};

			samplerCUBE _Skybox;
			float4 _MainTex_ST;
			float4 _CameraPosition;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				//Calculate in world space
				//o.position = mul((float3x3)unity_ObjectToWorld, v.vertex.xyz);
				//o.normal = UnityObjectToWorldNormal(v.normal);
				//Calculate in view space
				o.position = UnityObjectToViewPos( v.vertex.xyz);
				o.normal = normalize(mul((float3x3)UNITY_MATRIX_IT_MV, v.normal));

				o.color = v.color;
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				float3 incidentDir = normalize(i.position);
				//float3 incidentDir = normalize(i.position - _WorldSpaceCameraPos);
				float3 reflexDir = reflect(incidentDir,i.normal);
				fixed4 col = texCUBE(_Skybox, reflexDir);
				//col = float4(incidentDir,1);
				//col = float4(i.normal,1);
				//col = float4(reflexDir,1);
				return col;
			}
			ENDCG
		}
	}
}
