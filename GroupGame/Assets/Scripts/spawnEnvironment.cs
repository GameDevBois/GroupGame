using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnEnvironment : MonoBehaviour {

    public GameObject prefab1;
    public GameObject prefab2;
    public GameObject prefab3;
    public int randomX;
    public int randomY;
    public int minTree;
    public int maxTree;
    public int minMetal;
    public int maxMetal;
    public int minStone;
    public int maxStone;


    // Use this for initialization
    void Start () {
        for(int i = 0; i < Random.Range(minTree, maxTree); i++)
        {
            Instantiate(prefab1, new Vector3(Random.Range(-50, randomX), Random.Range(-50, randomY), 0), Quaternion.identity);
        }
        for (int i = 0; i < Random.Range(minMetal, maxMetal); i++)
        {
            Instantiate(prefab2, new Vector3(Random.Range(-50, randomX), Random.Range(-50, randomY), 0), Quaternion.identity);
        }
        for (int i = 0; i < Random.Range(minStone, maxStone); i++)
        {
            Instantiate(prefab3, new Vector3(Random.Range(-50, randomX), Random.Range(-50, randomY), 0), Quaternion.identity);
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
