using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuFunctional : MonoBehaviour
{
    [SerializeField]
    GameObject ChooseLevelWindow, SpinSlotMachineWindow, UpgradeWindow, ShopWindow, SettingsWindow;
    [SerializeField]
    Button ChooseLevelButton, SpinSlotMachineButton, UpgradeButton, ShopButton;

    private void Start()
    {
        ChooseLevelWindow.active = false;
        SpinSlotMachineWindow.active = false;
        UpgradeWindow.active = false;
        ShopWindow.active = false;
        SettingsWindow.active = false;

    }

    private void Update()
    {
        
    }
    public void ChooseWindow()
    {
        ChooseLevelWindow.active = true;

    }
    public void SpinSlotMachine()
    {
        SpinSlotMachineWindow.active = true;
    }
    public void Upgrade()
    {
        UpgradeWindow.active = true;
    }
    public void Shop()
    {
        ShopWindow.active = true;
    }
    public void Settings()
    {
        SettingsWindow.active = true;
    }
}
