using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;

	//Global Statistics
	private float playerHealth;
	public float maxHealth = 100;
	
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

		if(playerHealth <= 0) {
			//He ded
			deathTxt.SetActive(true);
			player.GetComponent<PlayerController>().SetSprites(false);
			player.GetComponent<PlayerController>().enabled = false;
			player.GetComponent<CircleCollider2D>().enabled = false;
			Instantiate(bloodSplat.transform, player.transform.position, player.transform.rotation);
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
}
