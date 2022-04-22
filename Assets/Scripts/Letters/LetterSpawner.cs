using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LetterSpawner : MonoBehaviour
{
    public static LetterSpawner Instance { get; private set; }
    [SerializeField] GameObject LetterPrefab;
    [SerializeField] Transform spawnPoint;
    [SerializeField] int totalLetters;
    [SerializeField] int currentLetters = 0;

    [SerializeField] int loadStory;

    // Start is called before the first frame update
    void Start()
    {
        totalLetters = Random.Range(4, 10);
        SpawnLetter();
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void SpawnLetter()
    {
        if (currentLetters >= totalLetters)
        {
            SceneManager.LoadScene(loadStory);
        }
        currentLetters++;
        Instantiate(LetterPrefab, spawnPoint);
    }
}
