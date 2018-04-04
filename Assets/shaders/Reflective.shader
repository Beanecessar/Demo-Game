// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Unlit/Blackhole"
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
				o.position = UnityObjectToViewPos( v.vertex.xyz);
				float3x3 normalMat = (float3x3)UNITY_MATRIX_IT_MV;
				o.normal = normalize(mul(normalMat, v.normal));
				o.color = v.color;
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				float3 incidentDir = normalize(i.position);
				float3 reflexDir = reflect(incidentDir,i.normal);
				fixed4 col = texCUBE(_Skybox, reflexDir);
				//col = float4(-dot(incidentDir,i.normal),0,0,1);
				return col;
			}
			ENDCG
		}
	}
}
