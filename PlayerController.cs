using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5;
    private float horizontalInput;
    private float forwardInput;

    public Transform playerBody;
    private CharacterController controller;

    [Header("Jumping/Gravity")]
    public float jumpForce = 3f;
    public Vector3 velocity;
    public float gravity = -9.81f;
    public Transform GroundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    private bool isGrounded;

    // Start is called before the first frame update
    void Awake()
    {
        controller = GetComponent<CharacterController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        Vector3 move = transform.right * horizontalInput + transform.forward * forwardInput;

        if(Input.GetButton("Sprint"))
        {
            speed = speed * 2f;
        }
        else
        {
            speed = 5f;
        }

        controller.Move(move * speed * Time.deltaTime);
        
    }

    void FixedUpdate()
    {
        velocity.y += gravity * Time.deltaTime;
        isGrounded = Physics.CheckSphere(GroundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
           velocity.y = -2f; 
        }

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);

        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

    }
}
