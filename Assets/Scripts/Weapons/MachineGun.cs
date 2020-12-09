using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : MonoBehaviour {
	
	private bool ready = true;
	
	private Camera mainCamera;
	
	public float bulletSpeed;
	
	public Player player;
	
	private bool isReloading;
	private int maxBullets = 30;
	private float reloadTime = 2.7f;
	
	private RectTransform reloadProgress;
	private AudioSource shotSound;
	private AudioSource dryShotSound;
	private AudioSource reloadSound;
	
	private Transform bolt;
	private Transform magazine;
	private Transform trigger;
	
	private PersistantData playerData;
	private float reloadScale;
	
	private float damage;
	private float damageScale;
	
	void Start() {
		
		mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
		player = GameObject.Find("Player").GetComponent<Player>();
		reloadProgress = GameObject.Find("HUD/WeaponData/ReloadingIndicator/ReloadingBarProgress").GetComponent<RectTransform>();
		shotSound = gameObject.GetComponent<AudioSource>();
		reloadSound = transform.GetChild(0).GetComponent<AudioSource>();
		dryShotSound = transform.GetChild(1).GetComponent<AudioSource>();
		
		bolt = transform.GetChild(2).transform;
		magazine = transform.GetChild(3).transform;
		trigger = transform.GetChild(6).transform;
		
		damage = 3.0f;
		
		reloadScale = 1.0f;
		damageScale = 1.0f;
		GameObject playerDataObject = GameObject.Find("PersistantData");
		if(playerDataObject != null) {
			playerData = playerDataObject.GetComponent<PersistantData>();
			
			reloadScale = playerData.automaticReload;
			damageScale = playerData.automaticDamage;
		}
		
	}
	
	void Update() {
		
		if(Input.GetMouseButton(0) && player.automaticMagazine == 0 && !player.reloading && !dryShotSound.isPlaying) {
			dryShotSound.Play(0);
		}
		
		// Check for usage
		if(Input.GetMouseButton(0) && ready && player.automaticMagazine > 0 && !player.reloading) {
			
			// Subtract 1 bullet from magazine
			player.automaticMagazine--;
			
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
		shotSound.PlayOneShot(shotSound.clip, 0.75f);
		
		// Create a bullet using the bullet prefab
		GameObject bullet = Instantiate(Resources.Load("Prefabs/Bullet")) as GameObject;
		bullet.transform.parent = GameObject.Find("Bullets").transform;
		
		// Set damage value of bullet
		bullet.GetComponent<Bullet>().damage = damage * damageScale;
		
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
				transform.Rotate(0.5f, 0, 0);
				
				// Move bolt
				bolt.Translate(0, 0, 0.005f);
				
				// Move trigger
				trigger.Translate(0, 0, 0.0015f);
			} else {
				// Rotate back downwards
				transform.Rotate(-0.5f, 0, 0);
				
				// Move bolt
				bolt.Translate(0, 0, -0.005f);
				
				// Move trigger
				trigger.Translate(0, 0, -0.0015f);
			}
			
			yield return new WaitForSecondsRealtime(0.01f);
			
		}
		
		// Reset everythings position/rotation to make sure we dont drift
		transform.localRotation = Quaternion.Euler(0, 180, 0);
		bolt.localPosition = new Vector3(0, 0.07780132f, 0.1563036f);
		trigger.localPosition = new Vector3(0, 0.01301076f, 0.09446944f);
		
		player.firing = false;
		ready = true;
		yield return null;
		
	}
	
	
	IEnumerator ReloadWeapon() {
		
		reloadSound.pitch = reloadScale;
		reloadSound.Play(0);
		
		for(int i = 0; i < 50; i++) {
			
			reloadProgress.sizeDelta = new Vector2(i * 2, 10);
			reloadProgress.localPosition = new Vector3(i - 50, 0, 0);
			
			// Reload animation
			if(i < 4) {
				// Tilt gun
				transform.Rotate(5.0f, 0, 5.0f);
			} else if(i > 10 && i < 15) {
				// Remove magazine
				magazine.Translate(0, -0.18f, 0.015f);
			} else if(i > 35 && i < 40) {
				// Insert magazine
				magazine.Translate(0, 0.18f, -0.015f);
			} else if(i > 45) {
				// Tilt gun back
				transform.Rotate(-5.0f, 0, -5.0f);
			}
			
			yield return new WaitForSecondsRealtime(reloadTime / (50.0f * reloadScale));
			
		}
		
		// Reset everythings position/rotation to make sure we dont drift
		transform.localRotation = Quaternion.Euler(0, 180, 0);
		magazine.localPosition = new Vector3(0, 0, 0);
		
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
