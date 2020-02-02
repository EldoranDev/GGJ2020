using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tutorial : MonoBehaviour
{
    [SerializeField]
    TMP_Text textField;

    [SerializeField]
    float secondsPerPannel;

    float currentPanelTime;

    int currentPanel = -1;


    private void Start()
    {
        currentPanelTime = secondsPerPannel;
    }

    void Update()
    {
        if (currentPanelTime >= secondsPerPannel)
        {
            currentPanelTime = 0;
            currentPanel++;
            switch (currentPanel)
            {
                case 0:
                    textField.text = "To move forward press W. \nA and D for sideways. \nAnd W for backwards. \nUse your mouse to look around.";
                    break;
                case 1:
                    textField.text = "Look for a tree or stone \nto harvest ressources with your left mousebutton.";
                    break;
                case 2:
                    textField.text = "Repair your walls with 10 wood and 10 stone each per repair.";
                    break;
                case 3:
                    textField.text = "Don't let the enemies destroy your walls and reach your main building.";
                    break;
                case 4:
                    Destroy(gameObject);
                    break;
                default:
                    break;
            }
        }
        currentPanelTime += Time.deltaTime;
    }
}
