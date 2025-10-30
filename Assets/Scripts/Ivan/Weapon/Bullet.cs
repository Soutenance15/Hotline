using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 12f;
    private float lifetime = 3f; // durée avant disparition

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // avance dans la direction "up" locale si state move
        transform.position += transform.up * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("TurnTable"))
        {
            TurnTable turnTable = collision.GetComponent<TurnTable>();
            if (turnTable.onSide)
            {
                Destroy(gameObject);
            }
        }
    }
}
