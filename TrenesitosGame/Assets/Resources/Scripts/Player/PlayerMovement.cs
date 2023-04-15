using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    private Animator animator;

    public bool mov = true;

    [SerializeField] private float speed = 2;
    [SerializeField] private float maxSpeed = 4;
    [SerializeField] private float maxSpeedSprint = 6;
    [SerializeField] private float speedMultiplier = 2;

    private float targetAngle = 0;
    [SerializeField] private float angle = 0;
    private float smoothTime = 0.05f;
    private float currentVelocity;

    private Vector3 direction;
    [SerializeField] private float currentSpeed;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        animator.SetFloat("speed", currentSpeed);
        
        if (mov)
        {
            direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

            if (direction.x != 0 || direction.z != 0)
            {
                if (currentSpeed < speed)
                {
                    currentSpeed += speedMultiplier * Time.deltaTime;
                }
                else
                {
                    if (currentSpeed > speed)
                    {
                        currentSpeed -= speedMultiplier * Time.deltaTime;
                    }
                    else
                    {
                        currentSpeed = speed;
                    }
                }
            }
            else
            {
                if (currentSpeed > 0)
                {
                    currentSpeed -= speedMultiplier * Time.deltaTime;
                }
                else
                {
                    currentSpeed = 0;
                }
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = maxSpeedSprint;
            }
            else
            {
                speed = maxSpeed;
            }

            applyRotation();
            applyMovement();
        }
        else
        {
            currentSpeed = 0;
        }
    }
    
    private void applyRotation()
    {
        if (direction.x != 0 || direction.z != 0)
        {
            targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentVelocity, smoothTime);
        }
        
        transform.rotation = Quaternion.Euler(0, angle,0);
    }

    private void applyMovement()
    {
        characterController.Move(direction.normalized * Time.deltaTime * currentSpeed);
    }
}
