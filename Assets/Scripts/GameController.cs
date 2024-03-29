﻿using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BackgroundController), typeof(DialogueManager))]
public class GameController : MonoBehaviour
{
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private BackgroundController backgroundController;
    public RectTransform canvas;
    [SerializeField] private int computerSceneIdx;
    [SerializeField] private int lettersSceneIdx;

    public StorySceneObject currentStoryScene;
    public bool computerSceneLoaded;
    public bool lettersSceneLoaded;
    public bool computerSceneComplete;
    public bool lettersSceneComplete;
    public static GameController instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(canvas);
    }

    private void Start()
    {
        if (currentStoryScene != null)
        {
            dialogueManager.StartDialogue(currentStoryScene);
            backgroundController.SetBackground(currentStoryScene.background);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            NextSentence();


        if (dialogueManager.IsComplete())
        {
            if (currentStoryScene.nextStoryScene == null)
            {
                dialogueManager.DisableTextBox();
                return;
            }

            currentStoryScene = currentStoryScene.nextStoryScene;
            backgroundController.SwitchBackground(currentStoryScene.background);
            dialogueManager.StartDialogue(currentStoryScene);
        }
    }

    public void NextSentence()
    {
        if (IsMiniGamesDay()) return;
        canvas.gameObject.SetActive(true);
        dialogueManager.DisplayNextSentence();
        ResetMiniGames();
    }

    private bool IsMiniGamesDay()
    {
        if (currentStoryScene.name.Equals("day01_09", StringComparison.OrdinalIgnoreCase))
        {
            if (!computerSceneLoaded)
            {
                SceneManager.LoadScene(computerSceneIdx);
                computerSceneLoaded = true;
                return true;
            }

            if (computerSceneComplete && !lettersSceneLoaded)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                SceneManager.LoadScene(lettersSceneIdx);
                lettersSceneLoaded = true;
                return true;
            }
        }

        if (currentStoryScene.name.Equals("day02_09", StringComparison.OrdinalIgnoreCase))
        {

            if (!computerSceneLoaded)
            {
                SceneManager.LoadScene(computerSceneIdx);
                computerSceneLoaded = true;
                return true;
            }

            if (computerSceneComplete && !lettersSceneLoaded)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                SceneManager.LoadScene(lettersSceneIdx);
                lettersSceneLoaded = true;
                return true;
            }
        }

        if (currentStoryScene.name.Equals("day03_09", StringComparison.OrdinalIgnoreCase))
        {

            if (!computerSceneLoaded)
            {
                SceneManager.LoadScene(computerSceneIdx);
                computerSceneLoaded = true;
                return true;
            }

            if (computerSceneComplete && !lettersSceneLoaded)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                SceneManager.LoadScene(lettersSceneIdx);
                lettersSceneLoaded = true;
                return true;
            }
        }

        if (currentStoryScene.name.Equals("day04_11", StringComparison.OrdinalIgnoreCase))
        {

            if (!computerSceneLoaded)
            {
                SceneManager.LoadScene(computerSceneIdx);
                computerSceneLoaded = true;
                return true;
            }
        }

        if (currentStoryScene.name.Equals("day05_11", StringComparison.OrdinalIgnoreCase))
        {

            if (!lettersSceneLoaded)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                SceneManager.LoadScene(lettersSceneIdx);
                lettersSceneLoaded = true;
                return true;
            }
        }

        return false;
    }

    private void ResetMiniGames()
    {
        computerSceneComplete = false;
        computerSceneLoaded = false;
        lettersSceneComplete = false;
        lettersSceneLoaded = false;
    }
}