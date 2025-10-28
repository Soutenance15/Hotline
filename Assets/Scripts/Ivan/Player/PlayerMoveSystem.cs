using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMoveSystem : MonoBehaviour
{
    // --- Mouvements ---
    private float currentSpeed = 3f;

    private Camera mainCam;

    // --- Composant ---
    private Rigidbody2D rb;

    public void Init(Rigidbody2D rb)
    {
        this.rb = rb;
    }

    public void Move(Vector2 moveInput)
    {
        Vector2 direction = new Vector2(moveInput.x, moveInput.y).normalized;

        // --- Appliquer la vitesse ---
        rb.linearVelocity = direction * currentSpeed;
    }

    void Awake()
    {
        mainCam = Camera.main;
    }

    public void Turn(Vector2 cursorInput)
    {
        if (null == mainCam)
        {
            mainCam = Camera.main;
        }
        // Recupere la direction
        Vector2 playerCursorDir = GetCursorDirection(cursorInput);

        // Calculer l’angle en degrés
        float anglePlayerCursor = GetAnglePlayerCursor(playerCursorDir);

        // Appliquer la rotation directement au Rigidbody2D
        rb.rotation = anglePlayerCursor;
    }

    public Vector2 GetCursorDirection(Vector2 cursorInput)
    {
        // Convertir la position du curseur de l'écran vers le monde
        Vector2 mouseWorldPos = mainCam.ScreenToWorldPoint(cursorInput);
        // Direction Joueur vers le curseur
        return mouseWorldPos - rb.position;
    }

    public float GetAnglePlayerCursor(Vector2 playerCursorDir)
    {
        // Calculer l’angle en degrés
        float angle = Mathf.Atan2(playerCursorDir.y, playerCursorDir.x) * Mathf.Rad2Deg;
        // Par défaut le sprite pointe vers le haut, on retire 90° pour compenser
        return angle - 90f;
    }

    public void ResetVelocity()
    {
        rb.linearVelocity = Vector2.zero;
    }
}
