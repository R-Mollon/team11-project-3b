using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {
	
	private Camera mainCamera;
	
	private int position;
	private int vertPosition;
	private float panTarget;
	
	private PersistantData data;
	
	private Text challengeOne;
	private Text challengeOneReward;
	private Text challengeOneProgress;
	
	private Text challengeTwo;
	private Text challengeTwoReward;
	private Text challengeTwoProgress;
	
	private Text challengeThree;
	private Text challengeThreeReward;
	private Text challengeThreeProgress;
	
	
	void Start() {
		
		mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
		
		position = 0;
		vertPosition = 0;
		
	}
	
	void Awake() {
		data = GameObject.Find("PersistantData").GetComponent<PersistantData>();
		
		challengeOne = GameObject.Find("New Game/Challenges/Background/Challenge1Box/Challenge").GetComponent<Text>();
		challengeOneReward = GameObject.Find("New Game/Challenges/Background/Challenge1Box/Reward").GetComponent<Text>();
		challengeOneProgress = GameObject.Find("New Game/Challenges/Background/Challenge1Box/Progress").GetComponent<Text>();
		
		challengeTwo = GameObject.Find("New Game/Challenges/Background/Challenge2Box/Challenge").GetComponent<Text>();
		challengeTwoReward = GameObject.Find("New Game/Challenges/Background/Challenge2Box/Reward").GetComponent<Text>();
		challengeTwoProgress = GameObject.Find("New Game/Challenges/Background/Challenge2Box/Progress").GetComponent<Text>();
		
		challengeThree = GameObject.Find("New Game/Challenges/Background/Challenge3Box/Challenge").GetComponent<Text>();
		challengeThreeReward = GameObject.Find("New Game/Challenges/Background/Challenge3Box/Reward").GetComponent<Text>();
		challengeThreeProgress = GameObject.Find("New Game/Challenges/Background/Challenge3Box/Progress").GetComponent<Text>();
	}
	
	void Update() {
		
		challengeOne.text = data.challenges[data.currentChallengeOne].description;
		challengeOneReward.text = "Reward: §" + data.challenges[data.currentChallengeOne].reward.ToString();
		
		// Challenge 1 progress
		int amountNeeded = (data.currentChallengeOne % 3) * 20 + 20;
		int amountHas = 0;
		
		switch(data.currentChallengeOne) {
			case 0: case 1: case 2:
				amountHas = data.handgunKills;
				break;
			case 3: case 4: case 5:
				amountHas = data.automaticKills;
				break;
			case 6: case 7: case 8:
				amountHas = data.shotgunKills;
				break;
			case 9: case 10: case 11:
				amountHas = data.swordKills;
				break;
			case 12: case 13: case 14:
				amountHas = data.airKills;
				break;
		}
		
		challengeOneProgress.text = "Progress: " + amountHas + "/" + amountNeeded;
		
		
		challengeTwo.text = data.challenges[data.currentChallengeTwo].description;
		challengeTwoReward.text = "Reward: §" + data.challenges[data.currentChallengeTwo].reward.ToString();
		
		// Challenge 2 progress
		amountNeeded = (data.currentChallengeTwo % 3) * 20 + 20;
		amountHas = 0;
		
		switch(data.currentChallengeTwo) {
			case 0: case 1: case 2:
				amountHas = data.handgunKills;
				break;
			case 3: case 4: case 5:
				amountHas = data.automaticKills;
				break;
			case 6: case 7: case 8:
				amountHas = data.shotgunKills;
				break;
			case 9: case 10: case 11:
				amountHas = data.swordKills;
				break;
			case 12: case 13: case 14:
				amountHas = data.airKills;
				break;
		}
		
		challengeTwoProgress.text = "Progress: " + amountHas + "/" + amountNeeded;
		
		
		challengeThree.text = data.challenges[data.currentChallengeThree].description;
		challengeThreeReward.text = "Reward: §" + data.challenges[data.currentChallengeThree].reward.ToString();
		
		// Challenge 3 progress
		amountNeeded = (data.currentChallengeThree % 3) * 20 + 20;
		amountHas = 0;
		
		switch(data.currentChallengeThree) {
			case 0: case 1: case 2:
				amountHas = data.handgunKills;
				break;
			case 3: case 4: case 5:
				amountHas = data.automaticKills;
				break;
			case 6: case 7: case 8:
				amountHas = data.shotgunKills;
				break;
			case 9: case 10: case 11:
				amountHas = data.swordKills;
				break;
			case 12: case 13: case 14:
				amountHas = data.airKills;
				break;
		}
		
		challengeThreeProgress.text = "Progress: " + amountHas + "/" + amountNeeded;
		
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
	
	public void startGame() {
		SceneManager.LoadScene(1);
	}
	
	public void doCredits() {
		SceneManager.LoadScene(2);
	}
	
	public void quitGame(int ignoreLevel) {

		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
		#else
			Application.Quit();
		#endif
    }
	
}
