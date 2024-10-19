using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    //Menus
    [SerializeField] private GameObject potionMenu;
    [SerializeField] private GameObject shopMenu;
    private bool menuOpen;
    private void OnEnable()
    {
        InputMonitor._clickedObject += ShowMenu;
    }
    private void OnDisable()
    {
        InputMonitor._clickedObject -= ShowMenu;
    }

    private void ShowMenu(GameObject obj)
    {
        if (!menuOpen)
        {
            if (obj.tag == "craft")
                potionMenu.SetActive(true);
            else if (obj.tag == "shop")
                shopMenu.SetActive(true);

            menuOpen = true;
        }
    }

    public void closePotionMenu()
    {
        if(potionMenu.activeSelf)
        {
            potionMenu.SetActive(false);
        }

        menuOpen = false;
    }

    public void closeShopMenu()
    {
        if (shopMenu.activeSelf)
        {
            shopMenu.SetActive(false);
        }

        menuOpen= false;
    }
}
