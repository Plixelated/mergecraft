using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    //Game Objects
    [SerializeField] private GameObject potionButton;
    [SerializeField] private GameObject shopButton;
    //Menus
    [SerializeField] private GameObject potionMenu;
    [SerializeField] private GameObject shopMenu;
    private void OnEnable()
    {
        InputMonitor._clickedObject += PotionMenu;
    }
    private void OnDisable()
    {
        InputMonitor._clickedObject -= PotionMenu;
    }

    private void PotionMenu(GameObject obj)
    {
        if (obj == potionButton)
            potionMenu.SetActive(true);

        if (obj == shopButton)
            shopMenu.SetActive(true);
    }

    public void closePotionMenu()
    {
        Debug.Log("CLOSED FOR BUSINESS");
        if(potionMenu.activeSelf)
        {
            potionMenu.SetActive(false);
        }
    }
}
