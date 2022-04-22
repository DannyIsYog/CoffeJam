using UnityEngine;

[RequireComponent(typeof(BackgroundController), typeof(DialogueManager))]
public class GameController : MonoBehaviour
{
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private BackgroundController backgroundController;
    
    public StorySceneObject currentStoryScene;
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