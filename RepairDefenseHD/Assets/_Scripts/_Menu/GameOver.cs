using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void OnRestart()
    {
        SceneManager.LoadScene(2);
    }
    public void OnQuit()
    {
        Application.Quit();
    }
}
