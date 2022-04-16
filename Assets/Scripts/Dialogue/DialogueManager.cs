using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private bool typeWrite;
    [SerializeField] private float typeSpeed = 20;

    public static DialogueManager instance;
    private Queue<DialogueSentence> sentences;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        sentences = new Queue<DialogueSentence>();
        DontDestroyOnLoad(gameObject);
    }

    public void StartDialogue(DialogueObject dialogueObject)
    {
        Debug.Log("Starting dialogue obj: " + dialogueObject);
        sentences.Clear();

        foreach (DialogueSentence sentence in dialogueObject.sentences)
            sentences.Enqueue(sentence);

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        DialogueSentence dialogueSentence = sentences.Dequeue();
        string sentence = dialogueSentence.actor + ": " + dialogueSentence.sentence;
        dialogueText.text = "";

        if (typeWrite)
        {
            StopAllCoroutines();
            StartCoroutine(TypeWrite(sentence));
        }
        else dialogueText.text = sentence;
    }

    private IEnumerator TypeWrite(string sentence)
    {
        foreach (char letter in sentence)
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(1 / typeSpeed);
        }
    }

    public void EndDialogue()
    {
        dialogueText.text = "";
        Debug.Log("End of dialogue");
    }
}