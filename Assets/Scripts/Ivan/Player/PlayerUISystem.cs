using TMPro;
using UnityEngine;

public class PlayerUISystem : MonoBehaviour
{
    GameObject HUD;
    TextMeshProUGUI weaponNameText;
    TextMeshProUGUI nbBulletText;

    void Awake()
    {
        HUD = GameObject.Find("HUD");

        if (null != HUD)
        {
            weaponNameText = HUD.transform.Find("WeaponNameText").GetComponent<TextMeshProUGUI>();
            nbBulletText = HUD.transform.Find("NbBulletText").GetComponent<TextMeshProUGUI>();
        }
    }

    public void UpdateWeaponNameText(string weaponName)
    {
        if (null != weaponNameText)
        {
            weaponNameText.text = weaponName;
        }
    }

    public void UpdateNbBulletNameText(string nbBullet)
    {
        if (null != nbBulletText)
        {
            nbBulletText.text = nbBullet;
        }
    }
}
