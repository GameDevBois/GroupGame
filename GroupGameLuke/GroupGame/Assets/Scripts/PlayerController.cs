using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float walkMod = 0.2f;

	public GameObject bodySprite;
	public GameObject legsSprite;
	public GameObject crosshair;
	public GameObject bulletPos;

	//Weapons
	//public GameObject Pistol;
	//public GameObject Axe;

	////Prefabs
	//public GameObject bullet;

	private Animator bAnim;
	private Animator lAnim;
	private Rigidbody2D rb2d;

	//Active Variables
	//bool armed = false;
	//bool hasAxe = false;

	//Statistics
	private float health;

	//

	// Use this for initialization
	void Start () {
		bAnim = bodySprite.GetComponent<Animator>();
		lAnim = legsSprite.GetComponent<Animator>();
		rb2d = gameObject.GetComponent<Rigidbody2D>();
		//Pistol.SetActive(false);
  //      Axe.SetActive(false);

		health = 100;

		Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {
		float mvX = Input.GetAxis("Horizontal");
		float mvY = Input.GetAxis("Vertical");

		if(mvX != 0 || mvY != 0) {
            //Walking in both Directions
            lAnim.SetBool("walking", true);

			//Position them legs
            float legAngle = walkDir(mvX, mvY);
            legsSprite.transform.rotation = Quaternion.AngleAxis(legAngle, legsSprite.transform.forward);
		} else {
            lAnim.SetBool("walking", false);
		}

		transform.Translate(mvX * walkMod, mvY * walkMod, 0);
        

		//MOVE CROSSHAIR
		Vector3 mousePos = Input.mousePosition;
		mousePos = Camera.main.ScreenToWorldPoint(mousePos);

		mousePos.z = 0;
		crosshair.transform.position = mousePos;

		//Set Body Look Direction
        bodySprite.transform.up = new Vector2 (
			mousePos.x - bodySprite.transform.position.x,
			mousePos.y - bodySprite.transform.position.y
		);

		// GET MOUSE INPUTS!
		//if(armed) {
			//if(Input.GetMouseButtonDown(0)) {
				//Instantiate(bullet.transform, bulletPos.transform.position, bulletPos.transform.rotation);
				//Pistol.GetComponent<Animator>().SetTrigger("fire");
			//}
		//}
		//if(hasAxe) {
		//	if(Input.GetMouseButtonDown(0)) {
		//		Axe.GetComponent<Animator>().SetTrigger("swing");
		//	}
		//}

		// GET KEYBOARD INPUTS
		//if(Input.GetKey(KeyCode.Alpha1)) {
  //          Debug.Log("Pistol");
		//	bAnim.SetBool("armed", true);
		//	armed = true;
		//	Pistol.SetActive(true);
  //          Axe.SetActive(false);
            
		//}
		//if(Input.GetKey(KeyCode.Alpha2)) {
		//	Debug.Log("My Axe!");
		//	Axe.SetActive(true);
  //          Pistol.SetActive(false);
		//	hasAxe = true;
		//}
		//if(Input.GetKey(KeyCode.Q)) {
		//	bAnim.SetBool("armed", false);
		//	armed = false;
		//	Pistol.SetActive(false);
		//}
		if(Input.GetKey(KeyCode.Escape)) {
			if(Cursor.visible == false) {
				Cursor.visible = true;
			} else {
				Cursor.visible = false;
			}
		}

	}

	float walkDir(float mvX, float mvY) {
        if (mvX == 0 && mvY > 0)
        {
            return 0;
        }
        if (mvX > 0 && mvY > 0)
        {
            return 315;
        }
        if (mvX > 0 && mvY == 0)
        {
            return 270;
        }
        if (mvX > 0 && mvY < 0)
        {
            return 225;
        }
        if (mvX == 0 && mvY < 0)
        {
            return 180;
        }
        if (mvX < 0 && mvY < 0)
        {
            return 135;
        }
        if (mvX < 0 && mvY == 0)
        {
            return 90;
        }
        if (mvX < 0 && mvY > 0)
        {
            return 45;
        }
		return -1;
	}

	public void SetSprites(bool toggle) {
		bAnim.gameObject.SetActive(toggle);
		lAnim.gameObject.SetActive(toggle);
		crosshair.SetActive(toggle);
	}

	void OnTriggerStay2D(Collider2D coll) {
		if(coll.gameObject.tag == "Tree") {
			if(Input.GetKeyDown(KeyCode.F)) {
				Destroy(coll.gameObject);
				GameManager.instance.AddResource("wood", 10);
			}
		}
	}
    
}
