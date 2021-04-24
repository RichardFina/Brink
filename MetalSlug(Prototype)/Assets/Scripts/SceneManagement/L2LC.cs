using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class L2LC : MonoBehaviour
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
