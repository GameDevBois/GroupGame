using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireBullet : MonoBehaviour {

    public float timeBetweenBullets = 1f;

    //bullet info
    public int maxrounds;
    public int startRounds;
    int remainingrounds;

    public GameObject projectile;
    public GameObject Weapon;
    public GameObject FirePos;

    float nextBullet;

	// Use this for initialization
	void Start () {
        nextBullet = 0f;

        remainingrounds = startRounds;
        
	}
	
	// Update is called once per frame
	void Update () {
        if (remainingrounds !=0)
        {
            if(Input.GetMouseButton(0) && nextBullet < Time.time)
            {
               nextBullet = Time.time + timeBetweenBullets;
               Instantiate(projectile.transform, FirePos.transform.position, FirePos.transform.rotation);
               Weapon.GetComponent<Animator>().SetTrigger("fire");
            
               remainingrounds -= 1;
            }
        }
		
        if(Input.GetKeyDown(KeyCode.R))
        {
            remainingrounds = maxrounds;
        }       
    }

    public void initialiseWeapon()
    {
        nextBullet = 0;
        //ui stuff can go in here too
    }
}
