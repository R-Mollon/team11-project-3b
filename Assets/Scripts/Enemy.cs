using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject _hitPrefab;
    [SerializeField] GameObject _explosionPrefab;
    [SerializeField] float _health = 20.0f;
	
	private bool dead;
	
	private PersistantData playerData;
	private float numCredits;

    float _currentHealth;

    void OnEnable()
    {
        _currentHealth = _health;
		dead = false;
		
		numCredits = 1.0f;
		GameObject playerDataObject = GameObject.Find("PersistantData");
		if(playerDataObject != null) {
			playerData = playerDataObject.GetComponent<PersistantData>();
			
			numCredits = playerData.creditsMult;
		}
    }


    void Update()
    {
        var player = FindObjectOfType<Player>();
        GetComponent<NavMeshAgent>().SetDestination(player.transform.position);
    }
	
	
    public void TakeDamage(float damage) {
		
        _currentHealth -= damage;
        //Instantiate(_hitPrefab, transform.position, transform.rotation);

        if (_currentHealth <= 0.0f && !dead) {
			
			dead = true;
            Instantiate(_explosionPrefab, transform.position, transform.rotation);
            Destroy(this.gameObject);
			
			GameObject.Find("Player").GetComponent<Player>().creditsDecimal += numCredits;
			
        }
            
    }
}
