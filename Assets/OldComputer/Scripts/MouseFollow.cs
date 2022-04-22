using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MouseFollow : MonoBehaviour
{
    [SerializeField] private EmailGenerator emailGenerator;
    [SerializeField] private RectTransform mouseRect;
    [SerializeField] private Canvas parentCanvas;
    [SerializeField] private Camera cam;
    [SerializeField] private Vector3 rayOffset;
    [SerializeField] private Vector2 minCoords;
    [SerializeField] private Vector2 maxCoords;
    [SerializeField] private SoundEffectObject soundEffectClick;
    [SerializeField] private int storySceneIdx;

    private Transform myTransform;
    private static GameController gameControllerInstance;

    public void Start()
    {
        Cursor.visible = false;
        myTransform = mouseRect.transform;
        gameControllerInstance = GameController.instance;
    }

    public void Update()
    {
        if (emailGenerator.allTaskComplete)
        {
            gameControllerInstance.computerSceneComplete = true;
            SceneManager.LoadScene(storySceneIdx);
        }
        
        UpdateCursorPosition();

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = new Ray(myTransform.position + rayOffset, Vector3.forward);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity))
            {
                if (hitInfo.transform.TryGetComponent(out ClickEvent clickEvent))
                {
                    if (emailGenerator.allTaskComplete &&
                        hitInfo.collider.name.Equals("EmailIcon", StringComparison.OrdinalIgnoreCase))
                        goto PlayAudio;

                    clickEvent.onClickResponse.Invoke(
                        hitInfo.transform.TryGetComponent(out EmailPlaceholder emailPlaceholder)
                            ? emailPlaceholder
                            : null);
                }
            }

            PlayAudio:
            soundEffectClick.Play();
        }
    }

    private void UpdateCursorPosition()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentCanvas.transform as RectTransform,
            Input.mousePosition, cam,
            out Vector2 movePos);

        Vector3 mousePos = parentCanvas.transform.TransformPoint(movePos);
        mousePos.z = 1;
        mousePos.x = Mathf.Clamp(mousePos.x, minCoords.x, maxCoords.x);
        mousePos.y = Mathf.Clamp(mousePos.y, minCoords.y, maxCoords.y);
        mouseRect.transform.position = mousePos;
    }
}