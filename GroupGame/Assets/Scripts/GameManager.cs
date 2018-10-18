using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;

	//Global Statistics
	private float playerHealth;
	public float maxHealth = 100;
	private bool playerDead = false;
	
	//Resources
	public int wood = 0;
	public int stone = 0;
	public int metal = 0;
	
	//Zombie Stats
	public int maxZombies = 0;
	private int numZombies = 6;

	//Player
	private GameObject player;

    //Prefabs
    public GameObject zombiePrefab;
	public GameObject bloodSplat;

	//Spawners
	private GameObject[] spawners;

	//Current Weapon Information
	private GameObject currentWeapon;

	//User Interface
	public Text dialogue;
	public GameObject deathTxt;
	public Text resources;
	public GameObject reloadText;
	public ReloadCircle reloadCircle;
	public Text weaponInfo;

	// Use this for initialization
	void Awake () {
		if(instance == null) {
			instance = this;
		} else {
			Destroy(this.gameObject);
		}

		DontDestroyOnLoad(this.gameObject);

		playerHealth = maxHealth;

	}

	void Start() {
		player = GameObject.FindGameObjectWithTag("Player");
		spawners = GameObject.FindGameObjectsWithTag("Spawner");
	}
	
	// Update is called once per frame
	void Update () {
		if(numZombies < maxZombies) {
			SpawnZombie();
		}

		if(playerHealth <= 0 && playerDead == false) {
			//He ded
			deathTxt.SetActive(true);
			player.GetComponent<PlayerController>().SetSprites(false);
			player.GetComponent<PlayerController>().enabled = false;
			player.GetComponent<CircleCollider2D>().enabled = false;
			Instantiate(bloodSplat.transform, player.transform.position, player.transform.rotation);
			playerDead = true;
		}

		if(currentWeapon == null) {
			//Either no weapons or axe/pickaxe

		} else {
			if(currentWeapon.GetComponentInChildren<FireBullet>()) {
				weaponInfo.text = currentWeapon.GetComponentInChildren<FireBullet>().getWeaponString();
			} else if(currentWeapon.GetComponent<MeleeAxe>()) {
				weaponInfo.text = "Axe";
			} else if(currentWeapon.GetComponent<MeleePick>()) {
				weaponInfo.text = "Pickaxe";
			} else {
				weaponInfo.text = "Unknown Weapon";
			}
		}
		
	}

	public void ZombieDeath() {
		numZombies--;
		if(numZombies < maxZombies ) {
			SpawnZombie();
		}
	}

	void SpawnZombie() {
		int i = Random.Range(0, spawners.Length - 1);
		Instantiate(zombiePrefab.transform, spawners[i].transform.position, spawners[i].transform.rotation);
		numZombies++;
	}

	public void SetDialogue(string person, string content) {
		dialogue.text = person + ": " + content;
	}

	public void Attack(float damage) {
		if(playerHealth > 0) {
            playerHealth -= damage;
            Debug.Log("Player took: " + damage + ", Health is now " + playerHealth);
		}
	}

	/* Resource Related Functions */

	//Add resource -- for collecting resources
	public void AddResource(string resource, int amount) {
		if(resource == "wood") {
			wood += amount;
		} else if(resource == "stone") {
			stone += amount;
		} else if(resource == "metal") {
			metal += amount;
		} else {
			Debug.Log("Resource Doesn't Exist!");
		}
		Debug.Log("Added " + amount + " " + resource);
		updateResourceUI();
	}

	//Remove Resources -- Used for construction
	// Returns 1 if removal was success
	// Returns 0 if not enough resources
	// Returns -1 if resource doesn't exist/other error
	public bool RemoveResource(string resource, int amount) {
		if(resource == "wood") {
			if(wood >= amount) {
				wood -= amount;
                updateResourceUI();
				return true;
			} else {
				return false;
			}
		} else if(resource == "stone") {
			if(stone >= amount) {
				stone -= amount;
                updateResourceUI();
				return true;
			} else {
				return false;
			}
		} else if(resource == "metal") {
			if(metal >= amount) {
				metal -= amount;
                updateResourceUI();
				return true;
			} else {
				return false;
			}
		} else {
			Debug.Log("Resource Doesn't Exist!");
			return false;
		}
	}

	//Updates Resource UI
	void updateResourceUI() {
		resources.text = "Wood: " + wood + " | Stone: " + stone + " | Metal: " + metal;
	}

	public void Reload(bool show) {
		reloadText.SetActive(show);
	}

	public void ReloadBegin(float delay) {
        reloadCircle.beginProgress(delay);
	}

	public void setCurrWeapon(GameObject weapon) {
		Debug.Log("Current Weapon!");
		currentWeapon = weapon;
	}

	public bool queryResource(string resourceName, int amount) {
		if(resourceName == "wood") {
			//Check if we have enough wood
			if(wood >= amount) {
				//Remove the wood, return true
				return true;
			} else {
				return false;
			}
		} else if(resourceName == "stone") {
			if(stone >= amount) {
				return true;
			} else {
				return false;
			}
		} else if(resourceName == "metal") {
			if(metal >= amount) {
				return true;
			} else {
				return false;
			}
		} else {
			Debug.Log("Resource Not Found");
			return false;
		}
	}
}
