using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour {

	//GameObject components of zombie
	public GameObject body;
	public GameObject legs;

	//Zombie Variables
	public int damage = 10;
	public bool special = false;

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

	//Target of attack
	private GameObject target;

	// Use this for initialization
	void Start () {
		bAnim = body.GetComponent<Animator>();
		lAnim = legs.GetComponent<Animator>();
		health = 10;

		speed = Random.Range(1.0f, 2.5f);

		attackTimer = attackCooldown; //Ready to strike Instantly
	}
	
	// Update is called once per frame
	void Update () {

		lAnim.SetBool("walking", true);

		if(attack) {
			if(attackTimer >= attackCooldown) {
				Attack(target);
				attackTimer = 0;
			} else {
                body.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 1);
				attackTimer += Time.deltaTime;
			}
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		//We are attacking
		if(coll.gameObject.tag == "Player" || coll.gameObject.tag == "Structure") {
			attack = true;
			target = coll.gameObject;
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
        if (health <= 0)
        {
            Death();
        }
	}

	public virtual void Death() {
        Debug.Log("I am ded");
        Instantiate(bloodsplat, transform.position, transform.rotation);
		if(special) {
			GameManager.instance.SpecialDeath();
		} else {
            GameManager.instance.ZombieDeath();
		}
        Destroy(this.gameObject);
	}

	public virtual void Attack(GameObject attackTarget) {
        if (attackTarget.tag == "Player")
        {
            GameManager.instance.Attack(damage);
        }
        else if (attackTarget.tag == "Structure")
        {
            attackTarget.GetComponent<Structure>().Damage(damage);        //Damage returns true if destroyed
        }
        body.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0, 1);
	}
}
