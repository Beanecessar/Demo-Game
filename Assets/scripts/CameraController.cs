using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject followedPlayer;
	public float angleOfDepression = 45;
	public Vector3 perspectPos = new Vector3 (0, 20, -20);
	public bool isEditMode = false;

	// Use this for initialization
	void Start () {
		Quaternion selfRotation = new Quaternion ();
		selfRotation.eulerAngles = new Vector3 (angleOfDepression, 0, 0);
		transform.rotation = selfRotation;
		transform.position = followedPlayer.transform.position + perspectPos;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.E)) 
		{
			isEditMode = !isEditMode;
			Quaternion selfRotation = new Quaternion ();
			selfRotation.eulerAngles = new Vector3 (angleOfDepression, 0, 0);
			transform.rotation = selfRotation;
		}
		if (isEditMode) 
		{
			Vector3 movementDir = new Vector3(0, 0, 0);

			if (Input.GetKey(KeyCode.W))
			{
				movementDir += Vector3.forward;
			}

			if (Input.GetKey(KeyCode.A))
			{
				movementDir += Vector3.left;
			}

			if (Input.GetKey(KeyCode.S))
			{
				movementDir += Vector3.back;
			}

			if (Input.GetKey(KeyCode.D))
			{
				movementDir += Vector3.right;
			}

			if (Input.GetKey(KeyCode.Space))
			{
				movementDir += Vector3.up;
			}

			if (Input.GetKey(KeyCode.C))
			{
				movementDir += Vector3.down;
			}

			transform.position += movementDir.normalized;

			Vector3 viewRotation = new Vector3 (0, 0, 0);

			if (Input.GetKey(KeyCode.UpArrow))
			{
				viewRotation.x -= 1;
			}

			if (Input.GetKey(KeyCode.DownArrow))
			{
				viewRotation.x += 1;
			}

			if (Input.GetKey (KeyCode.LeftArrow)) 
			{
				viewRotation.y -= 1;
			}

			if (Input.GetKey(KeyCode.RightArrow))
			{
				viewRotation.y += 1;
			}

			transform.Rotate (viewRotation * 2);
		} 
		else 
		{
			transform.position = followedPlayer.transform.position + perspectPos;
		}
	}
}
