using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour {

	//NPC Attributes
	public string charName = "N/A";

	//General Values
	private bool talkActive = false;

	//NPC Objects
	private Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
		rb2d = gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if(talkActive) {
			if(Input.GetKeyDown(KeyCode.F)) {
				//INTERACTION!
				GameManager.instance.SetDialogue(charName, "Hello!");
			}
		}
	}

	//If Press E
	void OnTriggerEnter2D(Collider2D other) {
        talkActive = true;
		Debug.Log("NPC is Active");
	}

	void OnTriggerExit2D(Collider2D other) {
        talkActive = false;
		Debug.Log("NPC is not active");
	}
}
