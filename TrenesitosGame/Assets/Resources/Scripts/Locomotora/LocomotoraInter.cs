using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocomotoraInter : MonoBehaviour
{
    private GameObject player;
    private RectTransform slider;

    private bool playerEnLocomotora = false;

    [SerializeField] GameObject posicionPlayer;

    private LocomotoraMovement locomotoraMovement;

    [SerializeField] private GameObject puntoBajarLocomotora;

    [SerializeField] private TeclaInteractuarController teclaInteractuarController;

    private void Awake()
    {
        locomotoraMovement = GetComponentInParent<LocomotoraMovement>();
    }

    private void Start()
    {
        slider = GameObject.Find("SliderAcelerador").GetComponent<RectTransform>();
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        if (playerEnLocomotora)
        {
            if (locomotoraMovement.currentSpeed == 0)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    slider.localScale = new Vector3(0, 0, 0);
                    player.transform.parent = null;
                    player.transform.position = puntoBajarLocomotora.transform.position;
                    player.GetComponent<CharacterController>().enabled = true;
                    player.GetComponent<PlayerMovement>().mov = true;
                    playerEnLocomotora = false;
                    player.GetComponent<PlayerInterController>().interactuando = false;
                }
            }
        }
    }

    public void inter()
    {
        if (!playerEnLocomotora)
        {
            slider.localScale = new Vector3(1, 1, 1);
            player.GetComponent<CharacterController>().enabled = false;
            player.transform.parent = gameObject.transform;
            player.transform.position = posicionPlayer.transform.position;
            player.GetComponent<PlayerMovement>().mov = false;
            player.transform.rotation = Quaternion.Euler(0,90,0);
            playerEnLocomotora = true;
        }
    }

    public void interEnter()
    {
        teclaInteractuarController.mostrarTecla();
    }

    public void interExit()
    {
        teclaInteractuarController.esconderTecla();
    }
}
