using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocomotoraInter : MonoBehaviour
{
    private GameObject player;

    private bool playerEnLocomotora = false;

    [SerializeField] GameObject posicionPlayer;

    private LocomotoraMovement locomotoraMovement;

    private void Awake()
    {
        locomotoraMovement = GetComponentInParent<LocomotoraMovement>();
    }

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        if (playerEnLocomotora)
        {
            if (locomotoraMovement.currentSpeed > 0)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    player.GetComponent<CharacterController>().enabled = true;
                    player.transform.parent = null;
                    player.GetComponent<PlayerMovement>().mov = true;
                    player.GetComponent<PlayerInterController>().interactuando = false;
                    playerEnLocomotora = false;
                }
            }
        }
    }

    public void inter()
    {
        player.GetComponent<CharacterController>().enabled = false;
        player.transform.parent = gameObject.transform;
        player.transform.position = posicionPlayer.transform.position;
        player.GetComponent<PlayerMovement>().mov = false;
        player.transform.rotation = Quaternion.Euler(0,90,0);
        playerEnLocomotora = true;
    }
}
