using UnityEngine;

public class PlayerAttackSystem : MonoBehaviour
{
    public GameObject bulletPrefab;
    public AmmoWeapon ammoWeapon;

    // Functions
    public void SetAmmoWeapon(AmmoWeapon ammoWeapon)
    {
        this.ammoWeapon = ammoWeapon;
    }

    public void Shoot(float speedPlayer)
    {
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
