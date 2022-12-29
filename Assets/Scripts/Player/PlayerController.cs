using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private CharacterController controller;

    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float jumpPower;
    private float moveY;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        Vector3 moveInput = Vector3.forward * Input.GetAxis("Vertical") + Vector3.right * Input.GetAxis("Horizontal");


        controller.Move(moveInput * moveSpeed * Time.deltaTime);
    }

    private void Jump()
    {
        moveY += Physics.gravity.y * Time.deltaTime;

        if (Input.GetButtonDown("Jump"))
        {
            moveY = jumpPower;
        }
        else if (controller.isGrounded && moveY < 0)
        {
            moveY = 0;
        }

        controller.Move(Vector3.up * moveY * Time.deltaTime);
    }
}
