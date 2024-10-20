using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputMonitor : MonoBehaviour
{
    private InputActions inputActions;
    private Vector2 mousePosition;

    public static Action<Vector2> _mousePosition;
    public static Action<GameObject> _clickedObject;

    public static Action _pause;

    private void OnEnable()
    {
        inputActions.player.Enable();
    }
    private void OnDisable()
    {
        inputActions.player.Disable();
    }
    private void Awake()
    {
        inputActions = new InputActions();

        var mouseClick = inputActions.player.mouseClick;
        var pause = inputActions.player.pause;

        mouseClick.performed += ctx => OnClick(ctx);
        pause.performed += ctx => OnPause(ctx);

    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = inputActions.player.mousePosition.ReadValue<Vector2>();
        if (_mousePosition != null)
            _mousePosition(mousePosition);
    }

    private void OnClick(InputAction.CallbackContext context)
    {
        Vector2 ray = Camera.main.ScreenToWorldPoint(mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray, Vector2.zero);


        if (hit)
        {
            print(hit.collider.tag);
            _clickedObject(hit.collider.gameObject);
        }

    }

    private void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("HOLUPWAITAMINUTE");
            if (_pause != null)
                _pause();
        }
    }
}
