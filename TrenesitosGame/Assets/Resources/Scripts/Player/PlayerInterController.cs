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

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Inter"))
        {
            gameObjectInter = other.gameObject;
            canInter = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Inter"))
        {
            canInter = false;
            gameObjectInter = null;
        }
    }
}
