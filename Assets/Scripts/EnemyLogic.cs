using UnityEngine;
using System.Collections;

public class EnemyLogic : MonoBehaviour
{

	#region SerializeVariables
	[SerializeField] private bool ranged;
	[SerializeField] private GameObject GameManager;
	[SerializeField] private float speed;
	[SerializeField] private int arrowDamage;
    [SerializeField] public Animator animator;
	[SerializeField] private Transform coinDrop;
	#endregion

	#region variables
	private int damage;

	[HideInInspector]public int health;
	[HideInInspector] public bool canMove = true;
	[HideInInspector] public bool hasArrived;
	#endregion
	

	private void Awake()
	{
		health = 100;
		speed = 2.5f;
		hasArrived = false;
		damage = 10;

		GameManager = GameObject.Find("GameManager");

        animator = GetComponent<Animator>();
        animator.SetBool("isAtTarget", false);
	}

	void Update()
	{
		if (health <= 0)
		{
			GameManager.GetComponent<WaveSpawner>().enemiesInThisWave--;

			if(Random.Range(0, 5) == 1)
			{
				DropCoin();
			}

            Destroy(gameObject);	
                
        }

		#region GettingToLocation
		if (!ranged && !hasArrived)
		{
			if (transform.position.x < 5)
			{
				if(canMove)
					transform.Translate(Vector3.right * speed * Time.deltaTime);
			}
			else
			{
				StartCoroutine(StartAttackingMelee());
				hasArrived = true;
			}
		}

		if (ranged && !hasArrived)
		{
			if (transform.position.x < Random.Range(.8f, 1.2f))
			{
				if(canMove)
					transform.Translate(Vector3.right * speed * Time.deltaTime);
			}
			else
			{
				StartCoroutine(StartAttackingRanged());
				hasArrived = true;
			}
		}
		#endregion
	}

	IEnumerator StartAttackingMelee()
	{
        animator.SetBool("isAtTarget", true);
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
