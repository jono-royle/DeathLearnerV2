using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    public Text DialogueSentence;
    public UnityEvent DialogueFinished;

    [TextArea(2, 10)]
    public string[] Sentences;

    private Queue<string> sentenceQueue;

    // Start is called before the first frame update
    void Start()
    {
        sentenceQueue = new Queue<string>(Sentences);
        DialogueSentence.text = sentenceQueue.Dequeue();
    }

    public void OnDialogueClick()
    {
        if(sentenceQueue.Count < 1)
        {
            DialogueFinished.Invoke();
            return;
        }
        DialogueSentence.text = sentenceQueue.Dequeue();
    }
}
