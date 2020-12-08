using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour {
	
	private Camera mainCamera;
	
	private int position;
	private float panTarget;
	
	void Start() {
		
		mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
		
		position = 0;
		
	}
	
	public void panLeft() {
		if(position <= -1)
			return;
		
		position--;
		
		// Pan 10 units to the left
		panTarget = -10.0f;
		StartCoroutine("smoothPan");
	}
	
	public void panRight() {
		if(position >= 1)
			return;
		
		position++;
		
		// Pan 10 units to the left
		panTarget = 10.0f;
		StartCoroutine("smoothPan");
	}
	
	IEnumerator smoothPan() {
		
		for(int i = 0; i < 100; i++) {
			mainCamera.transform.Translate(panTarget / 100, 0, 0);
			
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
