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

    public static Action<float> _playerBalance;
    public float lastBalance;


    private void OnEnable()
    {
        ShopInventory._buyItem += Buy;
        PlayerInventory._sellPotion += Sell;
    }

    private void OnDisable()
    {
        ShopInventory._buyItem -= Buy;
        PlayerInventory._sellPotion -= Sell;
    }

    private void Update()
    {
        if (lastBalance != playerBank)
        {
            if (_playerBalance != null)
                _playerBalance(playerBank);
        }
    }

    private void Start()
    {
        playerBank += playerStartAmount;
        playerBankDisplay.text = playerBank.ToString();

    }

    public void Buy(float value)
    {
        lastBalance = playerBank;
        playerBank -= value;
        playerBankDisplay.text = playerBank.ToString();
    }

    public void Sell(float value)
    {
        lastBalance = playerBank;
        playerBank += value;
        playerBankDisplay.text = playerBank.ToString();

    }
}


