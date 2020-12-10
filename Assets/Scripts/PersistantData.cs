using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PersistantData : MonoBehaviour
{
    
	public int credits = 0;
	
	/* Permanent Upgrades */
	public float handgunDamage = 1;
	public int handgunDamageUpgrades = 0;
	public float handgunReload = 1;
	public int handgunReloadUpgrades = 0;
	
	public float automaticDamage = 1;
	public int automaticDamageUpgrades = 0;
	public float automaticReload = 1;
	public int automaticReloadUpgrades = 0;
	
	public float shotgunDamage = 1;
	public int shotgunDamageUpgrades = 0;
	public float shotgunReload = 1;
	public int shotgunReloadUpgrades = 0;
	
	public float moveSpeed = 1;
	public int moveSpeedUpgrades = 0;
	public float creditsMult = 1;
	public int creditsMultUpgrades = 0;
	/* ------------------ */
	
	/* Current Challenges */
	public int currentChallengeOne = 0;
	public int currentChallengeTwo = 0;
	public int currentChallengeThree = 0;
	
	public int handgunKills = 0;
	public int automaticKills = 0;
	public int shotgunKills = 0;
	public int swordKills = 0;
	public int airKills = 0;
	/* ------------------ */
	
	/* Challenge List */
	public Challenge[] challenges = new[] {
		new Challenge() { description = "Kill 20 enemies with the handgun", reward = 20},
		new Challenge() { description = "Kill 40 enemies with the handgun", reward = 40},
		new Challenge() { description = "Kill 60 enemies with the handgun", reward = 60},
		
		new Challenge() { description = "Kill 20 enemies with the M4", reward = 10},
		new Challenge() { description = "Kill 40 enemies with the M4", reward = 20},
		new Challenge() { description = "Kill 60 enemies with the M4", reward = 30},
		
		new Challenge() { description = "Kill 20 enemies with the Shotgun", reward = 8},
		new Challenge() { description = "Kill 40 enemies with the Shotgun", reward = 16},
		new Challenge() { description = "Kill 60 enemies with the Shotgun", reward = 32},
		
		new Challenge() { description = "Kill 20 enemies with the Sword", reward = 20},
		new Challenge() { description = "Kill 40 enemies with the Sword", reward = 40},
		new Challenge() { description = "Kill 60 enemies with the Sword", reward = 60},
		
		new Challenge() { description = "Kill 20 enemies while airborne", reward = 30},
		new Challenge() { description = "Kill 40 enemies while airborne", reward = 60},
		new Challenge() { description = "Kill 60 enemies while airborne", reward = 80},
	};
	/* -------------- */
	
	
	public void rollChallenge(int challenge) {
		
		switch(challenge) {
			case 1:
				while(currentChallengeOne == currentChallengeTwo || currentChallengeOne == currentChallengeThree)
					currentChallengeOne   = (int) Math.Floor(UnityEngine.Random.value * challenges.Length);
				break;
			case 2:
				while(currentChallengeTwo == currentChallengeOne || currentChallengeTwo == currentChallengeThree)
					currentChallengeTwo   = (int) Math.Floor(UnityEngine.Random.value * challenges.Length);
				break;
			case 3:
				while(currentChallengeThree == currentChallengeOne || currentChallengeThree == currentChallengeTwo)
					currentChallengeThree = (int) Math.Floor(UnityEngine.Random.value * challenges.Length);
				break;
		}
		
	}
	
	
	public void Start() {
		rollChallenge(1);
		rollChallenge(2);
		rollChallenge(3);
	}
	
	
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
	
	public class Challenge {
		public string description;
		public int reward;
	}
	
}


