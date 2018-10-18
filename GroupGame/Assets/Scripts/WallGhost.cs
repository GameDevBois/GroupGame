using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGhost : MonoBehaviour {

	public bool sufficientSpace = true;
	private bool sufficientResources = false;

	private SpriteRenderer spriteRenderer;

	private Color colorGreen;
	private Color colorRed;

	// Use this for initialization
	void Start () {
		spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        colorGreen = new Color(0.46f, 1f, 0.54f, 0.76f);
        colorRed = new Color(0.89f, 0.41f, 0.26f, 0.76f);
		spriteRenderer.color = colorGreen;
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if(coll.gameObject.tag != "decoration") {
            setSpaceAvailable(false);
            Debug.Log("Entered");
		}
	}

	void OnTriggerExit2D(Collider2D coll) {
		setSpaceAvailable(true);
        Debug.Log("Left");
	}

	public void setResourceAvailable(bool hasResources) {
		sufficientResources = hasResources;
		isPlaceable();
	}

	public void setSpaceAvailable(bool hasSpace) {
		sufficientSpace = hasSpace;
		isPlaceable();
	}

	public bool isPlaceable() {
		if(sufficientResources && sufficientSpace) {
			spriteRenderer.color = new Color(0.46f, 1f, 0.54f, 0.76f);
			return true;
		} else {
			spriteRenderer.color = new Color(0.89f, 0.41f, 0.26f, 0.76f);
			return false;
		}
	}
}
