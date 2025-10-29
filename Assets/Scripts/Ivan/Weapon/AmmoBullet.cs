using System;
using UnityEngine;

public class AmmoBullet : MonoBehaviour
{
    public static Action<AmmoBullet> OnAmmoBulletEnter;
    public int nbBullet;
    public bool canNotShoot;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Trigger enter");
            OnAmmoBulletEnter?.Invoke(this);
        }
    }

    public void UsedOneBullet()
    {
        nbBullet -= 1;
        canNotShoot = CanNotShoot();
    }

    bool CanNotShoot()
    {
        if (nbBullet < 1)
        {
            return true;
        }
        return false;
    }
}
