using System;
using UnityEngine;

public class TurnTable : MonoBehaviour
{
    public bool onSide = false;
    public Collider2D collider2DStand;
    public Collider2D collider2DOnSide;

    public SpriteRenderer sr;

    void Awake()
    {
        collider2DOnSide.enabled = false;
        sr = GetComponent<SpriteRenderer>();
    }

    public void Turn()
    {
        onSide = !onSide;
        if (onSide)
        {
            collider2DOnSide.enabled = true;
            collider2DStand.enabled = false;
            sr.color = Color.red;
        }
        else
        {
            sr.color = Color.green;
            collider2DStand.enabled = true;
            collider2DOnSide.enabled = false;
        }
    }
}
