using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public float PlayerSpeed = 1.0f;
    private float JumpAmount = 2.0f;
    private float JumpMaximum = 10.0f;
    private float HorizontalInput;
    private float ForwardInput;
    private float JumpTime;
    private bool Jumping;

    [SerializeField]private Vector3 playerVelocityVector;

    private Rigidbody PlayerRB;
    // Start is called before the first frame update
    void Start()
    {
        PlayerRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        HorizontalInput = Input.GetAxis("Horizontal");
        ForwardInput = Input.GetAxis("Vertical");

        playerVelocityVector = Vector3.forward * PlayerSpeed * ForwardInput +
            Vector3.right * PlayerSpeed * HorizontalInput;

/*        transform.Translate(Vector3.forward * Time.deltaTime * PlayerSpeed * ForwardInput);
        transform.Translate(Vector3.right * Time.deltaTime * PlayerSpeed * HorizontalInput);*/


        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jumping = true; JumpTime = 0;
        }

        if (Jumping)
        {
            playerVelocityVector.y = JumpAmount;
            JumpTime += Time.deltaTime;
        }if(Input.GetKeyUp(KeyCode.Space) | JumpTime > JumpMaximum)
        {
            Jumping = false;
        }

        transform.Translate(playerVelocityVector * Time.deltaTime); 


        
    }

    private void FixedUpdate()
    {
        //PlayerRB.velocity = playerVelocityVector;
    }
}
