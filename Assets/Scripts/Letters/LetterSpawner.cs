using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterSpawner : MonoBehaviour
{
    public static LetterSpawner Instance { get; private set; }
    [SerializeField] GameObject LetterPrefab;

    [SerializeField] Transform spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
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
        Instantiate(LetterPrefab, spawnPoint);
    }
}
