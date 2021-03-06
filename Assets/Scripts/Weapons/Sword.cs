using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Sword : MonoBehaviour {
	
	private bool ready = true;
	
	private Camera mainCamera;
	
	public Player player;
	
	private AudioSource swingSound;
	
	private float damage;
	
	void Start() {
		
		mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
		player = GameObject.Find("Player").GetComponent<Player>();
		swingSound = gameObject.GetComponent<AudioSource>();
		
		damage = 8.0f;
		
	}
	
	void Update() {
		
		// Check for usage
		if(Input.GetMouseButtonDown(0) && ready && !Player.paused) {
			
			// Disallow weapon from being used again
			ready = false;
			
			// Activate the weapon
			StartCoroutine("ActivateWeapon");
			
		}
		
	}
	
	
	IEnumerator ActivateWeapon() {
		
		swingSound.Play(0);
		
		// Do swinging animation
		for(int i = 1; i < 81; i++) {
			
			if(i < 40) {
				
				transform.Rotate(0, -1f, 1.75f);
				
			} else if(i > 40) {
				
				transform.Rotate(0, 1f, -1.75f);
				
			}
			
			yield return new WaitForSeconds(0.0001f);
			
		}
		
		transform.localRotation = Quaternion.Euler(20, 90, 0);
		
		ready = true;
		yield return null;
		
	}
	
	void OnTriggerEnter(Collider collider) {
		
		var enemy = collider.GetComponent<Enemy>();
		
		if (enemy != null) {
			enemy.TakeDamage(damage);
		}
		
    }
	
}
















