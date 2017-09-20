using UnityEngine;
using System.Collections;

public class EnemyLogic : MonoBehaviour
{

	#region SerializeVariables
	[SerializeField] private bool ranged;
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
	#endregion

	public enum EnemyState {MOVING, WAITING, ATTACKING}

	private EnemyState state = EnemyState.MOVING;
	private EnemyState tempState;

	private void Awake()
	{
		hasArrived = false;
        OriginalSpeed = speed;

        GameManager = GameObject.Find("GameManager");
        animator = GetComponent<Animator>();        
	}

	void Update()
	{
		//prevents bunching at spawn
		if (transform.position.x < -35)
			speed = 20f;
		else if (transform.position.x >= -35)
			speed = OriginalSpeed;

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
		if (!ranged && !hasArrived)
		{
			if (transform.position.x < 5.1f)
			{
				if(canMove)
				{
					state = EnemyState.MOVING;
					transform.Translate(Vector3.right * speed * Time.deltaTime);
					ChangeAttackingState(state);
				}
				else
				{
					state = EnemyState.WAITING;
					ChangeAttackingState(state);
				}
			}
			else
			{
				StartCoroutine(StartAttackingMelee());
				hasArrived = true;
				state = EnemyState.ATTACKING;
				ChangeAttackingState(state);
			}
		}

		if (ranged && !hasArrived)
		{
			if (transform.position.x < Random.Range(.8f, 1.2f))
			{
				if(canMove)
				{
					transform.Translate(Vector3.right * speed * Time.deltaTime);
					state = EnemyState.MOVING;
					ChangeAttackingState(state);
				}
				else
				{
					state = EnemyState.WAITING;
					ChangeAttackingState(state);
				}
			}
			else
			{
				StartCoroutine(StartAttackingRanged());
				hasArrived = true;
				state = EnemyState.ATTACKING;
				ChangeAttackingState(state);
			}
		}
		#endregion
	}

	private void ChangeAttackingState(EnemyState _state)
	{
		if(_state == EnemyState.WAITING)
		{
			animator.SetBool("startWaiting", true);
			animator.SetBool("startAttacking", false);
			animator.SetBool("startRunning", false);
			animator.SetBool("isAttacking", false);
			animator.SetBool("isRunning", false);
		}
		else if(_state == EnemyState.ATTACKING)
		{
			animator.SetBool("startWaiting", false);
			animator.SetBool("startAttacking", true);
			animator.SetBool("startRunning", false);
			animator.SetBool("isAttacking", true);
			animator.SetBool("isRunning", false);
		}
		else if (_state == EnemyState.MOVING)
		{
			animator.SetBool("startWaiting", false);
			animator.SetBool("startAttacking", false);
			animator.SetBool("startRunning", true);
			animator.SetBool("isAttacking", false);
			animator.SetBool("isRunning", true);
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
			Rigidbody2D newArrow = Instantiate(archerArrow, new Vector3 (3,-3,0), Quaternion.Euler(transform.rotation.x, transform.rotation.y - 180f, transform.rotation.z));

			newArrow.name = "Enemy Arrow";

			newArrow.velocity = transform.TransformDirection(Vector3.right * 20);

			GameManager.GetComponent<GameLogic>().castleTakeDamage(damage);

			yield return new WaitForSeconds(3f);
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
