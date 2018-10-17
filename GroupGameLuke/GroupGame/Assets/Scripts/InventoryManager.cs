using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour {

    public GameObject[] weapons;
    bool[] weaponAvailable;
    public Image weaponImage; // for gui

    int currentWeapon;

	// Use this for initialization
	void Start () {
        weaponAvailable = new bool[weapons.Length];
        for (int i = 0; i < weapons.Length; i++) weaponAvailable[i] = false;
        
        //weaponAvailable[currentWeapon] = true;
        for (int i = 0; i < 3; i++) weaponAvailable[i] = true;
        currentWeapon = 2;

        deactivateWeapons();
        setWeaponActive(currentWeapon);

    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentWeapon = 0;
            setWeaponActive(currentWeapon);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && weapons.Length >=2 && weaponAvailable[1] == true)
        {
            currentWeapon = 1;
            setWeaponActive(currentWeapon);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && weapons.Length >= 3 && weaponAvailable[2] == true)
        {
            currentWeapon = 2;
            setWeaponActive(currentWeapon);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && weapons.Length >= 4 && weaponAvailable[3] == true)
        {
            currentWeapon = 3;
            setWeaponActive(currentWeapon);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5) && weapons.Length >= 5 && weaponAvailable[4] == true)
        {
            currentWeapon = 4;
            setWeaponActive(currentWeapon);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6) && weapons.Length >= 6 && weaponAvailable[5] == true)
        {
            currentWeapon = 5;
            setWeaponActive(currentWeapon);
        }
    }

    public void setWeaponActive(int whichWeapon)
    {
        if (!weaponAvailable[whichWeapon]) return;
        deactivateWeapons();
        weapons[whichWeapon].SetActive(true);
        weapons[whichWeapon].GetComponentInChildren<FireBullet>().initialiseWeapon();
    }

    void deactivateWeapons()
    {
        for (int i = 0; i < weapons.Length; i++) weapons[i].SetActive(false);
    }

    public void activateWeapon(int whichWeapon)
    {
        weaponAvailable[whichWeapon] = true;
        Debug.Log("Active Weapon ");
    }
}
