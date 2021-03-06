﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAxe : MonoBehaviour {

    public float swingtimer = 1f;
    public int damage;
    public int treedamage;
    public GameObject Weapon;

    public int woodAmount;

    float swing;

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
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Zombie")
        {
            other.gameObject.GetComponent<ZombieController>().Damage(damage);
        }
        else if (other.gameObject.tag == "Tree")
        {
            woodAmount = woodAmount + 10;
            //other.gameObject.GetComponent<TreeController>().Damage(treedamage); IF WE WANT TREES TO HAVE HEALTH
        }
    }
}
