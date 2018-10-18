using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure : MonoBehaviour {

	public float maxHealth = 100;
	public string structureName = "Structure";
	public string requiredResource = "none";
	public int resourceAmount = 0;

	private float currHealth;

	// Use this for initialization
	void Start () {
		currHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Damage(float damageTaken) {
		currHealth -= damageTaken;
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


}
