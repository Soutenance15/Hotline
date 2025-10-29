using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Scripts System
    PlayerMoveSystem playerMove;
    PlayerInputSystem playerInput;
    PlayerAttackSystem playerAttack;

    // Composants
    Rigidbody2D rb;

    void Awake()
    {
        playerMove = GetComponent<PlayerMoveSystem>();
        playerInput = GetComponent<PlayerInputSystem>();
        playerAttack = GetComponent<PlayerAttackSystem>();
        rb = GetComponent<Rigidbody2D>();

        // Init
        if (null != playerMove && null != rb)
        {
            playerMove.Init(rb);
        }
    }

    void FixedUpdate()
    {
        if (null != playerInput && null != playerMove)
        {
            playerMove.Move(playerInput.MoveInput);
            playerMove.Turn(playerInput.TurnInput);
        }
    }

    void Update()
    {
        if (null != playerInput)
        {
            // Attack
            if (playerInput.ShootPressed && null != playerAttack.ammoBullet)
            {
                if (!playerAttack.ammoBullet.canNotShoot)
                {
                    playerAttack.Shoot(rb.linearVelocity.magnitude);
                    playerAttack.ammoBullet.UsedOneBullet();
                }
            }
        }
    }
}
