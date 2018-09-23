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
	private int stone = 0;
	private int metal = 0;
	
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

	//User Interface
	public Text dialogue;
	public GameObject deathTxt;
	public Text resources;

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
	public int RemoveResource(string resource, int amount) {
		if(resource == "wood") {
			if(wood >= amount) {
				wood -= amount;
                updateResourceUI();
				return 1;
			} else {
				return 0;
			}
		} else if(resource == "stone") {
			if(stone >= amount) {
				stone -= amount;
                updateResourceUI();
				return 1;
			} else {
				return 0;
			}
		} else if(resource == "metal") {
			if(metal >= amount) {
				metal -= amount;
                updateResourceUI();
				return 1;
			} else {
				return 0;
			}
		} else {
			Debug.Log("Resource Doesn't Exist!");
			return -1;
		}
	}

	//Updates Resource UI
	void updateResourceUI() {
		resources.text = "Wood: " + wood + " | Stone: " + stone + " | Metal: " + metal;
	}
}
