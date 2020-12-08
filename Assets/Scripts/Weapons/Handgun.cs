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
	
	private Transform slider;
	
	void Start() {
		
		mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
		player = GameObject.Find("Player").GetComponent<Player>();
		reloadProgress = GameObject.Find("HUD/WeaponData/ReloadingIndicator/ReloadingBarProgress").GetComponent<RectTransform>();
		shotSound = gameObject.GetComponent<AudioSource>();
		dryShotSound = transform.GetChild(5).GetComponent<AudioSource>();
		reloadSound = transform.GetChild(1).GetComponent<AudioSource>();
		
		slider = transform.GetChild(3).transform;
		
		if(player.handgunMagazine == 0)
			slider.localPosition = new Vector3(slider.localPosition.x, slider.localPosition.y, 0.01252308f);
		
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
				transform.Rotate(-3f, 0, 0);
				
				slider.Translate(0, 0, -0.007f);
			} else {
				// Rotate back downwards
				transform.Rotate(3f, 0, 0);
				
				// If out of ammo, don't translate the slider back
				if(player.handgunMagazine > 0)
					slider.Translate(0, 0, 0.007f);
			}
			
			yield return new WaitForSecondsRealtime(0.02f);
			
		}
		
		ready = true;
		yield return null;
		
	}
	
	
	IEnumerator ReloadWeapon() {
		
		reloadSound.Play(0);
		
		for(int i = 0; i < 50; i++) {
			
			reloadProgress.sizeDelta = new Vector2(i * 2, 10);
			reloadProgress.localPosition = new Vector3(i - 50, 0, 0);
			
			yield return new WaitForSecondsRealtime(reloadTime / 50.0f);
			
		}
		
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
		
		// Move slider back to starting position
		slider.localPosition = new Vector3(slider.localPosition.x, slider.localPosition.y, 0.03002306f);
		
	}
	
}
















