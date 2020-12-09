using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	
	private float lifetime = 0.0f;
	
	public float damage;
	
	void Update() {
		
		lifetime += Time.deltaTime;
		
		if(lifetime > 5.0f)
			Destroy(gameObject);
		
	}
	
    void OnTriggerEnter(Collider collider) {
		
		var enemy = collider.GetComponent<Enemy>();
		
		if (enemy != null) {
			enemy.TakeDamage(damage);
			
			Destroy(this.gameObject);
		} else if(collider.GetComponent<Player>() == null && collider.GetComponent<Bullet>() == null) {
			Destroy(this.gameObject);
		}
		
    }
}
