using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Sword : MonoBehaviour {
	
	private bool ready = true;
	
	private Camera mainCamera;
	
	public Player player;
	
	private AudioSource swingSound;
	
	void Start() {
		
		mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
		player = GameObject.Find("Player").GetComponent<Player>();
		swingSound = gameObject.GetComponent<AudioSource>();
		
	}
	
	void Update() {
		
		// Check for usage
		if(Input.GetMouseButtonDown(0) && ready) {
			
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
				
				transform.Rotate(20.0f / i, -5.0f / i, 0);
				
			} else if(i > 40) {
				
				transform.Rotate(-20.0f / (i - 40), 5.0f /  (i - 40), 0);
				
			}
			
			yield return new WaitForSeconds(0.0001f);
			
		}
		
		transform.localRotation = Quaternion.Euler(0, -30, 0);
		
		ready = true;
		yield return null;
		
	}
	
}
















