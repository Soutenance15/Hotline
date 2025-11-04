using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

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

    public string sceneSuivante;

    [Header("Effets de texte")]
    public float delaiEntreLettres = 0.03f;
    private int index = 0;
    private bool enCoursAffichage = false;
    private Coroutine affichageCoroutine;

    private AudioManager audioManager;

    void Start()
    {
        audioManager = AudioManager.Instance;
        AfficherDialogue();
    }

    public void AfficherDialogue()
    {
        nomUI.text = dialogues[index].nom;
        spriteUI.sprite = dialogues[index].sprite;

        if (affichageCoroutine != null)
            StopCoroutine(affichageCoroutine);

        affichageCoroutine = StartCoroutine(AfficherTexteLettreParLettre(dialogues[index].phrase));
    }

    IEnumerator AfficherTexteLettreParLettre(string phrase)
    {
        enCoursAffichage = true;
        boutonContinue.interactable = false;
        phraseUI.text = "";

        AudioManager.Instance.PlayTypewriterSoundLoop();

        foreach (char c in phrase)
        {
            phraseUI.text += c;
            yield return new WaitForSeconds(delaiEntreLettres);
        }

        AudioManager.Instance.StopTypewriterSound();

        enCoursAffichage = false;
        boutonContinue.interactable = true;
    }

    public void Suivant()
    {
        if (enCoursAffichage)
        {
            StopCoroutine(affichageCoroutine);

            phraseUI.text = dialogues[index].phrase;

            AudioManager.Instance.StopTypewriterSound();

            enCoursAffichage = false;
            boutonContinue.interactable = true;
            return;
        }

        boutonContinue.interactable = false;
        index++;

        if (index >= dialogues.Length)
        {
            if (!string.IsNullOrEmpty(sceneSuivante))
                SceneManager.LoadScene(sceneSuivante);

            return;
        }

        AfficherDialogue();
    }
}
