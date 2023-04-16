using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAtackController : MonoBehaviour
{

    [SerializeField] private BoxCollider boxColliderAtaque;
    
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            boxColliderAtaque.enabled = false;
        }
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            boxColliderAtaque.enabled = true;
        }
    }
}
