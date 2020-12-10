using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
	
	private Rigidbody playerBody;
	private Camera mainCamera;
	
	private float xRotation;
	private float yRotation;
	
	public float playerSpeed = 5.0f;

	public static bool paused;
	
	public int credits;
	public float creditsDecimal;
	
	public float health;
	public float maxHealth;
	private bool takenDamage;
	
	private Image UIhealthBar;
	
	private CanvasGroup HUDCanvas;

	
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
	
	private Text UInumCredits;
	private Text UInumCreditsBG;
	
	private PersistantData playerData;
	private float movementScale;
	
	/* Vars Relating to Death */
	private CanvasGroup UIdeathBackground;
	
	private CanvasGroup UIdeathPersistantCredits;
	private CanvasGroup UIdeathGameCredits;
	private CanvasGroup UIdeathNewCredits;
	
	private Text UIdeathPersistantCreditsText;
	private Text UIdeathGameCreditsText;
	private Text UIdeathNewCreditsText;
	
	private int countedPersistantCredits;
	private int countedGameCredits;
	private int newCredits;
	
	private AudioSource dingSound;
	/**/
	
	
	void Start() {
		
		playerBody = gameObject.GetComponent<Rigidbody>();
		mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
		
		HUDCanvas = GameObject.Find("HUD").GetComponent<CanvasGroup>();
		
		UIweaponName = GameObject.Find("HUD/WeaponData/WeaponName").GetComponent<Text>();
		UIweaponShots = GameObject.Find("HUD/WeaponData/WeaponShots").GetComponent<Text>();
		UIreloadIndicator = GameObject.Find("HUD/WeaponData/ReloadIndicator").GetComponent<CanvasGroup>();
		UIreloadingIndicator = GameObject.Find("HUD/WeaponData/ReloadingIndicator").GetComponent<CanvasGroup>();
		UIoutOfAmmoIndicator = GameObject.Find("HUD/WeaponData/OutOfAmmoIndicator").GetComponent<CanvasGroup>();
		
		UInumCredits = GameObject.Find("HUD/CreditsData/NumCredits").GetComponent<Text>();
		UInumCreditsBG = GameObject.Find("HUD/CreditsData/NumCreditsBG").GetComponent<Text>();
		
		UIhealthBar = GameObject.Find("HUD/Health/HealthBar").GetComponent<Image>();
		
		
		UIdeathBackground = GameObject.Find("DeathHUD/DeathBackground").GetComponent<CanvasGroup>();
		
		UIdeathPersistantCredits = GameObject.Find("DeathHUD/DeathBackground/PersistantCredits").GetComponent<CanvasGroup>();
		UIdeathGameCredits = GameObject.Find("DeathHUD/DeathBackground/GameCredits").GetComponent<CanvasGroup>();
		UIdeathNewCredits = GameObject.Find("DeathHUD/DeathBackground/NewCredits").GetComponent<CanvasGroup>();
		
		UIdeathPersistantCreditsText = GameObject.Find("DeathHUD/DeathBackground/PersistantCredits/Overall").GetComponent<Text>();
		UIdeathGameCreditsText = GameObject.Find("DeathHUD/DeathBackground/GameCredits/Overall").GetComponent<Text>();
		UIdeathNewCreditsText = GameObject.Find("DeathHUD/DeathBackground/NewCredits/Overall").GetComponent<Text>();
		
		dingSound = GameObject.Find("DeathHUD/DeathBackground/Ding Sound").GetComponent<AudioSource>();
		
		
		// Lock cursor
		Cursor.lockState = CursorLockMode.Locked;
		paused = false;
		
		
		movementScale = 1.0f;
		GameObject playerDataObject = GameObject.Find("PersistantData");
		if(playerDataObject != null) {
			playerData = playerDataObject.GetComponent<PersistantData>();
			
			movementScale = playerData.moveSpeed;
		}
		
		playerSpeed *= movementScale;
		
		maxHealth = 30.0f;
		health = maxHealth;
		
		StartCoroutine("PlayerHeal");
		
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

		// Teleport camera to position of player body
		mainCamera.transform.position = transform.position + transform.up;

		// Render weapon data
		Vector2 numBullets;

		switch (equippedWeapon)
		{
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

		if (equippedWeapon < 4)
		{
			UIweaponShots.text = numBullets.x + "/" + numBullets.y;
		}
		else
		{
			UIweaponShots.text = "";
		}

		if (!paused)
        {
			// Set velocity of player body to new velocity
			playerBody.velocity = newVelocity;

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
		
		// Update number of credits
		UInumCredits.text = "§" + credits.ToString();
		UInumCreditsBG.text = "§" + credits.ToString();
		
		// Update health bar
		UIhealthBar.rectTransform.sizeDelta = new Vector2(health * 10.0f, 25);
		UIhealthBar.rectTransform.localPosition = new Vector3((30.0f - health) * -5.0f, -190, 0);
	
		// Convert decimal credits to credits
		if(creditsDecimal >= 1.0f) {
			creditsDecimal -= 1.0f;
			credits++;
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
	
	public void takeDamage(float damage)
    {
		health -= damage;
		takenDamage = true;

		if (health <= 0.0f)
		{
			
			onDeath();

		}
	}
	
	IEnumerator PlayerHeal() {
		
		while(true) {
			
			if(takenDamage) {
				takenDamage = false;
				yield return new WaitForSecondsRealtime(1.5f);
			}
			
			while(health < maxHealth && !takenDamage) {
				health += 0.1f;
				
				if(health > maxHealth)
					health = maxHealth;
				
				yield return new WaitForSecondsRealtime(0.01f);
			}
			
			yield return new WaitForSecondsRealtime(0.1f);
			
		}
		
	}
	
	
	private void onDeath() {
		
		Time.timeScale = 0;
		paused = true;
		
		countedPersistantCredits = 0;
		countedGameCredits = 0;
		newCredits = playerData.credits + credits;
		HUDCanvas.alpha = 0;
		Cursor.lockState = CursorLockMode.None;
		StopAllCoroutines();
		
		UIdeathPersistantCredits.alpha = 0;
		UIdeathGameCredits.alpha = 0;
		UIdeathNewCredits.alpha = 0;
		
		StartCoroutine("Death");
		
	}
	
	IEnumerator Death() {
		
		for(int i = 0; i < 1000; i++) {
			
			// Fade screen in
			if(i <= 20) {
				UIdeathBackground.alpha = i / 20.0f;
			}
			
			// Fade in overall credits
			if(i >= 150 && i <= 160) {
				UIdeathPersistantCredits.alpha = (i - 150) / 10.0f;
			}
			
			if(i == 161) {
				// Count overall credits
				while(countedPersistantCredits < playerData.credits) {
					
					if(playerData.credits - countedPersistantCredits > 10000)
						countedPersistantCredits += 1110;
					else if(playerData.credits - countedPersistantCredits > 1000)
						countedPersistantCredits += 110;
					else if(playerData.credits - countedPersistantCredits > 100)
						countedPersistantCredits += 11;
					
					countedPersistantCredits++;
					UIdeathPersistantCreditsText.text = "§" + countedPersistantCredits.ToString();
					dingSound.PlayOneShot(dingSound.clip, 0.4f);
					
					float countTime = Mathf.Min(1.0f / (playerData.credits - countedPersistantCredits), 0.00000001f);
					
					yield return new WaitForSecondsRealtime(countTime);
				}
			}
			
			// Fade in game credits
			if(i >= 250 && i <= 260) {
				UIdeathGameCredits.alpha = (i - 250) / 10.0f;
			}
			
			if(i == 261) {
				// Count game credits
				while(countedGameCredits < credits) {
					
					if(credits - countedGameCredits > 10000)
						countedGameCredits += 1110;
					else if(credits - countedGameCredits > 1000)
						countedGameCredits += 110;
					else if(credits - countedGameCredits > 100)
						countedGameCredits += 11;
					
					countedGameCredits++;
					UIdeathGameCreditsText.text = "§" + countedGameCredits.ToString();
					dingSound.PlayOneShot(dingSound.clip, 0.4f);
					
					float countTime = Mathf.Min(1.0f / (credits - countedGameCredits), 0.00000001f);
					
					yield return new WaitForSecondsRealtime(countTime);
				}
			}
			
			// Fade in new credits
			if(i >= 350 && i <= 360) {
				UIdeathNewCredits.alpha = (i - 350) / 10.0f;
				
				UIdeathNewCreditsText.text = "§" + newCredits.ToString();
			}
			
			// Load back to main menu
			if(i == 999) {
				playerData.credits = newCredits;
				Time.timeScale = 1;
				SceneManager.LoadScene(0);
			}
			
			
			yield return new WaitForSecondsRealtime(0.001f);
		}
		
		yield return null;
		
	}
	
}


























