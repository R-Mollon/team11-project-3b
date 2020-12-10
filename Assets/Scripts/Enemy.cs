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
	private bool quit = false;
	private PersistantData playerData;
	private float numCredits;

    public float _currentHealth;
	private Animator _animator;

	//Cooldown time between attacks
	private float _attackCooldownTime = 2.0f;
	private float _attackCooldownTimeMain = 2.0f;
	private PersistantData data;
	private Player player;
    
	// Enemy Damage Ammount
	float _damage = 10.0f;
	
	private AudioSource hitSound;
	
	private Rigidbody enemyBody;
	private Rigidbody playerBody;

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
		
		data = GameObject.Find("PersistantData").GetComponent<PersistantData>();
		player = GameObject.Find("Player").GetComponent<Player>();
		
		enemyBody = gameObject.GetComponent<Rigidbody>();
		playerBody = GameObject.Find("Player").GetComponent<Rigidbody>();
		
		hitSound = gameObject.GetComponent<AudioSource>();
    }


    void Update()
    {
     
       var player1 = FindObjectOfType<Player>();
		_animator.SetBool("Attack", false);
		float distance = Vector3.Distance(player1.transform.position, this.transform.position);
		if (distance > 4)
        {
			GetComponent<NavMeshAgent>().SetDestination(player1.transform.position);
        }else
		{
			_animator.SetBool("Attack", true);
			if (_attackCooldownTime > 0)
            {
				_attackCooldownTime -= Time.deltaTime;

			} else
            {
				_attackCooldownTime = _attackCooldownTimeMain;
				AttackTarget();
				 
			}
			
		}
    }
	void AttackTarget()
    {
		var player1 = FindObjectOfType<Player>();
		player1.takeDamage(_damage);
	}

    public void TakeDamage(float damage) {
		
        _currentHealth -= damage;
        //Instantiate(_hitPrefab, transform.position, transform.rotation);
		
		float distance = Vector3.Distance(enemyBody.position, playerBody.position);
		
		hitSound.PlayOneShot(hitSound.clip, 1.0f - (distance / 30));

        if (_currentHealth <= 0.0f && !dead) {
			
			dead = true;
			_animator.SetBool("Death", true);
			Instantiate(_explosionPrefab, transform.position, transform.rotation);

            Destroy(this.gameObject);
			
			GameObject.Find("Player").GetComponent<Player>().creditsDecimal += numCredits;
			
			switch(player.equippedWeapon) {
				case 1:
					data.handgunKills++;
					break;
				case 2:
					data.automaticKills++;
					break;
				case 3:
					data.shotgunKills++;
					break;
				case 4:
					data.swordKills++;
					break;
			}
			
			if(player.gameObject.GetComponent<Rigidbody>().velocity.y != 0.0f)
				data.airKills++;	
        }       
    }
}
