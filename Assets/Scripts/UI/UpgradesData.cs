using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UpgradesData : MonoBehaviour {
	
	// Script to hold data for persistant upgrades and initialize upgrade screen on start
	
	public int[] handgunDamageCosts = new[] {0, 10, 20, 40, 80, 160, 320, 640};
	public float[] handgunDamageAmounts = new[] {1.0f, 1.3f, 1.5f, 1.7f, 1.9f, 2.1f, 2.3f, 2.5f};
	
	public int[] handgunReloadCosts = new[] {0, 10, 20, 40, 80, 160, 320, 640};
	public float[] handgunReloadAmounts = new[] {1.0f, 1.1f, 1.2f, 1.3f, 1.35f, 1.41f, 1.46f, 1.5f};
	
	
	public int[] automaticDamageCosts = new[] {0, 10, 20, 40, 80, 160, 320, 640};
	public float[] automaticDamageAmounts = new[] {1.0f, 1.3f, 1.5f, 1.7f, 1.9f, 2.1f, 2.3f, 2.5f};
	
	public int[] automaticReloadCosts = new[] {0, 10, 20, 40, 80, 160, 320, 640};
	public float[] automaticReloadAmounts = new[] {1.0f, 1.2f, 1.4f, 1.6f, 1.7f, 1.8f, 1.9f, 2.0f};
	
	
	public int[] shotgunDamageCosts = new[] {0, 10, 20, 40, 80, 160, 320, 640};
	public float[] shotgunDamageAmounts = new[] {1.0f, 1.3f, 1.5f, 1.7f, 1.9f, 2.1f, 2.3f, 2.5f};

	public int[] shotgunReloadCosts = new[] {0, 10, 20, 40, 80, 160, 320, 640};
	public float[] shotgunReloadAmounts = new[] {1.0f, 1.4f, 1.7f, 2.0f, 2.3f, 2.6f, 2.8f, 3.0f};
	
	
	public int[] moveSpeedCosts = new[] {0, 10, 20, 40, 80, 160, 320, 640};
	public float[] moveSpeedAmounts = new[] {1.0f, 1.2f, 1.4f, 1.6f, 1.7f, 1.8f, 1.9f, 2.0f};
	
	public int[] creditsMultCosts = new[] {0, 10, 20, 40, 80, 160, 320, 640};
	public float[] creditsMultAmounts = new[] {1.0f, 1.2f, 1.4f, 1.6f, 1.7f, 1.8f, 1.9f, 2.0f};
	
	
	
	// Render all upgrades on start
	
	private Image[] handgunDamageCells;
	private Text handgunDamageAmnt;
	
	private Image[] handgunReloadCells;
	private Text handgunReloadAmnt;
	
	
	private Image[] automaticDamageCells;
	private Text automaticDamageAmnt;
	
	private Image[] automaticReloadCells;
	private Text automaticReloadAmnt;
	
	
	private Image[] shotgunDamageCells;
	private Text shotgunDamageAmnt;
	
	private Image[] shotgunReloadCells;
	private Text shotgunReloadAmnt;
	
	
	private Image[] moveSpeedCells;
	private Text moveSpeedAmnt;
	
	private Image[] creditsMultCells;
	private Text creditsMultAmnt;
	
	private PersistantData playerData;
	
	private Text amountCredits;
	
	void Start() {
		
		handgunDamageCells = GameObject.Find("Upgrades/Handgun/Upgrade_1/Cells").gameObject.GetComponentsInChildren<Image>();
		handgunDamageAmnt  = GameObject.Find("Upgrades/Handgun/Upgrade_1/AddButton/Text").GetComponent<Text>();
		
		handgunReloadCells = GameObject.Find("Upgrades/Handgun/Upgrade_2/Cells").gameObject.GetComponentsInChildren<Image>();
		handgunReloadAmnt  = GameObject.Find("Upgrades/Handgun/Upgrade_2/AddButton/Text").GetComponent<Text>();
		
		
		automaticDamageCells = GameObject.Find("Upgrades/AutomaticGun/Upgrade_1/Cells").gameObject.GetComponentsInChildren<Image>();
		automaticDamageAmnt  = GameObject.Find("Upgrades/AutomaticGun/Upgrade_1/AddButton/Text").GetComponent<Text>();
		
		automaticReloadCells = GameObject.Find("Upgrades/AutomaticGun/Upgrade_2/Cells").gameObject.GetComponentsInChildren<Image>();
		automaticReloadAmnt  = GameObject.Find("Upgrades/AutomaticGun/Upgrade_2/AddButton/Text").GetComponent<Text>();
		
		
		shotgunDamageCells = GameObject.Find("Upgrades/Shotgun/Upgrade_1/Cells").gameObject.GetComponentsInChildren<Image>();
		shotgunDamageAmnt  = GameObject.Find("Upgrades/Shotgun/Upgrade_1/AddButton/Text").GetComponent<Text>();
		
		shotgunReloadCells = GameObject.Find("Upgrades/Shotgun/Upgrade_2/Cells").gameObject.GetComponentsInChildren<Image>();
		shotgunReloadAmnt  = GameObject.Find("Upgrades/Shotgun/Upgrade_2/AddButton/Text").GetComponent<Text>();
		
		
		moveSpeedCells = GameObject.Find("Upgrades/Gameplay/Upgrade_1/Cells").gameObject.GetComponentsInChildren<Image>();
		moveSpeedAmnt  = GameObject.Find("Upgrades/Gameplay/Upgrade_1/AddButton/Text").GetComponent<Text>();
		
		creditsMultCells = GameObject.Find("Upgrades/Gameplay/Upgrade_2/Cells").gameObject.GetComponentsInChildren<Image>();
		creditsMultAmnt  = GameObject.Find("Upgrades/Gameplay/Upgrade_2/AddButton/Text").GetComponent<Text>();
		
		playerData = GameObject.Find("PersistantData").GetComponent<PersistantData>();
		
		amountCredits = GameObject.Find("Upgrades/NumCredits/Amount").GetComponent<Text>();
		
		RenderUpgrades();
		ApplyUpgrades();
	}
	
	
	// Render all current upgrades
	public void RenderUpgrades() {
		
		// Mark cells
		for(int i = 0; i < playerData.handgunDamageUpgrades && i < 7; i++)
			handgunDamageCells[i].color = new Color(0.1f, 0.8f, 0.1f);
		for(int i = 0; i < playerData.handgunReloadUpgrades && i < 7; i++)
			handgunReloadCells[i].color = new Color(0.1f, 0.8f, 0.1f);
		
		for(int i = 0; i < playerData.automaticDamageUpgrades && i < 7; i++)
			automaticDamageCells[i].color = new Color(0.1f, 0.8f, 0.1f);
		for(int i = 0; i < playerData.automaticReloadUpgrades && i < 7; i++)
			automaticReloadCells[i].color = new Color(0.1f, 0.8f, 0.1f);
		
		for(int i = 0; i < playerData.shotgunDamageUpgrades && i < 7; i++)
			shotgunDamageCells[i].color = new Color(0.1f, 0.8f, 0.1f);
		for(int i = 0; i < playerData.shotgunReloadUpgrades && i < 7; i++)
			shotgunReloadCells[i].color = new Color(0.1f, 0.8f, 0.1f);
		
		for(int i = 0; i < playerData.moveSpeedUpgrades && i < 7; i++)
			moveSpeedCells[i].color = new Color(0.1f, 0.8f, 0.1f);
		for(int i = 0; i < playerData.creditsMultUpgrades && i < 7; i++)
			creditsMultCells[i].color = new Color(0.1f, 0.8f, 0.1f);
		
		
		// Update button amounts
		int currentMetric = playerData.handgunDamageUpgrades;
		if(currentMetric < 7)
			handgunDamageAmnt.text = "Increase (§" + handgunDamageCosts[currentMetric + 1] + ")";
		else {
			handgunDamageAmnt.text = "MAX LEVEL";
			handgunDamageAmnt.transform.parent.gameObject.GetComponent<Button>().interactable = false;
		}
		currentMetric = playerData.handgunReloadUpgrades;
		if(currentMetric < 7)
			handgunReloadAmnt.text = "Increase (§" + handgunReloadCosts[currentMetric + 1] + ")";
		else {
			handgunReloadAmnt.text = "MAX LEVEL";
			handgunReloadAmnt.transform.parent.gameObject.GetComponent<Button>().interactable = false;
		}
		
		
		currentMetric = playerData.automaticDamageUpgrades;
		if(currentMetric < 7)
			automaticDamageAmnt.text = "Increase (§" + automaticDamageCosts[currentMetric + 1] + ")";
		else {
			automaticDamageAmnt.text = "MAX LEVEL";
			automaticDamageAmnt.transform.parent.gameObject.GetComponent<Button>().interactable = false;
		}
		currentMetric = playerData.automaticReloadUpgrades;
		if(currentMetric < 7)
			automaticReloadAmnt.text = "Increase (§" + automaticReloadCosts[currentMetric + 1] + ")";
		else {
			automaticReloadAmnt.text = "MAX LEVEL";
			automaticReloadAmnt.transform.parent.gameObject.GetComponent<Button>().interactable = false;
		}
		
		
		currentMetric = playerData.shotgunDamageUpgrades;
		if(currentMetric < 7)
			shotgunDamageAmnt.text = "Increase (§" + shotgunDamageCosts[currentMetric + 1] + ")";
		else {
			shotgunDamageAmnt.text = "MAX LEVEL";
			shotgunDamageAmnt.transform.parent.gameObject.GetComponent<Button>().interactable = false;
		}
		currentMetric = playerData.shotgunReloadUpgrades;
		if(currentMetric < 7)
			shotgunReloadAmnt.text = "Increase (§" + shotgunReloadCosts[currentMetric + 1] + ")";
		else {
			shotgunReloadAmnt.text = "MAX LEVEL";
			shotgunReloadAmnt.transform.parent.gameObject.GetComponent<Button>().interactable = false;
		}
		
		
		currentMetric = playerData.moveSpeedUpgrades;
		if(currentMetric < 7)
			moveSpeedAmnt.text = "Increase (§" + moveSpeedCosts[currentMetric + 1] + ")";
		else {
			moveSpeedAmnt.text = "MAX LEVEL";
			moveSpeedAmnt.transform.parent.gameObject.GetComponent<Button>().interactable = false;
		}
		currentMetric = playerData.creditsMultUpgrades;
		if(currentMetric < 7)
			creditsMultAmnt.text = "Increase (§" + creditsMultCosts[currentMetric + 1] + ")";
		else {
			creditsMultAmnt.text = "MAX LEVEL";
			creditsMultAmnt.transform.parent.gameObject.GetComponent<Button>().interactable = false;
		}
		
		
		// Update amount of credits
		amountCredits.text = "§" + playerData.credits;
		
	}
	
	
	// Apply all current upgrades
	public void ApplyUpgrades() {
		
		playerData.handgunDamage = handgunDamageAmounts[Math.Min(playerData.handgunDamageUpgrades, 7)];
		playerData.handgunReload = handgunReloadAmounts[Math.Min(playerData.handgunReloadUpgrades, 7)];
		
		
		playerData.automaticDamage = automaticDamageAmounts[Math.Min(playerData.automaticDamageUpgrades, 7)];
		playerData.automaticReload = automaticReloadAmounts[Math.Min(playerData.automaticReloadUpgrades, 7)];
		
		
		playerData.shotgunDamage = shotgunDamageAmounts[Math.Min(playerData.shotgunDamageUpgrades, 7)];
		playerData.shotgunReload = shotgunReloadAmounts[Math.Min(playerData.shotgunReloadUpgrades, 7)];
		
		
		playerData.moveSpeed = moveSpeedAmounts[Math.Min(playerData.moveSpeedUpgrades, 7)];
		playerData.creditsMult = creditsMultAmounts[Math.Min(playerData.creditsMultUpgrades, 7)];
		
	}
	
	
	// Buy an upgrade
	public void buyUpgrade(int upgradeID) {
		switch(upgradeID) {
			
			case 0:
				if(playerData.handgunDamageUpgrades < 7 && hasEnoughCredits(handgunDamageCosts[playerData.handgunDamageUpgrades + 1]))
					playerData.handgunDamageUpgrades++;
				break;
			case 1:
				if(playerData.handgunReloadUpgrades < 7 && hasEnoughCredits(handgunReloadCosts[playerData.handgunReloadUpgrades + 1]))
					playerData.handgunReloadUpgrades++;
				break;
			case 2:
				if(playerData.automaticDamageUpgrades < 7 && hasEnoughCredits(automaticDamageCosts[playerData.automaticDamageUpgrades + 1]))
					playerData.automaticDamageUpgrades++;
				break;
			case 3:
				if(playerData.automaticReloadUpgrades < 7 && hasEnoughCredits(automaticReloadCosts[playerData.automaticReloadUpgrades + 1]))
					playerData.automaticReloadUpgrades++;
				break;
			case 4:
				if(playerData.shotgunDamageUpgrades < 7 && hasEnoughCredits(shotgunDamageCosts[playerData.shotgunDamageUpgrades + 1]))
					playerData.shotgunDamageUpgrades++;
				break;
			case 5:
				if(playerData.shotgunReloadUpgrades < 7 && hasEnoughCredits(shotgunReloadCosts[playerData.shotgunReloadUpgrades + 1]))
					playerData.shotgunReloadUpgrades++;
				break;
			case 6:
				if(playerData.moveSpeedUpgrades < 7 && hasEnoughCredits(moveSpeedCosts[playerData.moveSpeedUpgrades + 1]))
					playerData.moveSpeedUpgrades++;
				break;
			case 7:
				if(playerData.creditsMultUpgrades < 7 && hasEnoughCredits(creditsMultCosts[playerData.creditsMultUpgrades + 1]))
					playerData.creditsMultUpgrades++;
				break;
			
		}
		
		RenderUpgrades();
		ApplyUpgrades();
	}
	
	
	// Helper function which checks if user has enough credits.
	// If true, also subtracts that amount from the user's total
	private bool hasEnoughCredits(int numCredits) {
		
		if(playerData.credits >= numCredits) {
			playerData.credits -= numCredits;
			return true;
		}
		
		return false;
	}
	
}


















