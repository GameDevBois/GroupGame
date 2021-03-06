﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieControllerExplosion : MonoBehaviour {

	//GameObject components of zombie
	public GameObject body;
	public GameObject legs;

	//Animators of Zombie
	private Animator bAnim;
	private Animator lAnim;

	//Prefabs
	public GameObject bloodsplat;
	public GameObject explosion;

	//AI
	private Transform playerTransform;

	//Zombie Stats
	private float health;
	private float speed;

	//Zombie Variables
	private bool attack = false;
	private float attackTimer;
	public float attackCooldown = 0.5f;

	// Use this for initialization
	void Start () {
		bAnim = body.GetComponent<Animator>();
		lAnim = legs.GetComponent<Animator>();
		health = 50;

		speed = Random.Range(0.5f, 1.5f);

		attackTimer = attackCooldown; //Ready to strike Instantly
	}
	
	// Update is called once per frame
	void Update () {

		lAnim.SetBool("walking", true);

		if(health <= 0) {
			Instantiate(bloodsplat.transform, transform.position, transform.rotation);
			Instantiate(explosion, transform.position, transform.rotation);
			GameManager.instance.ZombieDeath();
			Destroy(this.gameObject);
		}

		if(attack) {
			if(attackTimer >= attackCooldown) {
				GameManager.instance.Attack(500);
                body.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0, 1);
				attackTimer = 0;
                health = 0;
			} else {
                body.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 1);
				attackTimer += Time.deltaTime;
			}
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		//We are attacking
		if(coll.gameObject.tag == "Player") {
			attack = true;
		}
        lAnim.SetBool("walking", false);
    }

	void OnCollisionExit2D(Collision2D coll) {
		//We are no longer attacking
		if(coll.gameObject.tag == "Player") {
			attack = false;
            body.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 1);
			attackTimer = attackCooldown; //Ready to strike instantly
		}
        lAnim.SetBool("walking", true);
    }

	public void Damage(float damage) {
		health -= damage;
	}
}
