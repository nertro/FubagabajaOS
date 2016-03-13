using UnityEngine;
using System;

public class CustomEventArgs : EventArgs {
    public float Value;
    public string Message;

    public CustomEventArgs(float value)
    {
        this.Value = value;
    }
}
