using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EconomyManager : MonoBehaviour
{
    [SerializeField] private float playerStartAmount;
    [SerializeField] private float playerBank;
    [SerializeField] private TextMeshProUGUI playerBankDisplay;

    [SerializeField] private float storeStartAmount;
    [SerializeField] private float storeBank;

    private void OnEnable()
    {
        ShopInventory._buyItem += Buy;
    }

    private void OnDisable()
    {
        ShopInventory._buyItem -= Buy;
    }

    private void Start()
    {
        playerBank += playerStartAmount;
        playerBankDisplay.text = playerBank.ToString();

        storeBank += storeStartAmount;
    }
    public void Buy(float value)
    {
        playerBank -= value;
        playerBankDisplay.text = playerBank.ToString();

        storeBank += value;
    }

    public void Sell(float value)
    {
        playerBank += value;
        storeBank -= value;
    }
}
