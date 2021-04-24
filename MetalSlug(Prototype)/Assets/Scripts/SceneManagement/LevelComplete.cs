using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{public void NextLevel()
    {
        Debug.Log("Next Level!");
        SceneManager.LoadScene("Level 2");
    }    

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
