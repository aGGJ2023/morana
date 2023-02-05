using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame() {
        SceneManager.LoadScene("EnemyScene");
    }
    public void QuitGame() 
    {
        Debug.Log("Quitting game");
        Application.Quit();
    }
}
