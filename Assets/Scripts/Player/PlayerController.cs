using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PlayerState { Normal, Combat}
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Animator anim;

    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float jumpPower;
    [SerializeField]
    private float rotateSpeed;
    private float moveY;

    private PlayerState state;
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        state = PlayerState.Normal;
    }

    private void Update()
    {
        switch (state)
        {
            case PlayerState.Normal:
                Move();
                Rotate();
                Animation();
                Jump();
                break;
            case PlayerState.Combat:
                Move();
                Rotate();
                Jump();
                Attack();
                Animation();
                break;
        }

    }

    private void Attack()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetTrigger("Attack");
        }
    }
    private void Animation()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (!(horizontal == 0 && vertical == 0))
        {
            anim.SetBool("isRun", true);
        }
        else 
        {
            anim.SetBool("isRun", false);
        }
    }

    private void Move()
    {
        Vector3 fowardVec = new Vector3(Camera.main.transform.forward.x, 0f, Camera.main.transform.forward.z).normalized;
        Vector3 rightVec = new Vector3(Camera.main.transform.right.x, 0f, Camera.main.transform.right.z).normalized;

        Vector3 moveInput = Vector3.forward * Input.GetAxis("Vertical") + Vector3.right * Input.GetAxis("Horizontal");
        if (moveInput.sqrMagnitude > 1f) moveInput.Normalize();

        Vector3 moveVec = fowardVec * moveInput.z + rightVec * moveInput.x;

        controller.Move(moveVec * moveSpeed * Time.deltaTime);
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

    private void Rotate()
    {
        Vector3 fowardVec = new Vector3(Camera.main.transform.forward.x, 0f, Camera.main.transform.forward.z).normalized;
        Vector3 rightVec = new Vector3(Camera.main.transform.right.x, 0f, Camera.main.transform.right.z).normalized;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(horizontal, 0, vertical);

        Vector3 rotateVec = fowardVec * dir.z + rightVec * dir.x;

        if (!(horizontal == 0 && vertical == 0))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(rotateVec), Time.deltaTime * rotateSpeed);
        
        }
    }

    public void SwitchForm()
    {
        if (state == PlayerState.Normal)
        {
            state = PlayerState.Combat;
            anim.SetBool("Combat", true);
        }

        else if (state == PlayerState.Combat)
        {
            state = PlayerState.Normal;
            anim.SetBool("Combat", false);
        }
    }
}
