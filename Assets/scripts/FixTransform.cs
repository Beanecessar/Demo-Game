using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixTransform : MonoBehaviour {

	Vector3 position;
	Quaternion rotation;
	// Use this for initialization
	void Start () {
		position = transform.localPosition;
		rotation = transform.localRotation;
	}
	
	// Update is called once per frame
	void Update () {
		transform.localPosition = position;
		transform.localRotation = rotation;
	}
}
