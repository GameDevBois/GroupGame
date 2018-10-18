using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionHandler : MonoBehaviour {

	public float maxAge = 2;
	private float age = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(age < maxAge) {
			age += Time.deltaTime;
		} else {
			Destroy(this.gameObject);
		}
	}
}
