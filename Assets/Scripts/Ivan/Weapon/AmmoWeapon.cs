using System;
using UnityEngine;

public class AmmoWeapon : MonoBehaviour
{
    public static Action<AmmoWeapon> OnAmmoWeaponEnter;
    public int nbAmmo = 9;
    public bool canNotShoot;

    public enum WeaponName
    {
        Beretta,
        Shotgun,
        Uzi,
        Famas,
    }

    void Awake()
    {
        if (nbAmmo <= 0)
        {
            // Au minimum 1 balles dans le chargeur a ramassé
            nbAmmo = 1;
        }
    }

    public WeaponName weaponName = WeaponName.Beretta;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Trigger enter");
            OnAmmoWeaponEnter?.Invoke(this);
        }
    }

    public void UsedOneWeapon()
    {
        nbAmmo -= 1;
        canNotShoot = CanNotShoot();
    }

    bool CanNotShoot()
    {
        if (nbAmmo < 1)
        {
            return true;
        }
        return false;
    }
}
