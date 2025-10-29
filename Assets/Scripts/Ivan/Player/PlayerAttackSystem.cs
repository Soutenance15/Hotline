using UnityEngine;

public class PlayerAttackSystem : MonoBehaviour
{
    public GameObject bulletPrefab;
    public AmmoBullet ammoBullet;

    // Gestion evenement exterieur

    void OnEnable()
    {
        AmmoBullet.OnAmmoBulletEnter += TakeAmmoBullet;
    }

    void OnDisable()
    {
        AmmoBullet.OnAmmoBulletEnter -= TakeAmmoBullet;
    }

    void TakeAmmoBullet(AmmoBullet ammoBullet)
    {
        this.ammoBullet = ammoBullet;
    }

    public void Shoot(float speedPlayer)
    {
        Debug.Log(ammoBullet.nbBullet.ToString());
        if (null != bulletPrefab)
        {
            GameObject bulletObject = Instantiate(
                bulletPrefab,
                transform.position,
                transform.rotation
            );

            Bullet bullet = bulletObject.GetComponent<Bullet>();
            if (null != bullet)
            {
                // Ajoute la vitesse du player a la balle (utile si il avance)
                bullet.speed += speedPlayer;
            }
        }
    }
}
