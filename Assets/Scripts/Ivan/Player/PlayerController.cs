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
        AmmoWeapon.OnAmmoWeaponEnter += TakeAmmoWeapon;
    }

    void OnDisable()
    {
        AmmoWeapon.OnAmmoWeaponEnter -= TakeAmmoWeapon;
    }

    // Functions
    private void TakeAmmoWeapon(AmmoWeapon ammoWeapon)
    {
        if (null != playerAttack)
        {
            playerAttack.SetAmmoWeapon(ammoWeapon);
        }
        string weaponName = playerAttack.ammoWeapon.weaponName.ToString();
        if (null != weaponName)
        {
            playerUI.UpdateWeaponNameText(weaponName);
        }
        string nbAmmo = playerAttack.ammoWeapon.nbAmmo.ToString();
        if (null != nbAmmo)
        {
            playerUI.UpdateNbAmmoNameText(nbAmmo);
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
            if (playerInput.ShootPressed && null != playerAttack.ammoWeapon)
            {
                if (!playerAttack.ammoWeapon.canNotShoot)
                {
                    playerAttack.Shoot(rb.linearVelocity.magnitude);
                    playerAttack.ammoWeapon.UsedOneWeapon();
                    string nbAmmo = playerAttack.ammoWeapon.nbAmmo.ToString();
                    if (null != nbAmmo)
                    {
                        playerUI.UpdateNbAmmoNameText(nbAmmo);
                    }
                }
            }
        }
    }
}
