using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAtackController : MonoBehaviour
{

    private Animator animator;
    private PlayerMovement playerMovement;

    private bool atacando = false;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (!atacando && playerMovement.mov)
        {
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                playerMovement.mov = false;
                
                animator.SetTrigger("atacar");
                
                atacando = true;
            }
        }
    }

    public void setAtacandoFalse()
    {
        playerMovement.mov = true;
        atacando = false;
    }
}
