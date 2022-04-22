using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fixer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameController.instance.canvas.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
