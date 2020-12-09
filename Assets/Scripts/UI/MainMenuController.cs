using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour {
	
	private Camera mainCamera;
	
	private int position;
	private int vertPosition;
	private float panTarget;
	
	void Start() {
		
		mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
		
		position = 0;
		vertPosition = 0;
		
	}
	
	public void panLeft() {
		if(position <= -1)
			return;
		
		position--;
		
		// Pan 10 units to the left
		panTarget = -10.0f;
		StartCoroutine("smoothPanLR");
	}
	
	public void panRight() {
		if(position >= 1)
			return;
		
		position++;
		
		// Pan 10 units to the left
		panTarget = 10.0f;
		StartCoroutine("smoothPanLR");
	}
	
	IEnumerator smoothPanLR() {
		
		for(int i = 0; i < 100; i++) {
			mainCamera.transform.Translate(panTarget / 100, 0, 0);
			
			yield return new WaitForSecondsRealtime(0.0008f);
		}
		
		yield return null;
		
	}
	
	public void panUp() {
		if(vertPosition >= 1)
			return;
		
		vertPosition++;
		
		// Pan 10 units upward
		panTarget = 10.0f;
		StartCoroutine("smoothPanUD");
	}
	
	public void panDown() {
		if(vertPosition <= 0)
			return;
		
		vertPosition--;
		
		// Pan 10 units downward
		panTarget = -10.0f;
		StartCoroutine("smoothPanUD");
	}
	
	IEnumerator smoothPanUD() {
		
		for(int i = 0; i < 100; i++) {
			mainCamera.transform.Translate(0, panTarget / 100, 0);
			
			yield return new WaitForSecondsRealtime(0.0008f);
		}
		
		yield return null;
		
	}
	
	public void quitGame(int ignoreLevel) {

		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
		#else
			Application.Quit();
		#endif
    }
	
}
