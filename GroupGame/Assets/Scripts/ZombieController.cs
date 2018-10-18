using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour {

	//GameObject components of zombie
	public GameObject body;
	public GameObject legs;

	//Animators of Zombie
	private Animator bAnim;
	private Animator lAnim;

	//Prefabs
	public GameObject bloodsplat;

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
		playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
		health = 10;

		speed = Random.Range(1.0f, 2.5f);

		attackTimer = attackCooldown; //Ready to strike Instantly
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);

		Vector2 direction = playerTransform.position - transform.position;
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle - 90, transform.forward);

		lAnim.SetBool("walking", true);

		if(health <= 0) {
			Instantiate(bloodsplat, transform.position, transform.rotation);
			GameManager.instance.ZombieDeath();
			Destroy(this.gameObject);
		}

		if(attack) {
			if(attackTimer >= attackCooldown) {
				GameManager.instance.Attack(10);
                body.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0, 1);
				attackTimer = 0;
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
	}

	void OnCollisionExit2D(Collision2D coll) {
		//We are no longer attacking
		if(coll.gameObject.tag == "Player") {
			attack = false;
            body.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 1);
			attackTimer = attackCooldown; //Ready to strike instantly
		}
	}

	public void Damage(float damage) {
		health -= damage;
	}
}
