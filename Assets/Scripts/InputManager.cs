using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.OnFootActions onfoot;
    private PlayerMotor motor;
    private PlayerLook look;


    // Start is called before the first frame update
    void Awake()
    {
        playerInput = new PlayerInput();
        onfoot = playerInput.OnFoot;
        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();
        onfoot.Jump.performed += ctx => motor.jump();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        motor.ProcessMove(onfoot.Movement.ReadValue<Vector2>());
        
    }

    private void LateUpdate()
    {
        look.ProcessLook(onfoot.Look.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        onfoot.Enable();

    }
    private void OnDisable()
    {
        onfoot.Disable();

    }
}
