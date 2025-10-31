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

        // ✅ Lance le son une fois au début
        AudioManager.Instance.PlayTypewriterSoundLoop();

        foreach (char c in phrase)
        {
            phraseUI.text += c;
            yield return new WaitForSeconds(delaiEntreLettres);
        }

        // ✅ Arrête le son à la fin
        AudioManager.Instance.StopTypewriterSound();

        enCoursAffichage = false;
        boutonContinue.interactable = true;
    }

    public void Suivant()
    {
        // Si on clique pendant que le texte s'affiche
        if (enCoursAffichage)
        {
            StopCoroutine(affichageCoroutine);

            // ✅ On affiche directement tout le texte
            phraseUI.text = dialogues[index].phrase;

            // ✅ Et on arrête le son
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
