using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitBoxDetector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HurtBoxEnemigo"))
        {
            other.SendMessage("hurt");
            GetComponent<BoxCollider>().enabled = false;
        }
    }
}
