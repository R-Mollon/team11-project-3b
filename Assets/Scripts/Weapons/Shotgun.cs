using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour {
	
	private bool ready = true;
	
	private Camera mainCamera;
	
	public float bulletSpeed;
	
	public Player player;
	
	void Start() {
		
		mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
		player = GameObject.Find("Player").GetComponent<Player>();
		
	}
	
	void Update() {
		
		// Check for usage
		if(Input.GetMouseButtonDown(0) && ready && player.shotgunLoaded > 0) {
			
			// Subtract 1 shell from shotgun
			player.shotgunLoaded--;
			
			// Disallow weapon from being used again
			ready = false;
			
			// Activate the weapon
			StartCoroutine("ActivateWeapon");
			
		}
		
	}
	
	
	IEnumerator ActivateWeapon() {
		
		// Create ten bullets
		for(int i = 0; i < 10; i++) {
			// Create bullet using the bullet prefab
			GameObject bullet = Instantiate(Resources.Load("Prefabs/Bullet")) as GameObject;
			bullet.transform.parent = GameObject.Find("Bullets").transform;
			
			// Move bullet to camera position and rotation
			bullet.transform.position = mainCamera.transform.position;
			bullet.transform.rotation = mainCamera.transform.rotation;
			
			// Add some random rotation
			bullet.transform.Rotate(Random.insideUnitSphere * 5);
			
			// Get bullet body and add velocity
			Rigidbody bulletBody = bullet.GetComponent<Rigidbody>();
			bulletBody.velocity = bullet.transform.forward * bulletSpeed;
			
			// Rotate bullet to correct orientation AFTER velocity has been added
			bullet.transform.Rotate(90, 0, 0);
		}
		
		// Do firing animation
		for(int i = 0; i < 50; i++) {
			
			if(i < 25) {
				// Rotate weapon upwards
				transform.Rotate(0, 0, 1f);
			} else {
				// Rotate back downwards
				transform.Rotate(0, 0, -1f);
			}
			
			yield return new WaitForSeconds(0.0001f);
			
		}
		
		ready = true;
		yield return null;
		
	}
	
}
