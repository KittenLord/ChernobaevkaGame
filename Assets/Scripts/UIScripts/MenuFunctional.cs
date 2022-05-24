using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuFunctional : MonoBehaviour
{
    public GameObject requiredWindow;
    public Button closeButton;

   
    public void ButtonPressed()
    {
        requiredWindow.SetActive(true);
    }
    public void CloseButtonPressed()
    {
        requiredWindow.SetActive(false);
    }
}
