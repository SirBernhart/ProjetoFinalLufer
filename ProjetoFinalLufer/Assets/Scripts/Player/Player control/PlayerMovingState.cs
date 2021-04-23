using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovingState : State
{
    [Header("External references")]
    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform playerRoot;

    [Header("Gameplay tweeking fields")]
    [SerializeField] private float speed;
    [SerializeField] private float turnSmoothTime = 0.1f;

    // Internal attributes
    private float horizontalInput, verticalInput;
    private Vector3 direction;
    private float currSpeed;
    private float turnSmoothVelocity;

    public override void Enter()
    {
        HandleInput();
    }

    public override void HandleInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    public override void PhysicsUpdate()
    {
        direction = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(playerRoot.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            playerRoot.rotation = Quaternion.Euler(0f, angle, 0f);
            currSpeed = speed;

            controller.Move(direction * currSpeed * Time.deltaTime);
        }
    }

    public override void LogicUpdate() { }

    public override void Exit() { }
}
