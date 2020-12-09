using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    [SerializeField] GameObject _hitPrefab;
    [SerializeField] GameObject _explosionPrefab;
    public float _health = 20.0f;

	
	public bool dead;
	
	private PersistantData playerData;
	private float numCredits;

    public float _currentHealth;

	private Animator _animator;
	private bool _distanceCheck = false;
	private float _attackTime = 1.0f;

	float _damage = 10.0f;

	void Start()
    {
		_animator = GetComponent<Animator>();
    }

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

		float distance = Vector3.Distance(player.transform.position, this.transform.position);
		if (distance < 5.0f)
        {
			if (!_distanceCheck)
            {
				_distanceCheck = true;
            } else
            {
				_attackTime -= Time.deltaTime;
            }
			if (_attackTime <= 0.0f)
            {
				//Attack
				_animator.SetBool("Attack", true);
            }

        } else
        {
			_animator.SetBool("Attack", false);
			_distanceCheck = false;
			_attackTime = 1.0f;
        }

		
    }

	// Animation event
	public void AttackEnd()
    {
		// send damage to the player
		//PlayerController.Instance.OnHit(this.gameObject, 35);
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
	void OnCollisionEnter(
		Collision collision)
	{

		var player = collision.collider.GetComponent<Player>();
		if (player != null)
		{
			player.takeDamage(_damage);

		}
	}


}
