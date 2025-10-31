using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    // Scripts System
    PlayerMoveSystem playerMove;
    public PlayerInputSystem playerInput;
    PlayerAttackSystem playerAttack;
    PlayerUISystem playerUI;
    public Health health;

    // Scripts Globaux
    GameVisualEffect visualEffect;

    // Composants
    Rigidbody2D rb;

    [Header("Interaction")]
    public LayerMask turnTableLayer;
    private float interactRange = 0.6f;

    public Vector3 spawnPosition;

    // Gestion Evenement exterieur

    void OnEnable()
    {
        AmmoWeapon.OnAmmoWeaponEnter += TakeAmmoWeapon;
        health.OnDie += Die;
    }

    void OnDisable()
    {
        AmmoWeapon.OnAmmoWeaponEnter -= TakeAmmoWeapon;
    }

    // Functions

    void Die()
    {
        Debug.Log("Animation Die");
        StartCoroutine(WaitTwoSeconds());
        GameManager.instance.GameOver();
    }

    IEnumerator WaitTwoSeconds()
    {
        Debug.Log("Début de la coroutine...");
        yield return new WaitForSeconds(2f); // attend 2 secondes
        Debug.Log("2 secondes écoulées !");
    }

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
        visualEffect = GetComponent<GameVisualEffect>();
        health = GetComponent<Health>();

        rb = GetComponent<Rigidbody2D>();

        // Init
        if (null != playerMove && null != rb)
        {
            playerMove.Init(rb);
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

                    // Visual Effect
                    if (null != visualEffect)
                    {
                        visualEffect.ShootEffect(transform);
                    }
                }
            }

            if (playerInput.PushTablePressed)
            {
                Debug.Log("PushTable Pressed");
                TurnTable turnTable = HitTurnTable();
                if (null != turnTable)
                {
                    turnTable.Turn();
                }
            }
        }
    }
}
