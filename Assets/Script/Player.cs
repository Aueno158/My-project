using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
public Camera playerCamera;
public float walkSpeed = 45f;
public float runSpeed = 70f;
public float jumpPower = 7f;
public float gravity = 10f;
public float lookSpeed = 2f;
public float lookXLimit = 45f;
public float defaultHeight = 2f;
public float crouchHeight = 1f;
public float crouchSpeed = 3f;

public bool HasKey = false; 
public bool HasPostIt = false;

private Vector3 moveDirection = Vector3.zero;
private CharacterController characterController;

private bool canMove = true;
private bool canRun = true;

void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

bool isRunning = canRun && Input.GetKey(KeyCode.LeftShift);
float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Mathf.Max(Input.GetAxis("Vertical"), 0f) : 0;
float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
else
        {
            moveDirection.y = movementDirectionY;
        }

if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

if (Input.GetKey(KeyCode.R) && canMove)
        {
            characterController.height = crouchHeight;
            walkSpeed = crouchSpeed;
            runSpeed = crouchSpeed;

        }
else
        {
            characterController.height = defaultHeight;
            walkSpeed = 30f;
            runSpeed = 70f;
        }

        characterController.Move(moveDirection * Time.deltaTime);
    }

public void SetCanMove(bool value)
{
    canMove = value;
}

public void SetCanRun(bool value)
{
    canRun = value;
}
}