using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed = 10;
    public float rotateSpeed = 800;

    Vector3 movementDir;
    Animator animator;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        //movement
        movementDir = new Vector3(0, 0, 0);

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

        if(movementDir.magnitude > 0.01)
        {
            transform.Translate(movementDir.normalized * moveSpeed * Time.deltaTime, Space.World);

            bool isClockwise = Vector3.Cross(movementDir.normalized, transform.forward.normalized).y < 0;
            float rotateAngle = Mathf.Acos(Vector3.Dot(movementDir.normalized, transform.forward.normalized));
            //float rotateAngle = -Vector3.Dot(movementDir.normalized, transform.forward.normalized) + 1;
            if (isClockwise)
                transform.Rotate(0, rotateAngle * rotateSpeed * Time.deltaTime, 0, Space.World);
            else
                transform.Rotate(0, -rotateAngle * rotateSpeed * Time.deltaTime, 0, Space.World);
        }

        //action
        if (Input.GetKeyDown(KeyCode.J))
        {
            animator.SetTrigger("attack");
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            animator.SetTrigger("jump");
        }
    }
}
