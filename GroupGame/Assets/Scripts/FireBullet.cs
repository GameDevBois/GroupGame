using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class FireBullet : NetworkBehaviour
{

    public float timeBetweenBullets = 0.5f;

    public string weaponName = "Weapon";

    //bullet info
    public int maxrounds;
    public int startRounds;
    int remainingrounds;

    public GameObject projectile;
    public GameObject Weapon;
    public GameObject FirePos;

    float nextBullet;

    bool reloading;
    public float reloadDelay = 2;
    float reloadTime;

	// Use this for initialization
	void Start () {
        nextBullet = 0f;

        remainingrounds = startRounds;
        
	}
	
	// Update is called once per frame
	void Update () {
        if (remainingrounds > 0 && !reloading)
        {
            Fire();
            
        } else {
            GameManager.instance.Reload(true);
        }
		
        if(Input.GetKeyDown(KeyCode.R) && !reloading)
        {
            //initiate reload sequence
            reloading = true;
            reloadTime = 0;
            GameManager.instance.ReloadBegin(reloadDelay);
        }

        if(reloading) {
            if(reloadTime < reloadDelay) {
                reloadTime += Time.deltaTime;
            } else {
                remainingrounds = maxrounds;
                reloading = false;
                GameManager.instance.Reload(false);
            }
        }
    }

    public void initialiseWeapon()
    {
        nextBullet = 0;
        //ui stuff can go in here too
    }

    public string getWeaponString() {
        return weaponName + ": " + remainingrounds + "/" + maxrounds;
    }

    void Fire()
    {
        if (Input.GetMouseButton(0) && nextBullet < Time.time)
        {
            nextBullet = Time.time + timeBetweenBullets;
            Instantiate(projectile.transform, FirePos.transform.position, FirePos.transform.rotation);
            Weapon.GetComponent<Animator>().SetTrigger("fire");

            remainingrounds -= 1;
        }
    }
}
