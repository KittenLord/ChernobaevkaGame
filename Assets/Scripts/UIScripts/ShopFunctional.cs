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
        
        PlayerWindow.active = false;
        UtilityWindow.active = false;
        
    }
    public void PlayerButtonPressed()
    {
        PlayerWindow.SetActive(true);
        
        WeaponWindow.active = false;
        UtilityWindow.active = false;
        
    }
    public void UtilityButtonPressed()
    {
        UtilityWindow.SetActive(true);
        
        
        PlayerWindow.active = false;
        WeaponWindow.active = false;
        
    }
}
