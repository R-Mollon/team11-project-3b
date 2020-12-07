using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
	
	private Rigidbody playerBody;
	private Camera mainCamera;
	
	private float xRotation;
	private float yRotation;
	
	public float playerSpeed = 5.0f;
	
	
	
	/* Ingame weapon data */
	public int handgunMagazine;
	public int handgunBullets;
	
	public int automaticMagazine;
	public int automaticBullets;
	
	public int shotgunLoaded;
	public int shotgunShells;
	
	private int equippedWeapon = 1;
	
	private Text UIweaponName;
	private Text UIweaponShots;
	/**/
	
	
	void Start() {
		
		playerBody = gameObject.GetComponent<Rigidbody>();
		mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
		
		UIweaponName = GameObject.Find("HUD/WeaponData/WeaponName").GetComponent<Text>();
		UIweaponShots = GameObject.Find("HUD/WeaponData/WeaponShots").GetComponent<Text>();
		
		// Lock cursor
		Cursor.lockState = CursorLockMode.Locked;
		
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
		
		// Set velocity of player body to new velocity
		playerBody.velocity = newVelocity;
		
		// Teleport camera to position of player body
		mainCamera.transform.position = transform.position;
		
		// Add mouse x and y positions to rotation values
		xRotation += Input.GetAxis("Mouse X");
		yRotation += Input.GetAxis("Mouse Y");
		
		// Ensure x rotation stays in [-360, 360] but DON'T prevent rotation beyond
		if(Mathf.Abs(xRotation) > 360)
			xRotation = 0;
		
		// Ensure y rotation stays in [-90, 90] and prevent rotation beyond
		yRotation = Mathf.Clamp(yRotation, -90, 90);
		
		// Rotate camera to proper angle
		mainCamera.transform.eulerAngles = new Vector3(-yRotation, xRotation, 0);
		
		
		// Check for weapon changes
		if(Input.GetKey(KeyCode.Alpha1)) {
			equippedWeapon = 1;
			switchWeapon();
		} else if(Input.GetKey(KeyCode.Alpha2)) {
			equippedWeapon = 2;
			switchWeapon();
		} else if(Input.GetKey(KeyCode.Alpha3)) {
			equippedWeapon = 3;
			switchWeapon();
		} else if(Input.GetKey(KeyCode.Alpha4)) {
			equippedWeapon = 4;
			switchWeapon();
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
				UIweaponName.text = "Automatic Gun";
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
		
		switch(equippedWeapon) {
			case 1:
			default:
				weapon = Instantiate(Resources.Load("Prefabs/Handgun")) as GameObject;
				position = new Vector3(0.61f, -0.34f, 0.88f);
				break;
			case 2:
				weapon = Instantiate(Resources.Load("Prefabs/AutomaticGun")) as GameObject;
				position = new Vector3(0.61f, -0.34f, 0.88f);
				break;
			case 3:
				weapon = Instantiate(Resources.Load("Prefabs/Shotgun")) as GameObject;
				position = new Vector3(0.61f, -0.34f, 0.88f);
				break;
			case 4:
				weapon = Instantiate(Resources.Load("Prefabs/AutomaticGun")) as GameObject;
				position = new Vector3(0.61f, -0.34f, 0.88f);
				break;
		}
		
		weapon.transform.parent = mainCamera.gameObject.transform;
		weapon.transform.localPosition = position;
		
		weapon.transform.localRotation = Quaternion.Euler(0, -100, 0);
		//weapon.transform.Rotate(0, -100, 0);
		
	}
	
	
}











