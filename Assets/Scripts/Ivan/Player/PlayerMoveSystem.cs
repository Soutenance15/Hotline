using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMoveSystem : MonoBehaviour
{
    // --- Mouvements ---
    private float currentSpeed = 3f;

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

    public void ResetVelocity()
    {
        rb.linearVelocity = Vector2.zero;
    }
}
