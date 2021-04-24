using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    public void Level1() {
        SceneManager.LoadScene("Level 1");   // Loads Level 1
    }

    public void Level2() {
        SceneManager.LoadScene("Level 2");   // Loads Level 2
    }

    public void Level3() {
        SceneManager.LoadScene("Level 3");   // Loads Level 3
    }

    public void ReturnToMainMenu() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
