using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handgun : MonoBehaviour {
	
	private bool ready = true;
	
	private Camera mainCamera;
	
	public float bulletSpeed;
	
	public Player player;
	
	
	private bool isReloading;
	private int maxBullets = 8;
	private float reloadTime = 0.8f;
	
	private RectTransform reloadProgress;
	private AudioSource shotSound;
	private AudioSource dryShotSound;
	private AudioSource reloadSound;
	
	private Transform magazine;
	private Transform slider;
	
	void Start() {
		
		mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
		player = GameObject.Find("Player").GetComponent<Player>();
		reloadProgress = GameObject.Find("HUD/WeaponData/ReloadingIndicator/ReloadingBarProgress").GetComponent<RectTransform>();
		shotSound = gameObject.GetComponent<AudioSource>();
		
		reloadSound = transform.GetChild(0).GetComponent<AudioSource>();
		dryShotSound = transform.GetChild(1).GetComponent<AudioSource>();
		
		magazine = transform.GetChild(3).transform;
		slider = transform.GetChild(4).transform;
		
		if(player.handgunMagazine == 0)
			slider.localPosition = new Vector3(slider.localPosition.x, slider.localPosition.y, -0.1212297f);
		
	}
	
	void Update() {

		if(Input.GetMouseButtonDown(0) && player.handgunMagazine == 0 && !player.reloading && !dryShotSound.isPlaying) {
			dryShotSound.Play(0);
		}

		// Check for usage
		if(Input.GetMouseButtonDown(0) && ready && player.handgunMagazine > 0 && !player.reloading) {
			
			// Subtract 1 bullet from magazine
			player.handgunMagazine--;
			
			// Disallow weapon from being used again
			ready = false;
			player.firing = true;
			
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
		shotSound.PlayOneShot(shotSound.clip, 1);
		
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
		for(int i = 0; i < 10; i++) {
			
			if(i < 5) {
				// Rotate weapon upwards
				transform.Rotate(3f, 0, 0);
				
				slider.Translate(0, 0, 0.007f);
			} else {
				// Rotate back downwards
				transform.Rotate(-3f, 0, 0);
				
				// If out of ammo, don't translate the slider back
				if(player.handgunMagazine > 0)
					slider.Translate(0, 0, -0.007f);
			}
			
			yield return new WaitForSecondsRealtime(0.02f);
			
		}
		
		// Reset everythings position/rotation to make sure we dont drift
		transform.localRotation = Quaternion.Euler(0, 180, 0);
		if(player.handgunMagazine > 0)
			slider.localPosition = new Vector3(0, 0.008086923f, -0.1387274f);
		
		player.firing = false;
		ready = true;
		yield return null;
		
	}
	
	
	IEnumerator ReloadWeapon() {
		
		reloadSound.Play(0);
		
		for(int i = 0; i < 50; i++) {
			
			reloadProgress.sizeDelta = new Vector2(i * 2, 10);
			reloadProgress.localPosition = new Vector3(i - 50, 0, 0);
			
			
			// Reload animation
			if(i < 10) {
				// Tilt gun
				transform.Rotate(3.0f, 0, 3.0f);
			} else if(i < 20) {
				// Remove magazine
				magazine.Translate(0, -0.055f, 0.007f);
			} else if(i > 29 && i < 40) {
				// Insert magazine
				magazine.Translate(0, 0.055f, -0.007f);
			} else if(i > 40){
				// Tilt gun back
				transform.Rotate(-3.0f, 0, -3.0f);
			}
			
			
			yield return new WaitForSecondsRealtime(reloadTime / 50.0f);
			
		}
		
		// Reset rotation
		transform.localRotation = Quaternion.Euler(0, 180f, 0);
		magazine.localRotation = Quaternion.Euler(-20.68f, 0, 0);
		
		// Reset magazine position
		magazine.localPosition = new Vector3(0, -0.05186473f, 0.04769017f);
		
		
		
		isReloading = false;
		player.reloading = false;
		
		if(player.handgunBullets >= (maxBullets - player.handgunMagazine)) {
			// Player has enough bullets to fill magazine
			player.handgunBullets -= (maxBullets - player.handgunMagazine);
			player.handgunMagazine = maxBullets;
		} else {
			// Player does not have enough bullets to fully fill magazine
			player.handgunMagazine += player.handgunBullets;
			player.handgunBullets = 0;
		}
		
		// Reset slider back to starting position
		slider.localPosition = new Vector3(0, 0.008086923f, -0.1387274f);
		
	}
	
}
















