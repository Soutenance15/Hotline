using UnityEngine;

public class TurnTable : MonoBehaviour
{
    public bool onSide = false;
    public Collider2D collider2DStand;
    public Collider2D collider2DOnSide;

    void Awake()
    {
        collider2DOnSide.enabled = false;
    }

    public void Turn()
    {
        onSide = !onSide;
        if (onSide)
        {
            collider2DOnSide.enabled = true;
            collider2DStand.enabled = false;
        }
        else
        {
            collider2DStand.enabled = true;
            collider2DOnSide.enabled = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Turn();
        }
    }
}
