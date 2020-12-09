using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class WaveManager : MonoBehaviour {
	
	private WaveSpawner[] spawners;
	
	private Text waveNumText;
	private Text newWaveText;
	private Text remainingText;
	
	private int waveNum;
	
	private bool doNextWave;
	
	private int enemiesInWave;
	private float enemyHealth;
	
	
	void Start() {
		
		spawners = GameObject.Find("Spawners").GetComponentsInChildren<WaveSpawner>();
		waveNumText = GameObject.Find("HUD/WaveNum/WaveNum").GetComponent<Text>();
		newWaveText = GameObject.Find("HUD/WaveNum/NewWave").GetComponent<Text>();
		remainingText = GameObject.Find("HUD/WaveNum/RemainingEnemies").GetComponent<Text>();
		
		waveNum = 0;
		doNextWave = true;
		enemiesInWave = 0;
		enemyHealth = 0.0f;
		
		StartCoroutine("doWaves");
		
	}
	
	
	IEnumerator doWaves() {
		
		while(true) {
			
			if(doNextWave) {
				doNextWave = false;
				
				waveNum++;
				waveNumText.text = waveNum.ToString();
				
				enemiesInWave = waveNum + 4;
				
				enemyHealth = Mathf.Sqrt(waveNum) * 10.0f;
				
				StartCoroutine("doWaveText");
				
				yield return new WaitForSecondsRealtime(2.5f);
			}
			
			remainingText.text = "Enemies Remaining: " + (enemiesInWave + GameObject.Find("Enemies").transform.childCount).ToString();
			
			if(enemiesInWave > 0) {
				spawners[(int) Math.Floor(UnityEngine.Random.value * spawners.Length)].spawnEnemy(enemyHealth);
				enemiesInWave--;
			} else {
				if(GameObject.Find("Enemies").transform.childCount == 0) {
					// Wave is over
					doNextWave = true;
					yield return new WaitForSecondsRealtime(5.0f);
				}
			}
			
			
			yield return new WaitForSecondsRealtime(0.75f);
			
		}
		
	}
	
	IEnumerator doWaveText() {
		
		newWaveText.text = "WAVE " + waveNum;
		
		
		
		for(int i = 255; i > 0; i--) {
			Color textColor = new Color(i / 255.0f, 0, 0, i / 255.0f);
			
			newWaveText.color = textColor;
			
			yield return new WaitForSecondsRealtime(0.01f);
		}
		
	}
	
}














