using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistantData : MonoBehaviour
{
    
	public int credits = 0;
	
	/* Permanent Upgrades */
	public int handgunDamage = 1;
	public int handgunDamageUpgrades = 0;
	public int handgunReload = 1;
	public int handgunReloadUpgrades = 0;
	
	public int automaticDamage = 1;
	public int automaticDamageUpgrades = 0;
	public int automaticReload = 1;
	public int automaticReloadUpgrades = 0;
	
	public int shotgunDamage = 1;
	public int shotgunDamageUpgrades = 0;
	public int shotgunReload = 1;
	public int shotgunReloadUpgrades = 0;
	
	public int moveSpeed = 1;
	public int moveSpeedUpgrades = 0;
	public int creditsMult = 1;
	public int creditsMultUpgrades = 0;
	/* ------------------ */
	
	
	// Reset all data
	public void resetData() {
		credits = 0;
		
		handgunDamage = 1;
		handgunDamageUpgrades = 0;
		handgunReload = 1;
		handgunReloadUpgrades = 0;
		
		automaticDamage = 1;
		automaticDamageUpgrades = 0;
		automaticReload = 1;
		automaticReloadUpgrades = 0;
		
		shotgunDamage = 1;
		shotgunDamageUpgrades = 0;
		shotgunReload = 1;
		shotgunReloadUpgrades = 0;
		
		moveSpeed = 1;
		moveSpeedUpgrades = 0;
		creditsMult = 1;
		creditsMultUpgrades = 0;
	}
	
	void Awake() {
		GameObject[] objects = GameObject.FindGameObjectsWithTag("PersistantData");

        if (objects.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
	
}
