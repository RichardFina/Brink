using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteLv2 : MonoBehaviour
{public void NextLevel()
    {
        Debug.Log("Next Level!");
        SceneManager.LoadScene("Level 3");
    }    

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
