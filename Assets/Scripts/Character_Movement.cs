using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Character_Movement : MonoBehaviour
{
    
    Animator animator;
    private Rigidbody RB;    
    public float Speed = 8.0f;
    public Camera followCamera;
    private Vector3 CameraPos;
  


    private void Awake()
    {
        RB = GetComponent<Rigidbody>();
        CameraPos = followCamera.transform.position - transform.position;
        animator = GetComponent<Animator>();
    }
    
    
    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput).normalized;

        if(movement == Vector3.zero)
        {
            return;
        }

        Quaternion targetRotation = Quaternion.LookRotation(movement);
       // Debug.Log(targetRotation.eulerAngles);
        targetRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 360 * Time.fixedDeltaTime);
        RB.MovePosition(RB.position + movement * Speed * Time.fixedDeltaTime);
        RB.MoveRotation(targetRotation);
        
       // bool isJumping = Input.GetKey(KeyCode.Space);
       // animator.SetBool("Jump", isJumping);

      //  bool isRunning = Input.GetKey(KeyCode.W);
       // animator.SetBool("Run", isRunning);
    }

    private void LateUpdate()
    {
        followCamera.transform.position = RB.position + CameraPos;
    }
}
