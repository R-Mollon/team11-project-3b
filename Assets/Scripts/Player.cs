using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	
	private Rigidbody playerBody;
	private Camera mainCamera;
	
	private float xRotation;
	private float yRotation;
	
	public float playerSpeed = 5.0f;
	
	private bool jumpCooldown;
	
	void Start() {
		
		playerBody = gameObject.GetComponent<Rigidbody>();
		mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
		
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
	}
	
	
}











