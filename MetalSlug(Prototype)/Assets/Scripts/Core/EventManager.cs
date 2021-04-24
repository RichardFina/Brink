using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void ShootAction();
    public static event ShootAction onButton;

    public static void OnShootEvent()
    {
        if (onButton != null)
            onButton();
    }
}
