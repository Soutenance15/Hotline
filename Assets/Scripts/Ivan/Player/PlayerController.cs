using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class PlayerController : MonoBehaviour
{
    PlayerMoveSystem playerMove;
    PlayerInputSystem playerInput;
    PlayerAttackSystem playerAttack;
    PlayerUISystem playerUI;
    GameVisualEffect visualEffect;

    Rigidbody2D rb;
    Animator animator;
    private Vector3 lastPosition;

    [Header("Audio Clips")]
    public AudioClip ammoPickupSound;
    public AudioClip shootSound;

    private AudioSource audioSource;

    void OnEnable()
    {
        AmmoWeapon.OnAmmoWeaponEnter += TakeAmmoWeapon;
    }

    void OnDisable()
    {
        AmmoWeapon.OnAmmoWeaponEnter -= TakeAmmoWeapon;
    }

    private void TakeAmmoWeapon(AmmoWeapon ammoWeapon)
    {
        if (playerAttack != null)
        {
            playerAttack.SetAmmoWeapon(ammoWeapon);
        }

        if (playerAttack?.ammoWeapon != null)
        {
            string weaponName = playerAttack.ammoWeapon.weaponName.ToString();
            playerUI?.UpdateWeaponNameText(weaponName);

            string nbAmmo = playerAttack.ammoWeapon.nbAmmo.ToString();
            playerUI?.UpdateNbAmmoNameText(nbAmmo);

            if (ammoPickupSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(ammoPickupSound);
            }
        }
    }

    void Awake()
    {
        playerMove = GetComponent<PlayerMoveSystem>();
        playerInput = GetComponent<PlayerInputSystem>();
        playerAttack = GetComponent<PlayerAttackSystem>();
        playerUI = GetComponent<PlayerUISystem>();
        visualEffect = GetComponent<GameVisualEffect>();

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        lastPosition = transform.position;

        audioSource = GetComponent<AudioSource>();

        // Init
        if (playerMove != null && rb != null)
        {
            playerMove.Init(rb);
        }
    }

    void FixedUpdate()
    {
        if (playerInput != null && playerMove != null)
        {
            playerMove.Move(playerInput.MoveInput);
            playerMove.Turn(playerInput.TurnInput);
        }

        float moveDistance = (transform.position - lastPosition).magnitude;
        if (animator != null)
        {
            animator.SetBool("IsWalking", moveDistance > 0.01f);
        }
        lastPosition = transform.position;
    }

    void Update()
    {
        if (playerInput != null && playerInput.ShootPressed && playerAttack?.ammoWeapon != null)
        {
            if (!playerAttack.ammoWeapon.canNotShoot)
            {
                // Attack
                playerAttack.Shoot(rb.linearVelocity.magnitude);
                playerAttack.ammoWeapon.UsedOneWeapon();

                string nbAmmo = playerAttack.ammoWeapon.nbAmmo.ToString();
                playerUI?.UpdateNbAmmoNameText(nbAmmo);

                // Visual Effect
                visualEffect?.ShootEffect(transform);

                if (shootSound != null && audioSource != null)
                {
                    audioSource.PlayOneShot(shootSound);
                }
            }
        }
    }
}
