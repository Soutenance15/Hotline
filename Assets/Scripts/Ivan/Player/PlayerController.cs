using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Scripts System
    PlayerMoveSystem playerMove;
    PlayerInputSystem playerInput;
    PlayerAttackSystem playerAttack;
    PlayerUISystem playerUI;

    // Composants
    Rigidbody2D rb;

    // Gestion Evenement exterieur

    void OnEnable()
    {
        AmmoBullet.OnAmmoBulletEnter += TakeAmmoBullet;
    }

    void OnDisable()
    {
        AmmoBullet.OnAmmoBulletEnter -= TakeAmmoBullet;
    }

    // Functions
    private void TakeAmmoBullet(AmmoBullet ammoBullet)
    {
        if (null != playerAttack)
        {
            playerAttack.SetAmmoBullet(ammoBullet);
        }
        string weaponName = playerAttack.ammoBullet.weaponName.ToString();
        if (null != weaponName)
        {
            playerUI.UpdateWeaponNameText(weaponName);
        }
        string nbBullet = playerAttack.ammoBullet.nbBullet.ToString();
        if (null != nbBullet)
        {
            playerUI.UpdateNbBulletNameText(nbBullet);
        }
    }

    void Awake()
    {
        playerMove = GetComponent<PlayerMoveSystem>();
        playerInput = GetComponent<PlayerInputSystem>();
        playerAttack = GetComponent<PlayerAttackSystem>();
        playerUI = GetComponent<PlayerUISystem>();
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
                    string nbBullet = playerAttack.ammoBullet.nbBullet.ToString();
                    if (null != nbBullet)
                    {
                        playerUI.UpdateNbBulletNameText(nbBullet);
                    }
                }
            }
        }
    }
}
