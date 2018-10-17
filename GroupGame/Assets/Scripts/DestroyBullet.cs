using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBullet : MonoBehaviour {

    public float alivetime;


    // Use this for initialization
    private void Start()
    {
        Destroy(gameObject, alivetime);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
