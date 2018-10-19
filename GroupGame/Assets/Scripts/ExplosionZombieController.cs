using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionZombieController : ZombieController {

	[Header("Explosion Specific")]
	public GameObject explosion;

	// Use this for initialization
	/*
	void Start () {
		
	}
	*/
	
	// Update is called once per frame
	/*
	void Update () {
		
	}
	*/

	public override void Death() {
		Debug.Log("Defeated Explosion Zombie");
		Instantiate(bloodsplat, transform.position, transform.rotation);
		Instantiate(explosion, transform.position, transform.rotation);
		GameManager.instance.SpecialDeath();
		Destroy(this.gameObject);
	}

	public override void Attack(GameObject attackTarget) {
		if(attackTarget.tag == "Player") {
			GameManager.instance.Attack(damage);
			Instantiate(explosion, transform.position, transform.rotation);
			GameManager.instance.SpecialDeath();
			Destroy(this.gameObject);
		}
	}
}
