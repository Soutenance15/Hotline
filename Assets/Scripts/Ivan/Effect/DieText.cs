using TMPro;
using UnityEngine;

public class DieText : MonoBehaviour
{
    public string[] randomTexts =
    {
        "Boom! Kosa la fé batard",
        "Dans la guèle i lève pi",
        "Bang! I Kose pi don",
        "Aïe, prend ça pou toué.",
    };

    void Start()
    {
        Destroy(gameObject, 2f);

        TextMeshPro tmp = GetComponent<TextMeshPro>();

        if (null != tmp)
        {
            string randomMessage = randomTexts[Random.Range(0, randomTexts.Length)];
            tmp.text = randomMessage;
        }
    }
}
