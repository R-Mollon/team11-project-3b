using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour {
	
	private bool ready = true;
	
	private Camera mainCamera;
	
	public float bulletSpeed;
	
	public Player player;
	
	private bool isReloading;
	private int maxShells = 2;
	private float reloadTime = 4.0f;
	
	private RectTransform reloadProgress;
	private AudioSource shotSound;
	
	void Start() {
		
		mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
		player = GameObject.Find("Player").GetComponent<Player>();
		reloadProgress = GameObject.Find("HUD/WeaponData/ReloadingIndicator/ReloadingBarProgress").GetComponent<RectTransform>();
		shotSound = gameObject.GetComponent<AudioSource>();
		
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
	
	
	IEnumerator ReloadWeapon() {
		
		for(int i = 0; i < 100; i++) {
			
			reloadProgress.sizeDelta = new Vector2(i, 10);
			reloadProgress.localPosition = new Vector3((i / 2) - 50, 0, 0);
			
			yield return new WaitForSeconds(reloadTime / 100.0f);
			
		}
		
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
