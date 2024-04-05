using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationFunctions : MonoBehaviour
{
    [SerializeField] private GameObject controlPanel;
    [SerializeField] private GameObject tutorialPanel;
    public void Quit()
    {
        Application.Quit();
    }
    public void CloseTutorial()
    {
        tutorialPanel.SetActive(false);
    }
    public void CloseControls()
    {
        controlPanel.SetActive(false);
    }
}
