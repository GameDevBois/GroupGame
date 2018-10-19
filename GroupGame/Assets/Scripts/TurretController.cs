using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour {

	public GameObject turretHead;
	public GameObject bulletPos;
	
	[Header("Prefabs")]
	public BulletController bullet;

	//Private
	private GameObject target;
	private float fireCooldown;

	// Use this for initialization
	void Start () {
		target = null;
	}
	
	// Update is called once per frame
	void Update () {
		if(target != null) {
			//We have a target!

			//Check fire cooldown
			if(fireCooldown <= Time.time) {
                //Look at the target
                Vector3 direction = target.transform.position - this.transform.position;
                direction.z = 0;
                //float angleOfView = Vector2.Angle(direction, this.transform.forward);
                turretHead.transform.up = new Vector2(
                    target.transform.position.x - turretHead.transform.position.x,
                    target.transform.position.y - turretHead.transform.position.y
                );

                Instantiate(bullet, bulletPos.transform.position, bulletPos.transform.rotation);
				fireCooldown = Time.time + 0.2f;
			}
			
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		//Something has entered our FIELD OF VIEW
		if(other.gameObject.tag == "Zombie") {
			target = other.gameObject;
		}
		//Else don't worry
	}

	void OnTriggerExit2D(Collider2D other) {
		if(GameObject.ReferenceEquals(other.gameObject, target)) {
			//If our target has left.
			target = null;
		}
	}
}
