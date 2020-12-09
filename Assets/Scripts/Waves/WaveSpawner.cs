using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaveSpawner : MonoBehaviour {
   
   
	public void spawnEnemy(float health) {
		
		// Spawn new skeleton enemy
		GameObject enemy = Instantiate(Resources.Load("Prefabs/Skeleton")) as GameObject;
		
		// Set enemies object as enemy parent to keep object hierarchy clean
		enemy.transform.parent = GameObject.Find("Enemies").transform;
		
		// Disable navmesh agent
		enemy.GetComponent<NavMeshAgent>().enabled = false;
		
		// Move enemy to spawner position
		enemy.transform.position = transform.position;
		
		// Enable navmesh agent
		enemy.GetComponent<NavMeshAgent>().enabled = true;
		
		// Get enemy script and set health to given health
		Enemy enemyScript = enemy.GetComponent<Enemy>();
		enemyScript._health = health;
		enemyScript._currentHealth = health;
	}
   
   
}
