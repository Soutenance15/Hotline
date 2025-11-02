using System;
using UnityEngine;

public class AmmoToTake : MonoBehaviour
{
    public static Action<AmmoToTake> OnAmmoToTakeEnter;
    public int nbAmmo = 9;
    public int nbMaxAmmo = 9;
    public AudioClip reloadClip;

    void Awake()
    {
        if (nbAmmo <= 0)
        {
            // Au minimum 1 balles dans le chargeur a ramassé
            nbAmmo = 1;
        }
    }

    public AmmoWeapon.WeaponName weaponName = AmmoWeapon.WeaponName.Beretta;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerController =
                collision.gameObject.GetComponent<PlayerController>();
            AmmoWeapon ammo = playerController.GetAmmo();
            GameSoundEffect.PlaySound(reloadClip, 1f);
            OnAmmoToTakeEnter?.Invoke(this);
        }
    }
}
