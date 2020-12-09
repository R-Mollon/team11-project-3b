﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject _hitPrefab;
    [SerializeField] GameObject _explosionPrefab;
    [SerializeField] int _health = 3;

    int _currentHealth;

    void OnEnable()
    {
        _currentHealth = _health;
    }


    void Update()
    {
        var player = FindObjectOfType<Player>();
        GetComponent<NavMeshAgent>().SetDestination(player.transform.position);
    }
    public void TakeDamage()
    {
        _currentHealth--;
        //Instantiate(_hitPrefab, transform.position, transform.rotation);

        if (_currentHealth <= 0)
        {
            Instantiate(_explosionPrefab, transform.position, transform.rotation);
            gameObject.SetActive(false);
        }
            
    }
}
