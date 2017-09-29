using UnityEngine;
using System.Collections;

public class EnemyLogic : MonoBehaviour
{

	#region SerializeVariables
	[SerializeField] public bool ranged;
	[SerializeField] private float speed;
	[SerializeField] private Transform coinDrop;
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

	[HideInInspector] public float stoppingDistanceRanged;
	[HideInInspector] public float stoppingDistanceMelee;
	[HideInInspector] public float stoppingDistanceRangedLeft;
	[HideInInspector] public float stoppingDistanceMeleeLeft;
	#endregion

	public enum EnemyState {MOVING, WAITING, ATTACKING}

	public EnemyState state = EnemyState.MOVING;

	[HideInInspector] public bool goingRight;

	private bool delay;

	private void Awake()
	{
		hasArrived = false;
        OriginalSpeed = speed;

        GameManager = GameObject.Find("GameManager");
        animator = GetComponent<Animator>();    
		canMove = true; 

		stoppingDistanceRanged = 1f;
		stoppingDistanceMelee = 0f;
		stoppingDistanceRangedLeft = 3f;
		stoppingDistanceMeleeLeft = 0f;
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
		//prevents bunching at spawn
		if (goingRight)
		{
			if (transform.position.x < -35)
				speed = 20f;
			else if (transform.position.x >= -35)
				speed = OriginalSpeed;

			transform.GetComponent<SpriteRenderer>().flipX = false;
		}
		if(!goingRight)
		{
			if (transform.position.x > 30)
				speed = 20f;
			else if (transform.position.x <= 30)
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

            Destroy(gameObject);	
                
        }

		#region GettingToLocation
		if(!hasArrived && !ranged )
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
		else
		{
			state = EnemyState.ATTACKING;
			StartCoroutine(StartAttackingMelee());
		}

		if (ranged && !hasArrived && goingRight)
		{
			if (transform.position.x < 1f)
			{
				if(canMove)
				{
					transform.Translate(Vector3.right * speed * Time.deltaTime);
					state = EnemyState.MOVING;
				}
				else
				{
					state = EnemyState.WAITING;
				}
			}
			else
			{
				StartCoroutine(StartAttackingRanged());
				hasArrived = true;
				state = EnemyState.ATTACKING;
			}
		}

		if (ranged && !hasArrived && !goingRight)
		{
			if (transform.position.x > 14f)
			{
				if(canMove)
				{
					transform.Translate(Vector3.left * speed * Time.deltaTime);
					state = EnemyState.MOVING;
				}
				else
				{
					state = EnemyState.WAITING;
				}
			}
			else
			{
				StartCoroutine(StartAttackingRanged());
				hasArrived = true;
				state = EnemyState.ATTACKING;
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
				Rigidbody2D newArrow = Instantiate(archerArrow, new Vector3 (3,-3,0), Quaternion.Euler(transform.rotation.x, transform.rotation.y - 180f, transform.rotation.z));

				newArrow.name = "Enemy Arrow";

				newArrow.velocity = transform.TransformDirection(Vector3.right * 20);

				GameManager.GetComponent<GameLogic>().castleTakeDamage(damage);

				yield return new WaitForSeconds(3f);
			}
			else
			{
				Rigidbody2D newArrow = Instantiate(archerArrow, new Vector3 (13.5f,-3,0), Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z));

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

}
