using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Scene", menuName = "Scriptable Object/Story Scene")]
public class StorySceneObject : ScriptableObject
{
    public Sprite background;
    public StorySceneObject nextStoryScene;
    public DialogueSentence[] sentences;
}

[Serializable]
public class DialogueSentence
{
    public SpeakerObject speaker;
    [TextArea(3, 5)] public string sentence;
    public SoundEffectObject soundEffectObj;

    public string SpeakerName => speaker.speakerName;

    public void PlayAudio()
    {
        soundEffectObj.Play();
    }
}