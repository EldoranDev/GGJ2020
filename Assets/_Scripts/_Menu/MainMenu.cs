using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Animator menuAnimatior;
    public void StatGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void TriggerMenu(string menuToTrigger)
    {
        menuAnimatior.SetTrigger(menuToTrigger);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
