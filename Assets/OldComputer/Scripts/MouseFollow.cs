using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    [SerializeField] private RectTransform mouseRect;
    [SerializeField] private Canvas parentCanvas;
    [SerializeField] private Camera cam;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Vector2 minCoords;
    [SerializeField] private Vector2 maxCoords;
    [SerializeField] private SoundEffectObject soundEffectClick;

    private Transform myTransform;

    public void Start()
    {
        Cursor.visible = false;
        myTransform = mouseRect.transform;
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            soundEffectClick.Play();
        }
        
        UpdateCursorPosition();

        Ray ray = new Ray(myTransform.position, Vector3.forward);
        Debug.DrawRay(myTransform.position, Vector3.forward * 2, Color.green);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity))
        {
            Debug.DrawRay(myTransform.position, Vector3.forward * 2, Color.red);
            Debug.Log(hitInfo.collider.name);
        }
    }

    private void UpdateCursorPosition()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentCanvas.transform as RectTransform,
            Input.mousePosition, cam,
            out Vector2 movePos);
        
        Vector3 mousePos = parentCanvas.transform.TransformPoint(movePos) + offset;
        mousePos.z = 1;
        mousePos.x = Mathf.Clamp(mousePos.x, minCoords.x, maxCoords.x);
        mousePos.y = Mathf.Clamp(mousePos.y, minCoords.y, maxCoords.y);
        mouseRect.transform.position = mousePos;
    }
}