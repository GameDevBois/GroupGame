using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

	private Rigidbody2D rb2d;

	private float death = 5;
	private float age = 0;

	// Use this for initialization
	void Start () {
		rb2d = gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.up);
		if(age < death) {
			age += Time.deltaTime;
		} else {
			Destroy(this.gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.tag == "Zombie") {
			other.gameObject.GetComponent<ZombieController>().Damage(20);
		}
		Destroy(this.gameObject);
	}
}
