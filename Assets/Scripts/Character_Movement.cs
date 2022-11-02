using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Character_Movement : MonoBehaviour
{
    [Range(1,10)]public float mouseSensitivity = 5;
    public float rotationSnapMultiplier = 5f;
    Animator animator;
    private Rigidbody RB;    
    public float Speed = 8.0f;
    public Transform cameraPivot;
    private Vector3 CameraPos;
    public bool rotateCameraWithMouse = false;
    Collider col;

    private Vector3 mousePos;
    Transform activeCamera;

    [SerializeField]Vector3 globalDirection;
    Quaternion targetRotation;

    public float GroundCheck_Radius = 0.1f;
    public Vector3 GroundCheck_RaycastStartOffset;
    public LayerMask GroundCheck_LayerMask;

    public float JumpVelocity = 19.62f;
    bool isGrounded = false, isJumping;

    private void Awake()
    {
        RB = GetComponent<Rigidbody>();
        CameraPos = cameraPivot.transform.position - transform.position;
        animator = transform.GetChild(0).GetComponent<Animator>();
        mousePos = Input.mousePosition;
        col = GetComponent<Collider>();
    }

    private void Start()
    {
        activeCamera = Camera.main.transform;
    }

    void FixedUpdate()
    {
        Vector3 output = globalDirection * Speed;
        RB.velocity = new Vector3(output.x, RB.velocity.y, output.z);//=RB.position * Time.deltaTime;

        if (globalDirection != Vector3.zero)
        {
            RB.MoveRotation(targetRotation);
        }

        col.enabled = false;
        isGrounded = Physics.CheckSphere(this.transform.position + GroundCheck_RaycastStartOffset, GroundCheck_Radius, GroundCheck_LayerMask);
        col.enabled = true;
        if (isGrounded == true) isJumping = false;
    }

    private void OnDrawGizmosSelected()
    {
        /*Gizmos.DrawLine(this.transform.position + GroundCheck_RaycastStartOffset, 
            this.transform.position + GroundCheck_RaycastStartOffset + Vector3.down * GroundCheck_Radius);*/
        Gizmos.DrawWireSphere(this.transform.position + GroundCheck_RaycastStartOffset, GroundCheck_Radius);
    }

    private void Update()
    {
        

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput).normalized;

        globalDirection = activeCamera.TransformDirection(movement);
        globalDirection.y = 0;
        globalDirection.Normalize();

        //global to local space
        //clipspace

        if (globalDirection != Vector3.zero)
        {
            targetRotation = Quaternion.LookRotation(globalDirection);
            // Debug.Log(targetRotation.eulerAngles);
            targetRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 360 * Time.fixedDeltaTime * rotationSnapMultiplier);
        }

        animator.SetBool("Run", globalDirection == Vector3.zero ? false : true);

        if (Input.GetKeyDown(KeyCode.Space) && isJumping == false && isGrounded)
        {
            isJumping = true;
            RB.velocity = new Vector3(RB.velocity.x, JumpVelocity, RB.velocity.z);
        }
        
        animator.SetBool("Jump", isJumping);
        animator.SetBool("isGrounded", isGrounded);
    }

    private void LateUpdate()
    {
        cameraPivot.transform.position = RB.position + CameraPos;
        if (rotateCameraWithMouse)
        {
            Vector3 delta = mousePos - Input.mousePosition;//x - mouse x, y-axis : mouse y
            cameraPivot.transform.Rotate(new Vector3(0, -1 * delta.x, 0) * Time.deltaTime * mouseSensitivity);
            mousePos = Input.mousePosition;
        }
    }
}
