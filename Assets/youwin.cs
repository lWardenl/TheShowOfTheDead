using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class youwin : MonoBehaviour
{
    public GameObject youWinCanvas;

    private void Start()
    {
        youWinCanvas.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            youWinCanvas.SetActive(!youWinCanvas.activeSelf);
        }
    }
}
