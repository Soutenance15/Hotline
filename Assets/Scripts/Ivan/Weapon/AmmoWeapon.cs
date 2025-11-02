using System;
using UnityEngine;

public class AmmoWeapon : MonoBehaviour
{
    public static Action<AmmoWeapon> OnAmmoWeaponEnter;
    public int nbAmmo = 0;
    public int nbMaxAmmo = 9;
    public bool canNotShoot = true;
    public AudioClip reloadClip;

    public enum WeaponName
    {
        Beretta,
        Shotgun,
        Uzi,
        Famas,
    }

    public WeaponName weaponName;

    public void UsedOneWeapon()
    {
        nbAmmo -= 1;
        canNotShoot = CanNotShoot();
    }

    public bool CanNotShoot()
    {
        if (nbAmmo < 1)
        {
            return true;
        }
        return false;
    }
}
