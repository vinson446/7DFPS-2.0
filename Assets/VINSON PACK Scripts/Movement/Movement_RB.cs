using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement_RB : Movement
{
    [Header("Movement Settings")]
    [SerializeField] bool rawMovement;
    [SerializeField] float moveSpeed = 5;
    [SerializeField] float sprintSpeed = 10;
    Vector3 input;

    [Header("Jump Settings")]
    [SerializeField] float jumpPower = 7;
    [SerializeField] float fallMultiplier = 2.5f;
    [SerializeField] float lowJumpMultiplier = 2f;

    [Header("RotationSpeed")]
    [SerializeField] float rotationSpeed = 20;

    [Header("Ground Checks")]
    [SerializeField] Transform groundChecker;
    [SerializeField] float groundCheck = 0.2f;
    [SerializeField] LayerMask ground;
    [SerializeField] bool isGrounded;

    // need to freeze x and z rotation on Rigidbody
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rawMovement)
            GetMovementRaw();
        else
            GetMovement();

        RotateWithMovement();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
    }

    void GetMovement()
    {
        isGrounded = Physics.CheckSphere(groundChecker.position, groundCheck, ground, 
            QueryTriggerInteraction.Ignore);

        input = GetMovementInput();
    }

    void GetMovementRaw()
    {
        isGrounded = Physics.CheckSphere(groundChecker.position, groundCheck, ground, 
            QueryTriggerInteraction.Ignore);

        input = GetMovementInputRaw();
    }

    void ApplyMovement()
    {
        if (Input.GetKey(KeyCode.LeftShift))
            rb.MovePosition(rb.position + input * sprintSpeed * Time.fixedDeltaTime);
        else
            rb.MovePosition(rb.position + input * moveSpeed * Time.fixedDeltaTime);

        ApplyJump();
    }

    void ApplyJump()
    {
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            rb.velocity = Vector3.up * jumpPower;
        }

        // gravity
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        // short jump
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    // third person
    void RotateWithMovement()
    {
        if (input != Vector3.zero)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation,
                Quaternion.LookRotation(new Vector3(input.x, 0, input.z)), Time.deltaTime * rotationSpeed);
        }
    }
}
