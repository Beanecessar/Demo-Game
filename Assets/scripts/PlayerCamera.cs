using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {

	public GameObject followedPlayer;
	public float angleOfDepression = 45;
	public Vector3 perspectPos = new Vector3 (0, 20, -20);

	// Use this for initialization
	void Start () {
		Quaternion selfRotation = new Quaternion ();
		selfRotation.eulerAngles = new Vector3 (angleOfDepression, 0, 0);
		transform.rotation = selfRotation;
		transform.position = followedPlayer.transform.position + perspectPos;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = followedPlayer.transform.position + perspectPos;
	}
}
