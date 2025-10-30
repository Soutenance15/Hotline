using System;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class PlayerInputSystem : MonoBehaviour
{
    public Vector2 MoveInput { get; private set; }
    public Vector2 TurnInput { get; private set; }

    public bool ShootPressed { get; private set; }
    public bool PushTablePressed { get; private set; }
    private bool pushTableConsumed = false;

    // Ces fonctions seront appelées automatiquement par le PlayerInput
    public void OnMove(InputValue value)
    {
        MoveInput = value.Get<Vector2>();
    }

    public void OnTurn(InputValue value)
    {
        TurnInput = value.Get<Vector2>();
    }

    public void OnPushTable(InputValue value)
    {
        if (value.isPressed)
            PushTablePressed = true;
        else
            PushTablePressed = false;
    }

    public void OnShoot(InputValue value)
    {
        if (value.isPressed)
            ShootPressed = true;
        else
            ShootPressed = false;
    }

    public void SetPushTable(bool pushTable)
    {
        PushTablePressed = pushTable;
    }

    void LateUpdate()
    {
        ShootPressed = false;
        PushTablePressed = false;
    }
}
