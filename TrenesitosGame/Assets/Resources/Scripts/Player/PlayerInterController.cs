using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInterController : MonoBehaviour
{
    private GameObject gameObjectInter;
    public bool canInter = false;
    public bool interactuando = false;

    private void Update()
    {
        if (canInter && !interactuando)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                interactuando = true;
                gameObjectInter.SendMessage("inter");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Inter"))
        {
            gameObjectInter = other.gameObject;
            
            gameObjectInter.SendMessage("interEnter");
            
            canInter = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Inter"))
        {
            gameObjectInter.SendMessage("interExit");
            canInter = false;
            gameObjectInter = null;
        }
    }
}
