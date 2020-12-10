using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public GameObject _hitPrefab;
    public GameObject _explosionPrefab;
    public float _health = 20.0f;
	public bool dead;

	private PersistantData playerData;
	private float numCredits;

    public float _currentHealth;
	public Animator _animator;

	//Cooldown time between attacks
	private float _attackCooldownTime = -0.3f;
	private float _attackCooldownTimeMain = 1.15f;
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

	void AttackTarget()
	{	
		var player1 = FindObjectOfType<Player>();
		float distance = Vector3.Distance(player1.transform.position, this.transform.position);
		
		if (distance < 5)
        {
			player1.takeDamage(_damage);
        }
		
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
		if (distance > 5)
        {
			if (!dead)
            {
				GetComponent<NavMeshAgent>().SetDestination(player1.transform.position);
				_attackCooldownTime = _attackCooldownTimeMain;
            } else
            {
				GetComponent<NavMeshAgent>().SetDestination(transform.position);
			}
			
        }else if (!dead)
		{
			GetComponent<NavMeshAgent>().SetDestination(transform.position);
			_animator.SetBool("Attack", true);

			if (_attackCooldownTime > 0)
            {
				_attackCooldownTime -= Time.deltaTime;

			} else
            {
				_attackCooldownTime = Mathf.Abs(_attackCooldownTime - _attackCooldownTimeMain);
				
				StartCoroutine("Attack");
			}	
		}
    }
	
    public void TakeDamage(float damage) {
		
        _currentHealth -= damage;
        //Instantiate(_hitPrefab, transform.position, transform.rotation);
		
		float distance = Vector3.Distance(enemyBody.position, playerBody.position);
		
		hitSound.PlayOneShot(hitSound.clip, 1.0f - (distance / 30));

        if (_currentHealth <= 0.0f && !dead) {
			
			dead = true;
			gameObject.GetComponent<CapsuleCollider>().enabled = false;
			_animator.SetBool("Death", true);
			

			StartCoroutine("DeathAnimation");
			
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
	IEnumerator DeathAnimation()
    {
		for(int i = 0; i < 1; i++)
        {
			yield return new WaitForSecondsRealtime(5.0f);
        }
		Instantiate(_explosionPrefab, transform.position, transform.rotation);
		Destroy(this.gameObject);
    }
	
	IEnumerator Attack()
    {
		//AttackTarget();
		
		for (int i = 0; i < 1; i++)
        {
			yield return new WaitForSecondsRealtime(0.0f);
        }
		AttackTarget();
    }
}
