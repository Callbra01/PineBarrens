using System.Collections;
using TMPro;
using UnityEngine;

public class TypewriterEffect : MonoBehaviour
{
    public float delay = 0.17f;
    public string fullText; 

    private TextMeshProUGUI textMeshPro;

    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        textMeshPro.text = fullText;

        StartCoroutine(WriteText());
    }

    void Update()
    {
        if(textMeshPro.maxVisibleCharacters == fullText.Length)
        {
            FireScript.instance.toggleOverlay = true;
            StartCoroutine(HideText());
        }
    }
    
    IEnumerator HideText()
    {
        while (textMeshPro.alpha > 0)
        {
            textMeshPro.alpha--;
            yield return new WaitForSeconds(delay * 0.5f);
        }
    }

    IEnumerator WriteText()
    {
        textMeshPro.maxVisibleCharacters = 0;

        while (textMeshPro.maxVisibleCharacters < fullText.Length)
        {
            textMeshPro.maxVisibleCharacters++;
            yield return new WaitForSeconds(delay);
        }
    }
}