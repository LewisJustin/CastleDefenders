﻿using UnityEngine;
using System.Collections;

public class EnemyLogic : MonoBehaviour
{

	#region SerializeVariables
	[SerializeField] public bool ranged;
	public float speed;
	[SerializeField] private Transform coinDrop;
	[SerializeField] private Transform powerUp;
	[SerializeField] private Rigidbody2D archerArrow;
    [SerializeField] public int damage;
    [SerializeField] public int health;
    #endregion

    #region variables
    [HideInInspector]public Animator animator;
    [HideInInspector]private GameObject GameManager;
	[HideInInspector]public bool canMove = true;
	[HideInInspector]public bool hasArrived;
    [HideInInspector]private float OriginalSpeed;
	public float stoppingDistanceRanged;
	public float stoppingDistanceMelee;
	#endregion

	public enum EnemyState {MOVING, WAITING, ATTACKING}

	public EnemyState state = EnemyState.MOVING;

	[HideInInspector] public bool goingRight;

	private bool delay;

	private bool startedAttacking;

	private void Awake()
	{
		hasArrived = false;
        OriginalSpeed = speed;

        GameManager = GameObject.Find("GameManager");
        animator = GetComponent<Animator>();    
		canMove = true; 
		stoppingDistanceRanged = 3f;
		stoppingDistanceMelee = .25f;

		startedAttacking = false;
	}

	public void SetDirection()
	{
		if (transform.position.x < -35)
			goingRight = true;
		else
			goingRight = false;			
	}

	void Update()
	{
		//prevents bunching at spawn and makes them face correct direction
		if (goingRight)
		{
			if (transform.position.x < -35)
				speed = 20f;
			else if (transform.position.x >= -35)
				speed = OriginalSpeed;

			transform.GetComponent<SpriteRenderer>().flipX = false;
		}
		else
		{
			if (transform.position.x > 35)
				speed = 20f;
			else if (transform.position.x <= 35)
				speed = OriginalSpeed;

			transform.GetComponent<SpriteRenderer>().flipX = true;
		}
		

		if (health <= 0)
		{
			GameManager.GetComponent<WaveSpawner>().enemiesInThisWave--;

			if(Random.Range(0, 2) == 1)
			{
				DropCoin();
			}

			if(Random.Range(0, 5) == 1)
			{
				DropPowerUp();
			}

            Destroy(gameObject);	
                
        }

		#region GettingToLocation
		if(!hasArrived)
		{
			if(canMove)
			{
				if (goingRight)
					transform.Translate(Vector3.right * speed * Time.deltaTime);
				else
					transform.Translate(Vector3.left * speed * Time.deltaTime);
				state = EnemyState.MOVING;
			}
			else
			{
				state = EnemyState.WAITING;
			}
		}
		if(hasArrived && !startedAttacking)
		{
			startedAttacking = true;

			state = EnemyState.ATTACKING;
			if(!ranged)
			{
				StartCoroutine(StartAttackingMelee());
			}
			else
			{
				StartCoroutine(StartAttackingRanged());
			}
		}
		#endregion

		if(state == EnemyState.WAITING)
		{
			animator.SetBool("startWaiting", true);
			animator.SetBool("startAttacking", false);
			animator.SetBool("startRunning", false);
		}
		else if(state == EnemyState.ATTACKING)
		{
			animator.SetBool("startWaiting", false);
			animator.SetBool("startAttacking", true);
			animator.SetBool("startRunning", false);
		}
		else if (state == EnemyState.MOVING)
		{
			animator.SetBool("startWaiting", false);
			animator.SetBool("startAttacking", false);
			animator.SetBool("startRunning", true);
		}
		else
			Debug.LogWarning("Something went wrong");
	}
	
	
	IEnumerator StartAttackingMelee()
	{
		while (health > 0)
		{
			GameManager.GetComponent<GameLogic>().castleTakeDamage(damage);

			yield return new WaitForSeconds(3f);
		}
	}

	IEnumerator StartAttackingRanged()
	{
        while (health > 0)
		{
			if(goingRight)
			{
				Rigidbody2D newArrow = Instantiate(archerArrow, new Vector3 (transform.position.x + .5f,-3,0), Quaternion.Euler(transform.rotation.x, transform.rotation.y - 180f, transform.rotation.z));

				newArrow.name = "Enemy Arrow";

				newArrow.velocity = transform.TransformDirection(Vector3.right * 20);

				GameManager.GetComponent<GameLogic>().castleTakeDamage(damage);

				yield return new WaitForSeconds(3f);
			}
			else
			{
				Rigidbody2D newArrow = Instantiate(archerArrow, new Vector3 (transform.position.x - .5f,-3,0), Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z));

				newArrow.name = "Enemy Arrow";

				newArrow.velocity = transform.TransformDirection(Vector3.left * 20);

				GameManager.GetComponent<GameLogic>().castleTakeDamage(damage);

				yield return new WaitForSeconds(3f);
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.name == "Arrow")
			health -= collision.gameObject.GetComponent<ArrowLogic>().damage;
	}

	private void DropCoin()
	{
		Instantiate(coinDrop, transform.position, transform.rotation);
	}

	private void DropPowerUp()
	{
		Instantiate(powerUp, transform.position, transform.rotation);
	}

}
