﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
	
	private Rigidbody playerBody;
	private Camera mainCamera;
	
	private float xRotation;
	private float yRotation;
	
	public float playerSpeed = 5.0f;

	public static bool paused;


	
	/* Ingame weapon data */
	public int handgunMagazine;
	public int handgunBullets;
	public static bool hasHandgun = true;
	
	public int automaticMagazine;
	public int automaticBullets;
	public static bool hasAutomatic = true;

	public int shotgunLoaded;
	public int shotgunShells;
	public static bool hasShotgun = true;

	public static bool hasSword = true;

	public bool reloading;
	public bool firing;
	private int equippedWeapon = 1;
	
	private Text UIweaponName;
	private Text UIweaponShots;
	private CanvasGroup UIreloadIndicator;
	private CanvasGroup UIreloadingIndicator;
	private CanvasGroup UIoutOfAmmoIndicator;
	/**/
	
	
	void Start() {
		
		playerBody = gameObject.GetComponent<Rigidbody>();
		mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
		
		UIweaponName = GameObject.Find("HUD/WeaponData/WeaponName").GetComponent<Text>();
		UIweaponShots = GameObject.Find("HUD/WeaponData/WeaponShots").GetComponent<Text>();
		UIreloadIndicator = GameObject.Find("HUD/WeaponData/ReloadIndicator").GetComponent<CanvasGroup>();
		UIreloadingIndicator = GameObject.Find("HUD/WeaponData/ReloadingIndicator").GetComponent<CanvasGroup>();
		UIoutOfAmmoIndicator = GameObject.Find("HUD/WeaponData/OutOfAmmoIndicator").GetComponent<CanvasGroup>();
		
		// Lock cursor
		Cursor.lockState = CursorLockMode.Locked;
		paused = false;
		
	}
	
	void Update() {
		
		// Initialize velocity vector for movement
		Vector3 newVelocity = new Vector3(0, playerBody.velocity.y, 0);
		
		// Calculate forward/backward angles
		float fbZAngle = Mathf.Cos(xRotation * Mathf.PI / 180);
		float fbXAngle = Mathf.Sin(xRotation * Mathf.PI / 180);
		
		// Calculate strafe angles
		float strafeZAngle = Mathf.Cos((xRotation + 90.0f) * Mathf.PI / 180);
		float strafeXAngle = Mathf.Sin((xRotation + 90.0f) * Mathf.PI / 180);
		
		// Check for inputs
		if(Input.GetKey(KeyCode.W)) {
			// Walking forward
			newVelocity.z += fbZAngle * playerSpeed;
			newVelocity.x += fbXAngle * playerSpeed;
		}
		if(Input.GetKey(KeyCode.A)) {
			// Strafing left
			newVelocity.z -= strafeZAngle * playerSpeed;
			newVelocity.x -= strafeXAngle * playerSpeed;
		}
		if(Input.GetKey(KeyCode.S)) {
			// Walking backward
			newVelocity.z -= fbZAngle * playerSpeed;
			newVelocity.x -= fbXAngle * playerSpeed;
		}
		if(Input.GetKey(KeyCode.D)) {
			// Strafing right
			newVelocity.z += strafeZAngle * playerSpeed;
			newVelocity.x += strafeXAngle * playerSpeed;
		}
		if(Input.GetKey(KeyCode.Space) && newVelocity.y == 0) {
			// Jumping
			newVelocity.y += 5;
		}

        if (!paused)
        {
			// Set velocity of player body to new velocity
			playerBody.velocity = newVelocity;

			// Teleport camera to position of player body
			mainCamera.transform.position = transform.position + transform.up;

			// Add mouse x and y positions to rotation values
			xRotation += Input.GetAxis("Mouse X");
			yRotation += Input.GetAxis("Mouse Y");

			// Ensure x rotation stays in [-360, 360] but DON'T prevent rotation beyond
			if (Mathf.Abs(xRotation) > 360)
				xRotation = 0;

			// Ensure y rotation stays in [-90, 90] and prevent rotation beyond
			yRotation = Mathf.Clamp(yRotation, -90, 90);

			// Rotate camera to proper angle
			mainCamera.transform.eulerAngles = new Vector3(-yRotation, xRotation, 0);


			// Handle weapons
			handleWeapons();
		}
		
		
	
	}
	
	
	// handleWeapons is called once per update
	// It is separated to reduce clutter in Update
	private void handleWeapons() {
		
		// Check for weapon changes
		if(!reloading && !firing && !paused) {
			if(Input.GetKey(KeyCode.Alpha1) && hasHandgun) {
				equippedWeapon = 1;
				switchWeapon();
			} else if(Input.GetKey(KeyCode.Alpha2) && hasAutomatic) {
				equippedWeapon = 2;
				switchWeapon();
			} else if(Input.GetKey(KeyCode.Alpha3) && hasShotgun) {
				equippedWeapon = 3;
				switchWeapon();
			} else if(Input.GetKey(KeyCode.Alpha4) && hasSword) {
				equippedWeapon = 4;
				switchWeapon();
			}
		}
		
		// Check for reload
		if(Input.GetKey(KeyCode.R) && !reloading && !firing && !paused) {
			
			// Check current weapon is not full ammo
			if(
			(equippedWeapon == 1 && handgunMagazine < 8 && handgunBullets > 0) ||
			(equippedWeapon == 2 && automaticMagazine < 30 && automaticBullets > 0) ||
			(equippedWeapon == 3 && shotgunLoaded < 6 && shotgunShells > 0)
			) {
			
				reloading = true;
				
				UIreloadIndicator.alpha = 0;
				UIreloadingIndicator.alpha = 1;
				
			}
		}
		
		if(!reloading) {
			UIreloadingIndicator.alpha = 0;
		}
	
		// Render weapon data
		Vector2 numBullets;
		
		switch(equippedWeapon) {
			case 1:
			default:
				UIweaponName.text = "Handgun";
				numBullets = new Vector2(handgunMagazine, handgunBullets);
				break;
			case 2:
				UIweaponName.text = "M4";
				numBullets = new Vector2(automaticMagazine, automaticBullets);
				break;
			case 3:
				UIweaponName.text = "Shotgun";
				numBullets = new Vector2(shotgunLoaded, shotgunShells);
				break;
			case 4:
				UIweaponName.text = "Sword";
				numBullets = new Vector2(0, 0);
				break;
		}
		
		if(equippedWeapon < 4) {
			UIweaponShots.text = numBullets.x + "/" + numBullets.y;
		} else {
			UIweaponShots.text = "";
		}
		
		// Render if current weapon needs to be reloaded
		UIreloadIndicator.alpha = 0;
		UIoutOfAmmoIndicator.alpha = 0;
		switch(equippedWeapon) {
			case 1:
				if(handgunMagazine <= 0 && !reloading && handgunBullets > 0)
					UIreloadIndicator.alpha = 1;
				if(handgunMagazine <= 0 && handgunBullets <= 0)
					UIoutOfAmmoIndicator.alpha = 1;
				break;
			case 2:
				if(automaticMagazine <= 0 && !reloading && automaticBullets > 0)
					UIreloadIndicator.alpha = 1;
				if(automaticMagazine <= 0 && automaticBullets <= 0)
					UIoutOfAmmoIndicator.alpha = 1;
				break;
			case 3:
				if(shotgunLoaded <= 0 && !reloading && shotgunShells > 0)
					UIreloadIndicator.alpha = 1;
				if(shotgunLoaded <= 0 && shotgunShells <= 0)
					UIoutOfAmmoIndicator.alpha = 1;
				break;
			case 4:
			default:
				UIreloadIndicator.alpha = 0;
				break;
		}
		
	}
	
	
	// Change weapon player is currently holding
	private void switchWeapon() {
		
		// Destroy currently held weapon
		foreach(Transform child in mainCamera.gameObject.transform) {
			if(child.tag == "Weapon")
				Destroy(child.gameObject);
		}
		
		// Add new weapon
		GameObject weapon;
		Vector3 position;
		Quaternion rotation;
		
		switch(equippedWeapon) {
			case 1:
			default:
				weapon = Instantiate(Resources.Load("Prefabs/Handgun")) as GameObject;
				position = new Vector3(0.3f, -0.1f, 0.5f);
				rotation = Quaternion.Euler(0, 180, 0);
				break;
			case 2:
				weapon = Instantiate(Resources.Load("Prefabs/AutomaticGunM4")) as GameObject;
				position = new Vector3(0.49f, -0.3f, 0.74f);
				rotation = Quaternion.Euler(0, 180, 0);
				break;
			case 3:
				weapon = Instantiate(Resources.Load("Prefabs/Shotgun")) as GameObject;
				position = new Vector3(0.49f, -0.14f, 0.78f);
				rotation = Quaternion.Euler(0, 180, 0);
				break;
			case 4:
				weapon = Instantiate(Resources.Load("Prefabs/Sword")) as GameObject;
				position = new Vector3(0.4f, -0.4f, 0.5f);
				rotation = Quaternion.Euler(20, 90, 0);
				break;
		}
		
		weapon.transform.parent = mainCamera.gameObject.transform;
		weapon.transform.localPosition = position;
		
		weapon.transform.localRotation = rotation;
		
	}
	
	
}











