using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player2 : MonoBehaviour
{
    public Camera playerCamera;
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float gravity = 10f;
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;

    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private CharacterController characterController;

    private bool canMove = true;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // คำนวณทิศทางการเคลื่อนที่
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        // ตรวจสอบการกดปุ่มวิ่ง
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);
        moveDirection.y = movementDirectionY;

        
        // ระบบแรงโน้มถ่วง
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }
        else if (moveDirection.y < 0)
        {
            moveDirection.y = -2f; // รีเซ็ตแรงโน้มถ่วงเมื่อติดพื้นเพื่อไม่ให้ค่าสะสม
        }

        // สั่งให้ตัวละครเคลื่อนที่
        characterController.Move(moveDirection * Time.deltaTime);

        // ระบบหมุนมุมกล้อง
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }
}