using System;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class PlayerInputSystem : MonoBehaviour
{
    public Vector2 MoveInput { get; private set; }
    public float TurnInput { get; private set; }

    public bool ShootPressed { get; private set; }

    public static event Action OnStartGame;

    // Ces fonctions seront appelées automatiquement par le PlayerInput
    public void OnMove(InputValue value)
    {
        MoveInput = value.Get<Vector2>();
    }

    public void OnTurn(InputValue value)
    {
        TurnInput = value.Get<float>();
    }

    public void OnShoot(InputValue value)
    {
        if (value.isPressed)
            ShootPressed = true;
        else
            ShootPressed = false;
    }

    void LateUpdate()
    {
        ShootPressed = false;
    }
}
