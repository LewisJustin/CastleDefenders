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
        }

        animator.SetBool("Menu", menu);
	}
}
