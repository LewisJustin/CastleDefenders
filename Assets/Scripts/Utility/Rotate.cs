using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

    [HideInInspector] public bool aimingReversed;

    private float angle;
    

    void Awake()
    {
        aimingReversed = false;
        angle = 0f;
    }

	void Update ()
	{
        if (Input.GetMouseButton(0) && !transform.GetComponent<BowAI>().AIEnabled)
        {
            Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            if (!aimingReversed)
                angle = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
            if(aimingReversed)
                angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = rotation;
        }
	}
}
