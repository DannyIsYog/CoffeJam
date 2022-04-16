using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue Object", menuName = "Scriptable Object/Dialogue")]
public class DialogueObject : ScriptableObject
{
    public DialogueSentence[] sentences;
}

[Serializable]
public class DialogueSentence
{
    public string actor;
    [TextArea(3, 5)] public string sentence;
    public SoundEffectObject soundEffectObj;

    public void PlayAudio()
    {
        soundEffectObj.Play();
    }
}
