using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InputMonitor : MonoBehaviour
{
    private InputActions inputActions;
    private Vector2 mousePosition;

    public static Action<Vector2> _mousePosition;
    public static Action<GameObject> _clickedObject;

    private void OnEnable()
    {
        inputActions.player.Enable();
    }
    private void Awake()
    {
        inputActions = new InputActions();

        var mouseClick = inputActions.player.mouseClick;

        mouseClick.performed += ctx => onClick(ctx);
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = inputActions.player.mousePosition.ReadValue<Vector2>();
        if (_mousePosition != null)
            _mousePosition(mousePosition);
    }

    private void onClick(InputAction.CallbackContext context)
    {
        Vector2 ray = Camera.main.ScreenToWorldPoint(mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray, Vector2.zero);

        if (hit)
            _clickedObject(hit.collider.gameObject);

    }
}
