using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    public Text DialogueSentence;
    public UnityEvent DialogueFinished;
    public Button contineButton;

    [TextArea(2, 10)]
    public string[] Sentences;

    private Queue<string> sentenceQueue;
    private bool acceptInput = false;

    // Start is called before the first frame update
    void Start()
    {
        sentenceQueue = new Queue<string>(Sentences);
        DialogueSentence.text = sentenceQueue.Dequeue();
        StartCoroutine(RevealText());
    }

    public void OnDialogueClick()
    {
        if (acceptInput)
        {
            if (sentenceQueue.Count < 1)
            {
                DialogueFinished.Invoke();
                return;
            }
            DialogueSentence.text = sentenceQueue.Dequeue();
            StartCoroutine(RevealText());
        }
    }

    private IEnumerator RevealText()
    {
        acceptInput = false;
        contineButton.interactable = false;
        var originalString = DialogueSentence.text;
        DialogueSentence.text = "";

        var numCharsRevealed = 0;
        while (numCharsRevealed < originalString.Length)
        {
            while (originalString[numCharsRevealed] == ' ')
                ++numCharsRevealed;

            ++numCharsRevealed;

            DialogueSentence.text = originalString.Substring(0, numCharsRevealed);

            yield return new WaitForSeconds(0.15f);
        }
        acceptInput = true;
        contineButton.interactable = true;
    }
}
