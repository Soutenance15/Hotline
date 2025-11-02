using UnityEngine;

public class PlayerAttackSystem : MonoBehaviour
{
    public GameObject bulletPrefab;
    public AmmoWeapon ammoWeapon;

    // Effet
    public AudioClip shootClip;

    // Functions
    public void SetAmmoWeapon(AmmoWeapon ammoWeapon)
    {
        this.ammoWeapon = ammoWeapon;
    }

    public void ConfigAmmoWeapon(AmmoToTake ammoToTake)
    {
        ammoWeapon.nbAmmo = ammoToTake.nbAmmo;
        ammoWeapon.weaponName = ammoToTake.weaponName;
        ammoWeapon.canNotShoot = false;
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

            GameSoundEffect.PlaySound(shootClip, 0.005f);
        }
    }
}
