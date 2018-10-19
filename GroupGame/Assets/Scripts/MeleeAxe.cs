using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAxe : MonoBehaviour {

    public float swingtimer = 0.5f;
    public int damage;
    public int treedamage;
    public GameObject Weapon;

    public int woodAmount;

    float swing;

    //Gathering Resources
    private bool resourceReady = false;
    private GameObject resource;

    // Use this for initialization
    void Start () {
        swing = 0f;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0) && swing < Time.time)
        {
            swing = Time.time + swingtimer;
            Weapon.GetComponent<Animator>().SetTrigger("swing");

            if(resourceReady) {
                Debug.Log("Mining Time");
                resource.GetComponent<Resource>().mine(10);
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Tree") {
            resourceReady = true;
            resource = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if(GameObject.ReferenceEquals(other.gameObject, resource)) {
            resourceReady = false;
            resource = null;
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        //Debug.Log("Treegerinho");
        if(Input.GetMouseButtonDown(0) && swing < Time.time) {
            if (other.gameObject.tag == "Zombie")
            {
                other.gameObject.GetComponent<ZombieController>().Damage(damage);
            }
            /*
            else if (other.gameObject.tag == "Tree")
            {
                GameManager.instance.AddResource("wood", 10);
                Debug.Log("Hit a Tree");
                other.gameObject.GetComponent<Resource>().mine(10);
                //other.gameObject.GetComponent<TreeController>().Damage(treedamage); IF WE WANT TREES TO HAVE HEALTH
            }
            else if (other.gameObject.tag == "Junk")
            {
                GameManager.instance.AddResource("metal", 10);
            }
            */
        }
        
    }
}
