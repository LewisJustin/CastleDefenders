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
	[SerializeField] private Rigidbody2D archerArrow;
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
		hasArrived = false;
		damage = 10;

		GameManager = GameObject.Find("GameManager");

        animator = GetComponent<Animator>();
        animator.SetBool("isAtTarget", false);
	}

	void Update()
	{
		//prevents bunching at spawn
		if (transform.position.x < -35)
			speed = 20f;
		else if (transform.position.x >= -35)
			speed = 10f;

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
