using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleClick : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnMouseDown()
    {
        animator.SetTrigger("Click");
    }
}
