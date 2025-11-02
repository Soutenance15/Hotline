using TMPro;
using UnityEngine;

public class PlayerUISystem : MonoBehaviour
{
    GameObject HUD;
    GameObject weaponHUD;
    TextMeshProUGUI weaponNameText;
    TextMeshProUGUI nbBulletText;

    void Awake()
    {
        HUD = GameObject.Find("HUD");

        if (null != HUD)
        {
            weaponHUD = HUD.transform.Find("WeaponHUD").gameObject;
            if (null != weaponHUD)
            {
                weaponNameText = weaponHUD
                    .transform.Find("WeaponNameText")
                    .GetComponent<TextMeshProUGUI>();
                nbBulletText = weaponHUD
                    .transform.Find("NbBulletText")
                    .GetComponent<TextMeshProUGUI>();
            }
        }
    }

    public void UpdateWeaponNameText(string weaponName)
    {
        if (null != weaponNameText)
        {
            weaponNameText.text = weaponName;
        }
    }

    public void UpdateNbAmmoNameText(string nbBullet)
    {
        if (null != nbBulletText)
        {
            nbBulletText.text = nbBullet;
            int nbBulletInt = int.Parse(nbBullet);
            if (nbBulletInt == 0)
            {
                nbBulletText.color = Color.red;
            }
            else
            {
                nbBulletText.color = Color.white;
            }
        }
    }

    public void ShowWeaponHUD(bool show)
    {
        if (null != weaponHUD)
        {
            weaponHUD.SetActive(show);
        }
    }
}
