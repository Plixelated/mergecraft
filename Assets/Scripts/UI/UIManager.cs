using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    //Menus
    [SerializeField] private GameObject potionMenu;
    [SerializeField] private GameObject shopMenu;

    private bool menuOpen;

    public static Action _shopMenuOpen;
    public static Action _shopMenuClose;
    public static Action _potionMenuOpen;
    public static Action _potionMenuClose;

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
            {
                if (_potionMenuOpen != null)
                    _potionMenuOpen();
                potionMenu.SetActive(true);
            }
            else if (obj.tag == "shop")
            {
                if(_shopMenuOpen != null)
                    _shopMenuOpen();

                shopMenu.SetActive(true);
            }

            menuOpen = true;
        }
    }

    public void closePotionMenu()
    {
        if(potionMenu.activeSelf)
        {
            if (_potionMenuClose != null)
                _potionMenuClose();
            potionMenu.SetActive(false);
        }

        menuOpen = false;
    }

    public void closeShopMenu()
    {
        if (shopMenu.activeSelf)
        {
            if (_shopMenuClose != null)
                _shopMenuClose();
            shopMenu.SetActive(false);
        }

        menuOpen= false;
    }
}
