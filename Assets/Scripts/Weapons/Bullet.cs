using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	
	private float lifetime = 0.0f;
	
	void Update() {
		
		lifetime += Time.deltaTime;
		
		if(lifetime > 5.0f)
			Destroy(gameObject);
		
	}
   
	void OnTriggerEnter(Collider other) {
	 
		//Destroy(gameObject);
	 
	}
   void OnCollisionEnter(Collision collision)
    {
		var enemy = collision.collider.GetComponent<Enemy>();
		if (enemy != null)
			enemy.TakeDamage();
    }
}
