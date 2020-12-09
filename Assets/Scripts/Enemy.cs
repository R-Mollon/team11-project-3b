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

    float _currentHealth;

    void OnEnable()
    {
        _currentHealth = _health;
    }


    void Update()
    {
        var player = FindObjectOfType<Player>();
        GetComponent<NavMeshAgent>().SetDestination(player.transform.position);
    }
	
	
    public void TakeDamage(float damage) {
		
        _currentHealth -= damage;
        //Instantiate(_hitPrefab, transform.position, transform.rotation);

        if (_currentHealth <= 0.0f)
        {
            Instantiate(_explosionPrefab, transform.position, transform.rotation);
            gameObject.SetActive(false);
        }
            
    }
}
