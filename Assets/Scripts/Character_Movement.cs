using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Movement : MonoBehaviour
{
    Animator animator;
   
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        /*bool isRunning = animator.GetBool("isRunning");
        
              
        bool forwardPressed = Input.GetKey("w");

       

        if (!isRunning && forwardPressed)
        {
            animator.SetBool("isRunning", true);
        }
        if (isRunning && !forwardPressed)
        {
            animator.SetBool("isRunning", false);
        }*/

        bool isJumping = Input.GetKey(KeyCode.Space);
        animator.SetBool("Jump", isJumping);

        bool isRunning = Input.GetKey(KeyCode.W);
        animator.SetBool("Run", isRunning);

        bool isTurningLeft = Input.GetKey(KeyCode.A);
        animator.SetBool("Left", isTurningLeft);

        bool isTurningRight = Input.GetKey(KeyCode.D);
        animator.SetBool("Right", isTurningRight);

        bool isTurningBack = Input.GetKey(KeyCode.S);
        animator.SetBool("Back", isTurningBack);



    }
}
