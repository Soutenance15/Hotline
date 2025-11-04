using System.Collections;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    // Scripts System
    PlayerMoveSystem playerMove;
    public PlayerInputSystem playerInput;
    PlayerAttackSystem playerAttack;
    public PlayerUISystem playerUI;
    public Health health;

    // Composants
    Rigidbody2D rb;
    Animator animator;
    private Vector3 lastPosition;

    [Header("Interaction")]
    public LayerMask turnTableLayer;
    private float interactRange = 0.6f;

    public Vector3 spawnPosition;

    public AmmoWeapon ammoWeapon;

    // Effet
    public AudioClip dieClip;

    // Gestion Evenement exterieur

    void OnEnable()
    {
        AmmoToTake.OnAmmoToTakeEnter += TakeAmmoWeapon;
        health.OnDie += Die;
    }

    void OnDisable()
    {
        AmmoToTake.OnAmmoToTakeEnter -= TakeAmmoWeapon;
    }

    // Functions
    public AmmoWeapon GetAmmo()
    {
        return playerAttack.ammoWeapon;
    }

    void Die()
    {
        GameSoundEffect.PlaySound(dieClip);
        StartCoroutine(WaitTwoSeconds());
        if (null != GameManager.instance)
        {
            GameManager.instance.GameOver();
        }
        else
        {
            Debug.LogWarning("Attention GameManager instance est NULL.");
        }
    }

    IEnumerator WaitTwoSeconds()
    {
        yield return new WaitForSeconds(2f); // attend 2 secondes
    }

    private void TakeAmmoWeapon(AmmoToTake ammoToTake)
    {
        if (null != playerAttack)
        {
            playerAttack.ConfigAmmoWeapon(ammoToTake);
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
        if (null != playerUI)
        {
            playerUI.ShowWeaponHUD(true);
        }
    }

    public void ShowWeaponHUD(bool show)
    {
        if (null != playerUI)
        {
            playerUI.ShowWeaponHUD(show);
        }
    }

    void Awake()
    {
        playerMove = GetComponent<PlayerMoveSystem>();
        playerInput = GetComponent<PlayerInputSystem>();
        playerAttack = GetComponent<PlayerAttackSystem>();
        ammoWeapon = GetComponent<AmmoWeapon>();
        if (null == ammoWeapon)
        {
            ammoWeapon = GameObject.Find("AmmoWeapon").GetComponent<AmmoWeapon>();
        }
        ammoWeapon.canNotShoot = true;
        playerAttack.SetAmmoWeapon(ammoWeapon);

        playerUI = GetComponent<PlayerUISystem>();
        health = GetComponent<Health>();

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        lastPosition = transform.position;

        // Init
        if (null != playerMove && null != rb)
        {
            playerMove.Init(rb);
        }
        if (null != playerUI)
        {
            playerUI.ShowWeaponHUD(false);
        }
    }

    void Start()
    {
        spawnPosition = transform.position;
    }

    void FixedUpdate()
    {
        if (null != playerInput && null != playerMove)
        {
            playerMove.Move(playerInput.MoveInput);
            playerMove.Turn(playerInput.TurnInput);
        }

        // Simon en dessous
        float moveDistance = (transform.position - lastPosition).magnitude;
        if (animator != null)
        {
            animator.SetBool("IsWalking", moveDistance > 0.01f);
        }
        lastPosition = transform.position;
    }

    TurnTable HitTurnTable()
    {
        // Centre de détection = position du joueur
        Vector2 origin = transform.position;

        // On cherche un collider de type TurnTable dans la zone
        Collider2D hit = Physics2D.OverlapCircle(origin, interactRange, turnTableLayer);

        if (hit != null)
        {
            TurnTable table = hit.GetComponent<TurnTable>();
            if (table != null)
            {
                return table;
            }
        }
        return null;
    }

    void Update()
    {
        if (null != playerInput)
        {
            // if (playerInput.ShootPressed && null != playerAttack.ammoWeapon)
            if (playerInput.ShootPressed && null != playerAttack.ammoWeapon)
            {
                if (!playerAttack.ammoWeapon.canNotShoot)
                {
                    // Attack
                    playerAttack.Shoot(rb.linearVelocity.magnitude);
                    playerAttack.ammoWeapon.UsedOneWeapon();
                    string nbAmmo = playerAttack.ammoWeapon.nbAmmo.ToString();

                    // UI
                    if (null != nbAmmo)
                    {
                        playerUI.UpdateNbAmmoNameText(nbAmmo);
                    }
                }
            }

            if (playerInput.PushTablePressed)
            {
                TurnTable turnTable = HitTurnTable();
                if (null != turnTable)
                {
                    turnTable.Turn();
                }
            }

            if (playerInput.PausePressed)
            {
                GameManager.instance.TooglePause();
            }
        }
    }
}
