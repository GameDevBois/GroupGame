using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadCircle : MonoBehaviour {

	private float delay;

	private bool progressing;

	private float progressTime;

	private Image circle;

	// Use this for initialization
	void Start () {
		progressing = false;
		circle = gameObject.GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		if(progressing) {
			if(progressTime < delay) {
				progressTime += Time.deltaTime;
				circle.fillAmount = progressTime / delay;
			} else {
				// Reset all the values
				progressing = false;
				progressTime = 0;
				circle.fillAmount = 0;
				this.gameObject.SetActive(false);
				delay = 0;
			}
		}
	}

	public void beginProgress(float delayTime) {
		delay = delayTime;
		progressing = true;
		progressTime = 0;
		this.gameObject.SetActive(true);
	}
}
