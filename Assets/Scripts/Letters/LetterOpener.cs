using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterOpener : MonoBehaviour
{
    [SerializeField] GameObject openLetterObj;
    [SerializeField] GameObject closedLetterObj;

    [SerializeField] Rigidbody2D rb;
    public List<BoxCollider2D> colliders;
    public int current = 0;

    private void Start()
    {
        rb.AddTorque(Random.Range(-0.01f, 0.01f), ForceMode2D.Impulse);
        rb.AddForce(new Vector2(Random.Range(-5, 5), Random.Range(3.5f, 8)), ForceMode2D.Impulse);
    }
    private void OnMouseEnter()
    {
        if (current >= colliders.Count) return;
        if (Input.GetMouseButton(0) && current < colliders.Count)
        {
            colliders[current].enabled = false;
            Debug.Log("Opened " + ((current + 1) * 10).ToString() + " % of the letter");
            current++;
            if (current >= colliders.Count)
            {
                openLetter();
                return;
            }
            colliders[current].enabled = true;

        }
    }

    private void openLetter()
    {
        closedLetterObj.SetActive(false);
        openLetterObj.SetActive(true);
        LetterSpawner.Instance.SpawnLetter();
    }
}
