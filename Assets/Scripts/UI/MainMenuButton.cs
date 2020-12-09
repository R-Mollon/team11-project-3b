using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	
	public Text thisText;
	
	
	private int blue;
	private int red;
	private bool dir;
	
	void Start() {
		
		thisText = transform.GetChild(0).GetComponent<Text>();
		
	}
	
	
	public void OnPointerEnter(PointerEventData eventData) {
		thisText.color = new Color(0, 0, 190.0f / 255);
		blue = 190;
		red = 0;
		dir = true;
		StartCoroutine("Glow");
	}
	
	public void OnPointerExit(PointerEventData eventData) {
		StopCoroutine("Glow");
		thisText.color = new Color(215.0f / 255, 215.0f / 255, 215.0f / 255);
	}
	
	
	IEnumerator Glow() {
		
		while(true) {
			
			if(dir) {
				blue--;
				red++;
				
				if(blue <= 0)
					dir = false;
			} else {
				blue++;
				red--;
				
				if(red <= 0)
					dir = true;
			}
			
			thisText.color = new Color(red / 255.0f, 0, blue / 255.0f);
			
			yield return new WaitForSecondsRealtime(0.005f);
		}
		
	}
	
	
}

















