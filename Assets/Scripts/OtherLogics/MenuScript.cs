using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour
{

    Animator animator;
    private bool menu;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

	void Update () {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menu = !menu;
           
           StartCoroutine(SetTimeRate());
        }

        if(!menu)
            Time.timeScale = 1f;

        animator.SetBool("Menu", menu);
	}

    IEnumerator SetTimeRate()
    {
         if(menu)
         {
            yield return new WaitForSeconds(.7f);
            Time.timeScale = 0;
         }
    }
}
