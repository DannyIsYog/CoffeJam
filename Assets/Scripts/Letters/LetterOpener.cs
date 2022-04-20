using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterOpener : MonoBehaviour
{
    public List<BoxCollider2D> colliders;
    public int current = 0;
    private void OnMouseEnter()
    {
        if (Input.GetMouseButton(0) && current < colliders.Count)
        {
            colliders[current].enabled = false;
            current++;
            colliders[current].enabled = true;
            Debug.Log("Opened " + (current * 10).ToString() + " % of the letter");
        }
    }
}
