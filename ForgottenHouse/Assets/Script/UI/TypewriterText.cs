using System.Collections;
using TMPro;
using UnityEngine;

public class TypewriterText : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public float typingSpeed = 0.02f;

    private string fullText;
    private Coroutine typingCoroutine;

    void Awake()
    {
        
        fullText = textComponent.text;
    }

    void OnEnable()
    {
        StartTyping();
    }

    public void StartTyping()
    {
        StopAllCoroutines();
        typingCoroutine = StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        textComponent.text = "";

        foreach (char letter in fullText)
        {
            textComponent.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    // Instantly finish the typing
    public void CompleteText()
    {
        StopAllCoroutines();
        textComponent.text = fullText;
    }
}