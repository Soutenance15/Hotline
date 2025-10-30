using UnityEngine;

public class IntroToDialogue : MonoBehaviour
{
    public GameObject IntroText;
    public GameObject PanelDialogue;
    public float delay = 10f;

    void Start()
    {
        if (IntroText != null) IntroText.SetActive(true);
        if (PanelDialogue != null) PanelDialogue.SetActive(false);

        Invoke(nameof(SwitchToDialogue), delay);
    }

    void SwitchToDialogue()
    {
        if (IntroText != null) IntroText.SetActive(false);
        if (PanelDialogue != null) PanelDialogue.SetActive(true);
    }
}
