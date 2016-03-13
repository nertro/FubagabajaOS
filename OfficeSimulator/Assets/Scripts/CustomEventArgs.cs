using UnityEngine;
using System;

public class CustomEventArgs : EventArgs {
    public float value;
    public string message;

    public CustomEventArgs(float value)
    {
        this.value = value;
    }
}
