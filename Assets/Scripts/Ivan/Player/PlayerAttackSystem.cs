using UnityEngine;

public class PlayerAttackSystem : MonoBehaviour
{
    public GameObject bulletPrefab;

    public void Shoot(float speedPlayer)
    {
        if (null != bulletPrefab)
        {
            Debug.Log("Shoot");
            // Instancie la bombe devant le kart, dans sa rotation actuelle
            GameObject bulletObject = Instantiate(
                bulletPrefab,
                transform.position,
                transform.rotation
            );

            Bullet bullet = bulletObject.GetComponent<Bullet>();
            if (null != bullet)
            {
                bullet.speed += speedPlayer;
                // currentAmmo -= 1;
            }
        }
    }
}
