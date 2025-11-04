using TMPro;
using UnityEngine;

public class PlayerUISystem : MonoBehaviour
{
    GameObject HUD;
    GameObject weaponHUD;
    GameObject nbEnemyHUD;
    public TextMeshProUGUI weaponNameText;
    public TextMeshProUGUI nbBulletText;
    public TextMeshProUGUI nbEnemyKilledText;
    public TextMeshProUGUI nbEnemyTotalText;

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
            nbEnemyHUD = HUD.transform.Find("NbEnemyHUD").gameObject;
            if (null != nbEnemyHUD)
            {
                nbEnemyTotalText = nbEnemyHUD
                    .transform.Find("NbEnemyTotalText")
                    .GetComponent<TextMeshProUGUI>();
                nbEnemyKilledText = nbEnemyHUD
                    .transform.Find("NbEnemyKilledText")
                    .GetComponent<TextMeshProUGUI>();
            }
        }
    }

    public void UpdateNbEnemyTotalText(string nbEnemyTotal)
    {
        if (null != nbEnemyTotalText)
        {
            nbEnemyTotalText.text = nbEnemyTotal;
        }
    }

    public void UpdateNbEnemyKilledText(string nbEnemyKilled)
    {
        if (null != nbEnemyTotalText)
        {
            nbEnemyKilledText.text = nbEnemyKilled;
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
