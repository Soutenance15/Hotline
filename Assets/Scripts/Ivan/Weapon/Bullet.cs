using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 12f;
    private float timer; // durée avant disparition
    public Health health;

    void Start() { }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            health = collision.gameObject.GetComponent<Health>();
            health.UpdateDamage(-50);
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "TileMapCollider")
        {
            Destroy(gameObject);
        }
        else if (collision.CompareTag("TurnTable"))
        {
            TurnTable turnTable = collision.GetComponent<TurnTable>();
            if (turnTable.onSide)
            {
                Destroy(gameObject);
            }
        }
    }

    void Update()
    {
        // avance dans la direction "up" locale si state move
        transform.position += transform.up * speed * Time.deltaTime;
        timer += Time.deltaTime;
        if (timer > 4)
        {
            Destroy(gameObject);
        }
    }
}
