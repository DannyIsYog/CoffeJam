using UnityEngine;

[CreateAssetMenu(fileName = "New Speaker", menuName = "Scriptable Object/Speaker")]
public class SpeakerObject : ScriptableObject
{
    public string speakerName;
    public Color textColor;
}