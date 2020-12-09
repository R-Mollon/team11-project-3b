using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpgradesHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	
	
	private Image thisBackground;
	private CanvasGroup hoverBox;
	private RectTransform hoverTransform;
	
	public int hoverID;
	
	private bool pointerInside;
	
	private PersistantData playerData;
	private UpgradesData upgradeData;
	
	private Text upgradeTitleText;
	private Text upgradeCurrentText;
	private Text upgradeNextText;
	private Text upgradeCostText;
	
	void Start() {
		thisBackground = gameObject.GetComponent<Image>();
		hoverBox = GameObject.Find("Upgrades/UpgradesHover").GetComponent<CanvasGroup>();
		hoverTransform = GameObject.Find("Upgrades/UpgradesHover").GetComponent<RectTransform>();
		
		playerData = GameObject.Find("PersistantData").GetComponent<PersistantData>();
		upgradeData = GameObject.Find("Upgrades/Upgrades Data").GetComponent<UpgradesData>();
		
		upgradeTitleText = GameObject.Find("Upgrades/UpgradesHover/Info/Name").GetComponent<Text>();
		upgradeCurrentText = GameObject.Find("Upgrades/UpgradesHover/Info/Current").GetComponent<Text>();
		upgradeNextText = GameObject.Find("Upgrades/UpgradesHover/Info/Next").GetComponent<Text>();
		upgradeCostText = GameObject.Find("Upgrades/UpgradesHover/Info/Cost").GetComponent<Text>();
	}
	
	public void OnPointerEnter(PointerEventData eventData) {
		thisBackground.color = new Color(1f, 1f, 1f, 0.25f);
		hoverBox.alpha = 1;
		
		
		// Get details for upgrade to show in hover box
		string upgradeName = "";
		float upgradeCurrent = -1.0f;
		float upgradeNext = -1.0f;
		int upgradeCost = -1;
		
		switch(hoverID) {
			case 0:
				upgradeName = "Handgun Damage";
				upgradeCurrent = upgradeData.handgunDamageAmounts[playerData.handgunDamageUpgrades];
				if(playerData.handgunDamageUpgrades < 7) {
					upgradeNext = upgradeData.handgunDamageAmounts[playerData.handgunDamageUpgrades + 1];
					upgradeCost = upgradeData.handgunDamageCosts[playerData.handgunDamageUpgrades + 1];
				} else {
					upgradeNext = -1.0f;
					upgradeCost = -1;
				}
				break;
			case 1:
				upgradeName = "Handgun Reload Speed";
				upgradeCurrent = upgradeData.handgunReloadAmounts[playerData.handgunReloadUpgrades];
				if(playerData.handgunReloadUpgrades < 7) {
					upgradeNext = upgradeData.handgunReloadAmounts[playerData.handgunReloadUpgrades + 1];
					upgradeCost = upgradeData.handgunReloadCosts[playerData.handgunReloadUpgrades + 1];
				} else {
					upgradeNext = -1.0f;
					upgradeCost = -1;
				}
				break;
			case 2:
				upgradeName = "M4 Damage";
				upgradeCurrent = upgradeData.automaticDamageAmounts[playerData.automaticDamageUpgrades];
				if(playerData.automaticDamageUpgrades < 7) {
					upgradeNext = upgradeData.automaticDamageAmounts[playerData.automaticDamageUpgrades + 1];
					upgradeCost = upgradeData.automaticDamageCosts[playerData.automaticDamageUpgrades + 1];
				} else {
					upgradeNext = -1.0f;
					upgradeCost = -1;
				}
				break;
			case 3:
				upgradeName = "M4 Reload Speed";
				upgradeCurrent = upgradeData.automaticReloadAmounts[playerData.automaticReloadUpgrades];
				if(playerData.automaticReloadUpgrades < 7) {
					upgradeNext = upgradeData.automaticReloadAmounts[playerData.automaticReloadUpgrades + 1];
					upgradeCost = upgradeData.automaticReloadCosts[playerData.automaticReloadUpgrades + 1];
				} else {
					upgradeNext = -1.0f;
					upgradeCost = -1;
				}
				break;
			case 4:
				upgradeName = "Shotgun Damage";
				upgradeCurrent = upgradeData.shotgunDamageAmounts[playerData.shotgunDamageUpgrades];
				if(playerData.shotgunDamageUpgrades < 7) {
					upgradeNext = upgradeData.shotgunDamageAmounts[playerData.shotgunDamageUpgrades + 1];
					upgradeCost = upgradeData.shotgunDamageCosts[playerData.shotgunDamageUpgrades + 1];
				} else {
					upgradeNext = -1.0f;
					upgradeCost = -1;
				}
				break;
			case 5:
				upgradeName = "Shotgun Reload Speed";
				upgradeCurrent = upgradeData.shotgunReloadAmounts[playerData.shotgunReloadUpgrades];
				if(playerData.shotgunReloadUpgrades < 7) {
					upgradeNext = upgradeData.shotgunReloadAmounts[playerData.shotgunReloadUpgrades + 1];
					upgradeCost = upgradeData.shotgunReloadCosts[playerData.shotgunReloadUpgrades + 1];
				} else {
					upgradeNext = -1.0f;
					upgradeCost = -1;
				}
				break;
			case 6:
				upgradeName = "Movement Speed";
				upgradeCurrent = upgradeData.moveSpeedAmounts[playerData.moveSpeedUpgrades];
				if(playerData.moveSpeedUpgrades < 7) {
					upgradeNext = upgradeData.moveSpeedAmounts[playerData.moveSpeedUpgrades + 1];
					upgradeCost = upgradeData.moveSpeedCosts[playerData.moveSpeedUpgrades + 1];
				} else {
					upgradeNext = -1.0f;
					upgradeCost = -1;
				}
				break;
			case 7:
				upgradeName = "Credits Multiplier";
				upgradeCurrent = upgradeData.creditsMultAmounts[playerData.creditsMultUpgrades];
				if(playerData.creditsMultUpgrades < 7) {
					upgradeNext = upgradeData.creditsMultAmounts[playerData.creditsMultUpgrades + 1];
					upgradeCost = upgradeData.creditsMultCosts[playerData.creditsMultUpgrades + 1];
				} else {
					upgradeNext = -1.0f;
					upgradeCost = -1;
				}
				break;
		}
		
		// Place data
		upgradeTitleText.text = "Upgrade: " + upgradeName;
		upgradeCurrentText.text = "Current:\n" + upgradeCurrent.ToString() + "x";
		if(upgradeNext == -1.0f)
			upgradeNextText.text = "Next:\nN/A";
		else
			upgradeNextText.text = "Next:\n" + upgradeNext.ToString() + "x";
		
		if(upgradeCost == -1)
			upgradeCostText.text = "MAX LEVEL";
		else
			upgradeCostText.text = "Cost: §" + upgradeCost.ToString();
		
	}
	
	public void OnPointerExit(PointerEventData eventData) {
		thisBackground.color = new Color(1f, 1f, 1f, 0.0f);
		hoverBox.alpha = 0;
	}
	
}





















