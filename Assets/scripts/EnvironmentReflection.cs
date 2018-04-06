using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentReflection : MonoBehaviour {

	Cubemap environmentMap;
	GameObject cameraObj;
	Camera perspectiveCam;
	Material reflectiveMaterial;

	// Use this for initialization
	void Start () {
		cameraObj = new GameObject ("ReflectCamera");
		perspectiveCam = cameraObj.AddComponent<Camera> ();
		cameraObj.transform.SetParent(transform);
		cameraObj.transform.position = transform.position;
		cameraObj.transform.rotation = Quaternion.identity;

        environmentMap = new Cubemap(256, TextureFormat.ARGB32, true);

		//perspectiveCam.targetDisplay = -1;
		perspectiveCam.depth = -1;
		perspectiveCam.RenderToCubemap(environmentMap, 0x3F);

		foreach (Material material in GetComponent<Renderer>().materials)
		{
			if (material.HasProperty("_Skybox"))
			{
				reflectiveMaterial = material;
			}
		}
    }

    private void OnWillRenderObject()
    {
		
    }

    // Update is called once per frame
    void Update () {
		//TODO: Calculate and cull back face
		perspectiveCam.RenderToCubemap(environmentMap, 0x3F);
		reflectiveMaterial.SetTexture ("_Skybox", environmentMap);
    }
}
