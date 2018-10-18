using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure : MonoBehaviour {

	public float maxHealth = 100;
	public string structureName = "Structure";
	public string requiredResource = "none";
	public int resourceAmount = 0;

	public bool shootOver = false;

	public float currHealth;
	public GameObject explosion;

	// Use this for initialization
	void Start () {
		currHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
	}

	public bool Damage(float damageTaken) {
		currHealth -= damageTaken;
		if(currHealth <= 0) {
            Instantiate(explosion, transform.position, transform.rotation);
			Destroy(this.gameObject);
            return true;
		} else {
			return false;
		}
	}

	public void Repair(float repairGiven) {
		currHealth += repairGiven;
	}

	public string getName() {
		return structureName;
	}

	public float getHealth() {
		return currHealth;
	}

	public bool canShootOver() {
		return shootOver;
	}
}
