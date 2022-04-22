using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BackgroundController), typeof(DialogueManager))]
public class GameController : MonoBehaviour
{
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private BackgroundController backgroundController;
    [SerializeField] private int computerSceneIdx;
    [SerializeField] private int lettersSceneIdx;
    
    public StorySceneObject currentStoryScene;
    public bool computerSceneLoaded;
    public bool miniGameComplete;
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

        if (currentStoryScene.name.Equals("day01_09", StringComparison.OrdinalIgnoreCase) && !computerSceneLoaded)
        {
            SceneManager.LoadScene(computerSceneIdx);
            computerSceneLoaded = true;
        }

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
        dialogueManager.DisplayNextSentence();
    }
}