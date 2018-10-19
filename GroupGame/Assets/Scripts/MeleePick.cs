using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleePick : MonoBehaviour {

    public float swingtimer = 0.5f;
    public int damage;
    public int stonedamage;
    public GameObject Weapon;

    public int stoneAmount;

    float swing;

    //Gathering Resources
    private bool resourceReady = false;
    private GameObject resource;

    // Use this for initialization
    void Start()
    {
        swing = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && swing < Time.time)
        {
            swing = Time.time + swingtimer;
            Weapon.GetComponent<Animator>().SetTrigger("swing");

            if(resourceReady) {
                Debug.Log("Stone Time!");
                resource.GetComponent<Resource>().mine(10);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Stone")
        {
            Debug.Log("Hit a Stone!");
            resourceReady = true;
            resource = other.gameObject;
        } else if(other.gameObject.tag == "Metal") {
            resourceReady = true;
            resource = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (GameObject.ReferenceEquals(other.gameObject, resource))
        {
            resourceReady = false;
            resource = null;
        }
    }

    /*
    void OnTriggerEnter2D(Collider2D other)
    {
        if(Input.GetMouseButtonDown(0) && swing < Time.time) {
            if (other.gameObject.tag == "Zombie")
            {
                other.gameObject.GetComponent<ZombieController>().Damage(damage);
            }
            else if (other.gameObject.tag == "Stone") // if the tag for stone is different please change
            {
                GameManager.instance.AddResource("stone", 10);
                other.gameObject.GetComponent<Resource>().mine(10);
                //other.gameObject.GetComponent<StoneController>().Damage(stonedamage); IF WE WANT ROCKS TO HAVE HEALTH
            }
        }
        
    }
    */
}
