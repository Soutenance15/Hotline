using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueScene : MonoBehaviour
{
    [System.Serializable]
    public class LigneDialogue
    {
        public string nom;
        public Sprite sprite;
        [TextArea] public string phrase;
    }

    public LigneDialogue[] dialogues;

    public TextMeshProUGUI nomUI;
    public TextMeshProUGUI phraseUI;
    public Image spriteUI;

    public Button boutonContinue;

    private int index = 0;

    void Start()
    {
        AfficherDialogue();
    }

    void AfficherDialogue()
    {
        Debug.Log($"Index {index} : {dialogues[index].nom} - {dialogues[index].phrase}");
        nomUI.text = dialogues[index].nom;
        phraseUI.text = dialogues[index].phrase;
        spriteUI.sprite = dialogues[index].sprite;
    }

    public void Suivant()
    {
        boutonContinue.interactable = false;

        index++;

        if (index >= dialogues.Length)
        {
            Debug.Log("Fin du dialogue");
            return;
        }

        AfficherDialogue();
        boutonContinue.interactable = true;
    }
}
