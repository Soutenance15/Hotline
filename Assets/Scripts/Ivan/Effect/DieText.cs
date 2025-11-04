using TMPro;
using UnityEngine;

public class DieText : MonoBehaviour
{
    public string[] randomTexts =
    {
        "Boom! Kosa la fé batard ?",
        "Dans la guèle i lève pi ?",
        "Bang! I Kose pi don ?",
        "Ou la rodé, prend ça pou toué !",
        "Té Cabron, ou veu encore ?",
        "Té, aterre pas la paille, i dort pas ter là.",
        "Le respect s'apprend, ou la apprend ?'.",
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
