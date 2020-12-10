using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsManager : MonoBehaviour {
	
	private Transform creditsText;
	
	private float targetPos = 3600.0f;
	
	void Start() {
		
		creditsText = GameObject.Find("Canvas/Text").transform;
		
		StartCoroutine("DoScroll");
		
	}
	
	
	void Update() {
		
		if(Input.GetKey(KeyCode.Escape)) {
			StopCoroutine("DoScroll");
			SceneManager.LoadScene(0);
		}
		
	}
	
	
	IEnumerator DoScroll() {
		
		while(creditsText.localPosition.y < targetPos) {
			
			creditsText.Translate(0, Screen.height / 300.0f, 0);
			
			yield return new WaitForSecondsRealtime(0.0000001f);
		}
		
		SceneManager.LoadScene(0);
		
		yield return null;
		
	}
	
}
