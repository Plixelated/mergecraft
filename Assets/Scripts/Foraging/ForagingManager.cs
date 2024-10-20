using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ForagingManager : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private float foragingTimer;
    public Image icon;

    private float fillValue = 1.0f;
    private bool finished = false;

    private bool notificationOn;
    [SerializeField] private TextMeshProUGUI notificationText;
    [SerializeField] private float notificationDuration = 1.0f;
    private Vector2 notifStartPos;
    private float timer;

    [SerializeField] private List<Ingredient> ingredientPool;
    private int selection;

    public static Action<Ingredient> _ingredient;
    public static Action _forageSFX;

    private void Start()
    {
        Debug.Log("AM RUNNING");
        notifStartPos = notificationText.rectTransform.position;
        StartCoroutine(FillBar());

        GetForagedItem();
    }

    private void Update()
    {
        float speed = 15f;

        if (finished)
            StartCoroutine(FillBar());
        if(timer > 0)
        {
            timer -= Time.deltaTime;

            notificationText.rectTransform.position = new Vector2(notifStartPos.x, notificationText.rectTransform.position.y - speed * Time.deltaTime);
            if(timer  < 0)
            {
                timer = 0;
                notificationText.gameObject.SetActive(false);
                notificationText.rectTransform.position = notifStartPos;
            }
        }
    }

    private IEnumerator FillBar()
    {
        finished = false;
        slider.value = 0f;

        float currentTime = 0;
        while(currentTime < foragingTimer)
        {
            float progressValue = Mathf.Lerp(0, fillValue, currentTime / foragingTimer);
            slider.value = progressValue;

            currentTime += Time.deltaTime;
            yield return null;
        }

        slider.value = fillValue;
        finished = true;

        OnFinish($"+3 Items Added"); //{ingredientPool[selection].ingredientName}");
    }

    private void GetForagedItem()
    {
        selection = UnityEngine.Random.Range(0, ingredientPool.Count - 1);
        icon.sprite = ingredientPool[selection].icon;
    }

    private void OnFinish(string notification)
    {
        if(_forageSFX != null)
            _forageSFX();

        for (int i = 0; i < 3; i++)
        {
            GetForagedItem();
            if (_ingredient != null)
                _ingredient(ingredientPool[selection]);
        }


        notificationText.gameObject.SetActive(true);
        notificationText.text = notification;

        timer = notificationDuration;
    }

}
