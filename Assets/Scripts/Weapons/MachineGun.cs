﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : MonoBehaviour {
	
	private bool ready = true;
	
	private Camera mainCamera;
	
	public float bulletSpeed;
	
	public Player player;
	
	private bool isReloading;
	private int maxBullets = 30;
	private float reloadTime = 4.7f;
	
	private RectTransform reloadProgress;
	private AudioSource shotSound;
	private AudioSource reloadSound;
	
	void Start() {
		
		mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
		player = GameObject.Find("Player").GetComponent<Player>();
		reloadProgress = GameObject.Find("HUD/WeaponData/ReloadingIndicator/ReloadingBarProgress").GetComponent<RectTransform>();
		shotSound = gameObject.GetComponent<AudioSource>();
		reloadSound = transform.GetChild(1).GetComponent<AudioSource>();
		
	}
	
	void Update() {
		
		// Check for usage
		if(Input.GetMouseButton(0) && ready && player.automaticMagazine > 0 && !player.reloading) {
			
			// Subtract 1 bullet from magazine
			player.automaticMagazine--;
			
			// Disallow weapon from being used again
			ready = false;
			
			// Activate the weapon
			StartCoroutine("ActivateWeapon");
			
		}
		
		// Check for reload
		if(player.reloading && !isReloading) {
			isReloading = true;
			
			StartCoroutine("ReloadWeapon");
		}
		
	}
	
	
	IEnumerator ActivateWeapon() {
		
		// Play shot sound
		shotSound.PlayOneShot(shotSound.clip, 0.75f);
		
		// Create a bullet using the bullet prefab
		GameObject bullet = Instantiate(Resources.Load("Prefabs/Bullet")) as GameObject;
		bullet.transform.parent = GameObject.Find("Bullets").transform;
		
		// Move bullet to camera position and rotation
		bullet.transform.position = mainCamera.transform.position;
		bullet.transform.rotation = mainCamera.transform.rotation;
		
		// Get bullet body and add velocity
		Rigidbody bulletBody = bullet.GetComponent<Rigidbody>();
		bulletBody.velocity = bullet.transform.forward * bulletSpeed;
		
		// Rotate bullet to correct orientation AFTER velocity has been added
		bullet.transform.Rotate(90, 0, 0);
		
		// Do firing animation
		for(int i = 0; i < 20; i++) {
			
			if(i < 10) {
				// Rotate weapon upwards
				transform.Rotate(0, 0, 1f);
			} else {
				// Rotate back downwards
				transform.Rotate(0, 0, -1f);
			}
			
			yield return new WaitForSeconds(0.00001f);
			
		}
		
		ready = true;
		yield return null;
		
	}
	
	
	IEnumerator ReloadWeapon() {
		
		reloadSound.Play(0);
		
		for(int i = 0; i < 100; i++) {
			
			reloadProgress.sizeDelta = new Vector2(i, 10);
			reloadProgress.localPosition = new Vector3((i / 2) - 50, 0, 0);
			
			yield return new WaitForSeconds(reloadTime / 100.0f);
			
		}
		
		isReloading = false;
		player.reloading = false;
		
		if(player.automaticBullets >= (maxBullets - player.automaticMagazine)) {
			// Player has enough bullets to fill magazine
			player.automaticBullets -= (maxBullets - player.automaticMagazine);
			player.automaticMagazine = maxBullets;
		} else {
			// Player does not have enough bullets to fully fill magazine
			player.automaticMagazine += player.automaticBullets;
			player.automaticBullets = 0;
		}
		
	}
	
}
