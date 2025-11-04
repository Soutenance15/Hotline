using UnityEngine;

public class DieBlood : MonoBehaviour
{
    public SpriteRenderer sr;
    public Sprite[] possibleSprites;

    void Start()
    {
        if (possibleSprites != null && possibleSprites.Length > 0)
        {
            Debug.Log("Possibles Sprites pas Null");
            int randomIndex = Random.Range(0, possibleSprites.Length);
            sr = GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                Debug.Log("Assign les sprites");
                sr.sprite = possibleSprites[randomIndex];
                Debug.Log(randomIndex.ToString());
            }
        }
    }
}
