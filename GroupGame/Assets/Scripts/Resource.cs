using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour {

	public int resourceAmount = 100;
	public string resourceName = "Resource";

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(resourceAmount <= 0) {
			Destroy(this.gameObject);
		}
	}

	public void mine(int amount) {
		resourceAmount -= amount;
		GameManager.instance.AddResource(resourceName, amount);
	}
}
