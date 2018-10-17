using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickupController : MonoBehaviour {

    public int whichWeapon;
    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            other.gameObject.GetComponentInChildren<InventoryManager>().activateWeapon(whichWeapon);
            Debug.Log("Weapon Pickup");
            Destroy(transform.gameObject);
        }
    }

}
