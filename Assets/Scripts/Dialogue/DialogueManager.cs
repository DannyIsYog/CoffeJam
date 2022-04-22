using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private RectTransform textBox;
    [SerializeField] private TextMeshProUGUI speakerText;
    [SerializeField] private TextMeshProUGUI sentenceText;
    [SerializeField] private bool typeWrite;
    [SerializeField] private float typeSpeed = 20;

    [HideInInspector] public State state = State.Complete;

    public enum State
    {
        Complete,
        Running
    }

    private Queue<DialogueSentence> sentences;

    private void Awake()
    {
        sentences = new Queue<DialogueSentence>();
    }

    public void StartDialogue(StorySceneObject storySceneObject)
    {
        if (storySceneObject == null)
        {
            DisableTextBox();
            return;
        }

        EnableTextBox();
        sentences.Clear();
        state = State.Running;

        foreach (DialogueSentence sentence in storySceneObject.sentences)
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
        
        if (dialogueSentence.SpeakerName.Equals(" ", StringComparison.OrdinalIgnoreCase))
        {
            DisableTextBox();
            return;
        }

        EnableTextBox();
        string sentence = dialogueSentence.sentence;
        speakerText.text = dialogueSentence.SpeakerName;
        Color speakerColor = dialogueSentence.speaker.textColor;
        speakerText.color = new Color(speakerColor.r, speakerColor.g, speakerColor.b, 1);
        sentenceText.text = "";

        if (typeWrite)
        {
            StopAllCoroutines();
            StartCoroutine(TypeWrite(sentence));
        }
        else sentenceText.text = sentence;

        if (dialogueSentence.soundEffectObj != null)
            dialogueSentence.PlayAudio();
    }

    private IEnumerator TypeWrite(string sentence)
    {
        foreach (char letter in sentence)
        {
            sentenceText.text += letter;
            yield return new WaitForSeconds(1 / typeSpeed);
        }
    }

    public void EndDialogue()
    {
        speakerText.text = "";
        sentenceText.text = "";
        state = State.Complete;
    }

    public bool IsComplete()
    {
        return state == State.Complete;
    }

    private void EnableTextBox()
    {
        textBox.gameObject.SetActive(true);
    }

    public void DisableTextBox()
    {
        textBox.gameObject.SetActive(false);
    }
}