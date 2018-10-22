using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnEnvironment : MonoBehaviour {

    public GameObject prefab1;
    public GameObject prefab2;
    public GameObject prefab3;
    public GameObject prefab4;
    public int randomX;
    public int randomY;
    public int minTree;
    public int maxTree;
    public int minMetal;
    public int maxMetal;
    public int minStone;
    public int maxStone;
    public int minNest;
    public int maxNest;


    // Use this for initialization
    void Start () {
        for(int i = 0; i < Random.Range(minTree, maxTree); i++)
        {
            Instantiate(prefab1, new Vector3(Random.Range(-100, randomX), Random.Range(-100, randomY), 0), Quaternion.identity);
        }
        for (int i = 0; i < Random.Range(minMetal, maxMetal); i++)
        {
            Instantiate(prefab2, new Vector3(Random.Range(-100, randomX), Random.Range(-100, randomY), 0), Quaternion.identity);
        }
        for (int i = 0; i < Random.Range(minStone, maxStone); i++)
        {
            Instantiate(prefab3, new Vector3(Random.Range(-100, randomX), Random.Range(-100, randomY), 0), Quaternion.identity);
        }
        for (int i = 0; i < (Random.Range(minNest, maxNest)/2); i++)
        {
            Instantiate(prefab4, new Vector3(Random.Range(-100, -75), Random.Range(-100, 100), 0), Quaternion.identity);
        }
        for (int i = 0; i < (Random.Range(minNest, maxNest) / 2); i++)
        {
            Instantiate(prefab4, new Vector3(Random.Range(100, 75), Random.Range(-100, 100), 0), Quaternion.identity);
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
