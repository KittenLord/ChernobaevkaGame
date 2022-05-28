using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopFunctional : MonoBehaviour
{
   
    public GameObject WeaponWindow, PlayerWindow, UtilityWindow;


    public void WeaponButtonPressed()
    {
        WeaponWindow.SetActive(true);

        PlayerWindow.SetActive(false);
        UtilityWindow.SetActive(false);

    }
    public void PlayerButtonPressed()
    {
        PlayerWindow.SetActive(true);
        
        WeaponWindow.SetActive(false);
        UtilityWindow.SetActive(false);

    }
    public void UtilityButtonPressed()
    {
        UtilityWindow.SetActive(true);
        
        
        PlayerWindow.SetActive(false);
        WeaponWindow.SetActive(false);

    }
}
