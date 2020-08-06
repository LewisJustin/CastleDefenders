using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BowAI : MonoBehaviour {

	[SerializeField] Transform enemyParent; 
	[HideInInspector] public bool AIEnabled;
	public Transform target;
	private float angle;
	public float offset;
	public float offset2;//

	void Start()
	{
		StartCoroutine(pullArrow());
		AIEnabled = false;
	}

	void Update ()
	{
		if(Input.GetKeyDown("tab"))
		{
			AIEnabled = !AIEnabled;
		}

		if(AIEnabled && enemyParent.transform.childCount > 0)
        {
			target = enemyParent.gameObject.transform.GetChild(0);
            Vector2 direction = target.position - transform.position;

			if(enemyParent.transform.GetChild(0).GetComponent<EnemyLogic>().goingRight)
            	angle = Mathf.Atan2(direction.y, direction.x + -1.4f) * Mathf.Rad2Deg;
			else
				angle = Mathf.Atan2(direction.y, direction.x + .7f) * Mathf.Rad2Deg;

            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = rotation;
        }
	}

	private IEnumerator pullArrow()
	{
		bool makeItGoForever = true;
		while(makeItGoForever)
		{
			while(AIEnabled && enemyParent.transform.childCount > 0)
			{
				transform.GetComponent<BowLogic>().animator.SetBool("isCharging", true);

				transform.GetComponent<BowLogic>().canShoot = false;

				yield return new WaitForSeconds(2f);

				transform.GetComponent<BowLogic>().Shoot();

				transform.GetComponent<BowLogic>().canShoot = false;

				transform.GetComponent<BowLogic>().animator.SetBool("isCharging", false);

				yield return new WaitForSeconds(transform.GetComponent<BowLogic>().bowDrawSpeed);
			}
			yield return new WaitForSeconds(2f);
		}
	}
}