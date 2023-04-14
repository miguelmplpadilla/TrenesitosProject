using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;

    public float speed = 2;

    private float targetAngle = 0;
    private float angle = 0;
    private float smoothTime = 0.05f;
    private float currentVelocity;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (move.x != 0 || move.z != 0)
        {
            targetAngle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg;
            angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentVelocity, smoothTime);
        }

        transform.rotation = Quaternion.Euler(0, angle,0);
            
        characterController.Move(move * Time.deltaTime * speed);
    }
}
