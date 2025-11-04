using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class IntroToDialogue : MonoBehaviour
{
    public GameObject IntroText;
    public GameObject PanelDialogue;
    public float delay = 10f;
    public float fadeDuration = 2f;
    public DialogueScene dialogueScene;
    private CanvasGroup introCanvasGroup;

    void Start()
    {
        if (IntroText != null)
        {
            introCanvasGroup = IntroText.GetComponent<CanvasGroup>();
            if (introCanvasGroup == null)
                introCanvasGroup = IntroText.AddComponent<CanvasGroup>();

            IntroText.SetActive(true);
            introCanvasGroup.alpha = 1f;
        }

        if (PanelDialogue != null)
            PanelDialogue.SetActive(false);

        Invoke(nameof(StartFadeOut), delay);
    }

    void StartFadeOut()
    {
        if (IntroText != null)
            StartCoroutine(FadeOutAndSwitch());
    }

    IEnumerator FadeOutAndSwitch()
    {
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            introCanvasGroup.alpha = alpha;
            yield return null;
        }

        IntroText.SetActive(false);

        if (PanelDialogue != null)
            PanelDialogue.SetActive(true);

        if (dialogueScene != null)
            dialogueScene.AfficherDialogue();
    }
}