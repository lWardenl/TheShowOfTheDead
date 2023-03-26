using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private void Update()
    {
        NextLevel();
    }
    void NextLevel()
    {
        if(Input.GetKeyDown(KeyCode.N)) 
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
