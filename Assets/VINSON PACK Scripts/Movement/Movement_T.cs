using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_T : Movement
{
    [SerializeField] bool rawMovement;
    [SerializeField] float moveSpeed = 5;
    [SerializeField] float sprintSpeed = 10;

    // Update is called once per frame
    void Update()
    {
        if (rawMovement)
            MovementRaw();
        else
            Movement();
    }

    void Movement()
    {
        Vector3 input = GetMovementInput();

        if (Input.GetKey(KeyCode.LeftShift))
            transform.position += input * sprintSpeed * Time.deltaTime;
        else
            transform.position += input * moveSpeed * Time.deltaTime;
    }

    void MovementRaw()
    {
        Vector3 input = GetMovementInputRaw();

        if (Input.GetKey(KeyCode.LeftShift))
            transform.position += input * sprintSpeed * Time.deltaTime;
        else
            transform.position += input * moveSpeed * Time.deltaTime;
    }
}
