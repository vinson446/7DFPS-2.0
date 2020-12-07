using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Movement_CC : Movement
{
    [Header("Movement Settings")]
    [SerializeField] bool rawMovement;
    [SerializeField] float moveSpeed = 5;
    [SerializeField] float sprintSpeed = 10;
    Vector3 input;

    [Header("Jump Settings")]
    [SerializeField] float jumpPower = 7;
    [SerializeField] float gravity = -9.81f;
    [SerializeField] float fallMultiplier = 2.5f;
    [SerializeField] float lowJumpMultiplier = 2f;
    Vector3 drag;

    [Header("RotationSpeed")]
    [SerializeField] bool thirdPerson;
    [SerializeField] float rotationSpeed = 20;

    [Header("Ground Checks")]
    [SerializeField] Transform groundChecker;
    [SerializeField] float groundCheck = 0.2f;
    [SerializeField] LayerMask ground;
    [SerializeField] bool isGrounded;

    CharacterController characterController;
    Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!rawMovement)
            Movement();
        else
            MovementRaw();

        if (thirdPerson)
            RotateWithMovement();
        else
            RotateWithMouse();
    }

    void Movement()
    {
        isGrounded = Physics.CheckSphere(groundChecker.position, groundCheck, ground,
            QueryTriggerInteraction.Ignore);

        input = GetMovementInput();

        if (Input.GetKey(KeyCode.LeftShift))
            characterController.Move(input * sprintSpeed * Time.deltaTime);
        else
            characterController.Move(input * moveSpeed * Time.deltaTime);

        Jump();
    }

    void MovementRaw()
    {
        isGrounded = Physics.CheckSphere(groundChecker.position, groundCheck, ground,
            QueryTriggerInteraction.Ignore);

        input = GetMovementInputRaw();

        if (Input.GetKey(KeyCode.LeftShift))
            characterController.Move(input * sprintSpeed * Time.deltaTime);
        else
            characterController.Move(input * moveSpeed * Time.deltaTime);

        Jump();
    }

    void Jump()
    {
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            velocity = Vector3.up * jumpPower;
        }

        // gravity
        velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;

        // short jump
        if (velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        characterController.Move(velocity * Time.deltaTime);
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

    // first person
    void RotateWithMouse()
    {

    }
}
