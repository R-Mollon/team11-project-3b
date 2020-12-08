using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpgradesHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	
	
	private Image thisBackground;
	private CanvasGroup hoverBox;
	private RectTransform hoverTransform;
	
	private bool pointerInside;
	
	void Start() {
		thisBackground = gameObject.GetComponent<Image>();
		hoverBox = GameObject.Find("Upgrades/UpgradesHover").GetComponent<CanvasGroup>();
		hoverTransform = GameObject.Find("Upgrades/UpgradesHover").GetComponent<RectTransform>();
	}
	
	
	void Update() {
		if(pointerInside)
			hoverTransform.localPosition = new Vector3((Input.mousePosition.x / 225) - 12.2f, (Input.mousePosition.y / 225) + 52.3f, -19.3f);
	}
	
	
	public void OnPointerEnter(PointerEventData eventData) {
		thisBackground.color = new Color(1f, 1f, 1f, 0.25f);
		hoverBox.alpha = 1;
		pointerInside = true;
	}
	
	public void OnPointerExit(PointerEventData eventData) {
		thisBackground.color = new Color(1f, 1f, 1f, 0.0f);
		hoverBox.alpha = 0;
		pointerInside = false;
	}
	
}
