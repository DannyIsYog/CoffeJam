using System;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "New Sound Effect", menuName = "Scriptable Object/Sound Effect")]
public class SoundEffectObject : ScriptableObject
{
    public AudioClip clip;
    public Vector2 volume = new Vector2(0.5f, 0.5f);
    public Vector2 pitch = new Vector2(1, 1);

    #region PreviewCode

# if UNITY_EDITOR
    private AudioSource _previewer;

    private void OnEnable()
    {
        _previewer = EditorUtility
            .CreateGameObjectWithHideFlags("AudioPreview", HideFlags.HideAndDontSave, typeof(AudioSource))
            .GetComponent<AudioSource>();
    }

    private void OnDisable()
    {
        DestroyImmediate(_previewer.gameObject);
    }

    private void PlayPreview()
    {
        Play(_previewer);
    }

    private void StopPreview()
    {
        _previewer.Stop();
    }
#endif

    #endregion

    public AudioSource Play(AudioSource audioSource = null)
    {
        if (clip == null)
            throw new Exception($"Missing sound clip for {name}");

        AudioSource source = audioSource;
        if (source == null)
        {
            GameObject obj = new GameObject("Sound", typeof(AudioSource));
            source = obj.GetComponent<AudioSource>();
        }

        source.clip = clip;
        source.volume = Random.Range(volume.x, volume.y);
        source.pitch = Random.Range(pitch.x, pitch.y);

        source.Play();

#if UNITY_EDITOR
        if (source != _previewer)
            Destroy(source.gameObject, source.clip.length / source.pitch);
#else
            Destroy(source.gameObject, source.clip.length / source.pitch);
#endif
        return source;
    }
    
    [CustomEditor(typeof(SoundEffectObject))]
    private class MyEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            SoundEffectObject SoundEffectObject = (SoundEffectObject)target;
            if (GUILayout.Button("Play Preview"))
                SoundEffectObject.PlayPreview();
            
            if (GUILayout.Button("Stop Preview"))
                SoundEffectObject.StopPreview();
        }
    }
}