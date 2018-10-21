using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{

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

	//======== BASE BUILDING ==============
	[Header("Basebuilding")]
	private bool buildMode = false;
	private bool ghostReady = false;
	private float structureAngle = 0;
	
	private int currStructure = 0;
	private WallGhost currGhost;

	public Structure[] structures;
	public WallGhost[] ghosts;

	//tesitng
	public GameObject explosion;

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

        if (!isLocalPlayer)
        {
            return;
        }
        if(health < 100)
        {
            Debug.Log(health);
        }

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
		mousePos = gameObject.GetComponentInChildren<Camera>().ScreenToWorldPoint(mousePos);

		mousePos.z = 0;
		crosshair.transform.position = mousePos;

		//Set Body Look Direction
        bodySprite.transform.up = new Vector2 (
			mousePos.x - bodySprite.transform.position.x,
			mousePos.y - bodySprite.transform.position.y
		);

		if(Input.GetKeyDown(KeyCode.Q)) {
			//Toggle Base Building
			buildMode = !buildMode;
			GameManager.instance.setBasebuildingUIActive(buildMode);
			//If in building AND ghost is not ready
			if(buildMode && !ghostReady) {
				//Instantiate the Ghost
				currGhost = Instantiate(ghosts[currStructure], FloorCoords(mousePos), bodySprite.transform.rotation);
				setBBUI();
				ghostReady = true;
			} else if(!buildMode) {
				Destroy(currGhost.gameObject);
				ghostReady = false;
			}
		}

		if(buildMode && ghostReady) {
			currGhost.transform.position = FloorCoords(mousePos);
			currGhost.transform.rotation = SetRot(bodySprite.transform.rotation);
			//Check for resources
			if(!GameManager.instance.queryResource(structures[currStructure].requiredResource, structures[currStructure].resourceAmount)) {
				currGhost.setResourceAvailable(false);
			} else {
				currGhost.setResourceAvailable(true);
			}

			if(Input.GetMouseButtonDown(1)) {
				structureAngle += 90;
			}

			if(Input.GetKeyDown(KeyCode.Space)) {
				//Check if placeable
				if(currGhost.isPlaceable()) {
					//Ask for resources
					if(GameManager.instance.RemoveResource(structures[currStructure].requiredResource, structures[currStructure].resourceAmount)) {
                        Instantiate(structures[currStructure], FloorCoords(mousePos), SetRot(bodySprite.transform.rotation));
					} else {
						Debug.Log("insufficient resources!");
					}   
				} else {
					Debug.Log("UN PLACEBAL!");
				}
			}
			if(Input.GetKeyDown(KeyCode.C)) {
				Debug.Log("Prev Structure Please");
				nextStruct(-1);
				Destroy(currGhost.gameObject);
				currGhost = Instantiate(ghosts[currStructure], FloorCoords(mousePos), bodySprite.transform.rotation);
				Debug.Log("New Structure is: " + structures[currStructure].getName());
				setBBUI();
			} else if(Input.GetKeyDown(KeyCode.V)) {
                Debug.Log("Next Structure Please");
				nextStruct(1);
                Destroy(currGhost.gameObject);
                currGhost = Instantiate(ghosts[currStructure], FloorCoords(mousePos), bodySprite.transform.rotation);
                Debug.Log("New Structure is: " + structures[currStructure].getName());
				setBBUI();
			}
		}

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

	Vector2 FloorCoords(Vector2 initPos) {
		Vector2 newPos;
		newPos.x = Mathf.Floor(initPos.x);
		newPos.y = Mathf.Floor(initPos.y);
		return newPos;
	}

	Quaternion SetRot(Quaternion initRot) {
		Vector3 newRot = initRot.eulerAngles;
		newRot.z = structureAngle;
		initRot.eulerAngles = newRot;
		return initRot;
	}

	void setBBUI() {
        int prevStruct = currStructure <= 0 ? structures.Length - 1 : currStructure - 1;
        int nextStruct = currStructure >= structures.Length - 1 ? 0 : currStructure + 1;
        GameManager.instance.setBasebuildingUI(structures[currStructure], structures[nextStruct], structures[prevStruct]);
	}

	void nextStruct(int dir) {
		if(currStructure + dir >= structures.Length) {
			//It overflows in the positive direction
			currStructure = 0;

		} else if(currStructure + dir < 0) {
			//It overflows in the negative direction
			currStructure = structures.Length - 1;
		} else {
			//It's fine
			currStructure = currStructure + dir;
		}
	}
    
}
