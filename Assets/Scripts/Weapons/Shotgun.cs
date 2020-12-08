using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour {
	
	private bool ready = true;
	
	private Camera mainCamera;
	
	public float bulletSpeed;
	
	public Player player;
	
	private bool isReloading;
	private int maxShells = 6;
	private float reloadTime = 4.8f;
	
	private RectTransform reloadProgress;
	private AudioSource shotSound;
	private AudioSource reloadSound;
	private AudioSource dryShotSound;
	
	private Transform pump;
	private Transform trigger;
	
	void Start() {
		
		mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
		player = GameObject.Find("Player").GetComponent<Player>();
		reloadProgress = GameObject.Find("HUD/WeaponData/ReloadingIndicator/ReloadingBarProgress").GetComponent<RectTransform>();
		shotSound = gameObject.GetComponent<AudioSource>();
		reloadSound = transform.GetChild(0).GetComponent<AudioSource>();
		dryShotSound = transform.GetChild(1).GetComponent<AudioSource>();
		
		pump = transform.GetChild(2).transform;
		trigger = transform.GetChild(3).transform;
		
	}
	
	void Update() {
		
		if(Input.GetMouseButtonDown(0) && player.shotgunLoaded == 0 && !player.reloading && !dryShotSound.isPlaying) {
			dryShotSound.Play(0);
		}
		
		// Check for usage
		if(Input.GetMouseButtonDown(0) && ready && player.shotgunLoaded > 0 && !player.reloading) {
			
			// Subtract 1 shell from shotgun
			player.shotgunLoaded--;
				
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
		for(int i = 0; i < 75; i++) {
			
			if(i < 10) {
				// Rotate weapon upwards
				transform.Rotate(2f, 0, 0);
				
				// Move trigger
				trigger.Translate(0, 0, 0.0015f);
			} else if(i < 20) {
				// Rotate back downwards
				transform.Rotate(-2f, 0, 0);
				
				// Move trigger
				trigger.Translate(0, 0, -0.0015f);
			}
			
			if(i > 40 && i < 55) {
				pump.Translate(0, 0, -0.01f);
			} else if(i > 55 && i < 70) {
				pump.Translate(0, 0, 0.01f);
			}
			
			yield return new WaitForSeconds(0.01f);
			
		}
		
		// Reset everythings position/rotation to make sure we dont drift
		transform.localRotation = Quaternion.Euler(0, 180, 0);
		trigger.localPosition = new Vector3(0, -0.03839342f, 0.125f);
		pump.localPosition = new Vector3(0, -0.03153691f, -0.1396921f);
		
		player.firing = false;
		ready = true;
		yield return null;
		
	}
	
	
	IEnumerator ReloadWeapon() {
		
		reloadSound.Play(0);
		
		for(int i = 0; i < 100; i++) {
			
			reloadProgress.sizeDelta = new Vector2(i, 10);
			reloadProgress.localPosition = new Vector3((i / 2) - 50, 0, 0);
			
			if(i < 8) {
				transform.Rotate(2f, 0, 2f);
			} else if(i > 92) {
				transform.Rotate(-2f, 0, -2f);
			}
			
			yield return new WaitForSeconds(reloadTime / 100.0f);
			
		}
		
		// Reset everythings position/rotation to make sure we dont drift
		transform.localRotation = Quaternion.Euler(0, 180, 0);
		
		isReloading = false;
		player.reloading = false;
		
		if(player.shotgunShells >= (maxShells - player.shotgunLoaded)) {
			// Player has enough bullets to fill magazine
			player.shotgunShells -= (maxShells - player.shotgunLoaded);
			player.shotgunLoaded = maxShells;
		} else {
			// Player does not have enough bullets to fully fill magazine
			player.shotgunLoaded += player.shotgunShells;
			player.shotgunShells = 0;
		}
		
	}
	
}
