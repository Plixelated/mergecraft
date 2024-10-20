using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static Action _resume;
    public static Action _exit;

    public void OnResume()
    {
        if(_resume != null)
            _resume();
    }

    public void OnExit()
    {
        if(_exit != null )
            _exit();
    }
}
