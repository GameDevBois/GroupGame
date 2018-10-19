using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

	private Rigidbody2D rb2d; 

    public float timer = 5f;
    public float damage = 5f;

	// Use this for initialization
	void Start () {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.up * 0.5f);
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Structure") {
            if(other.gameObject.GetComponent<Structure>().canShootOver())
                return;
        } 
        else if (other.gameObject.tag == "Zombie")
        {
            other.gameObject.GetComponent<ZombieController>().Damage(damage);
        }
        Destroy(this.gameObject);
    }
}
