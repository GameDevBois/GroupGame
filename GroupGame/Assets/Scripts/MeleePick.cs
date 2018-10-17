using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleePick : MonoBehaviour {

    public float swingtimer = 1f;
    public int damage;
    public int stonedamage;
    public GameObject Weapon;

    public int stoneAmount;

    float swing;

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
        }
    }
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
}
