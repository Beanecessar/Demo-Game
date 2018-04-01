using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyZombie : MonoBehaviour {

    public float guardingRange = 50;
    public float attackRange = 1;
    public float targetBindingRange = 0.5F;
    public float moveSpeed = 4;
	public float rotateSpeed = 800;

	GameObject target;
	// Use this for initialization
	void Start () {
		target = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
        float targetDistance = (transform.position - target.transform.position).magnitude;
		Vector3 movementDirection = (target.transform.position - transform.position).normalized;

        if (targetDistance < guardingRange)
        //Face to target
        {
            float rotateAngle = Mathf.Acos(Vector3.Dot(movementDirection.normalized, transform.forward.normalized));
            if (Vector3.Cross(movementDirection.normalized, transform.forward.normalized).y < 0)
                transform.Rotate(0, rotateAngle * rotateSpeed * Time.deltaTime, 0, Space.World);
            else
                transform.Rotate(0, -rotateAngle * rotateSpeed * Time.deltaTime, 0, Space.World);

            if (targetDistance > attackRange + targetBindingRange)
            //Move to target
            {
                transform.Translate(movementDirection * moveSpeed * Time.deltaTime, Space.World);
				Debug.Log (movementDirection + " " + Time.deltaTime);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MeleeWeapon" && other.GetComponentInParent<PlayerController>().isAttacking)
		{
            //Debug.Log(name + " damaged.");
			//Destroy(this.gameObject);
			gameObject.SetActive(false);
		}
    }
}
