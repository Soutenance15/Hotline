using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Dialogue : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private TextMeshProUGUI speakerText;
    [SerializeField] private TextMeshProUGUI dialogText;
    [SerializeField] private Image portraitText;

    [SerializeField] private string[] speaker;
    [SerializeField] [TextArea] private string[] dialogueWords;
    [SerializeField] private Sprite[] portrait;

    private bool dialogueActivated;
    private int step;


    void Update()
    {
        if (Input.GetKeyDown("E") && dialogueActivated == true)
        {
            if (step >= speaker.Length)
            {
                canvas.SetActive(false);
                step = 0;
            }
            else
            {
                canvas.SetActive(true);
                speakerText.text = speaker[step];
                dialogText.text = dialogueWords[step];
                portraitText.sprite = portrait[step];
                step += 1;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            dialogueActivated = true;
        }
    }    
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            dialogueActivated = false;
            canvas.SetActive(false);
        }
    }
}
