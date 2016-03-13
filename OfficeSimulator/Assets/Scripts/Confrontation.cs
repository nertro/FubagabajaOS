using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Confrontation {
    public string Question { get; set; }
    public Dictionary<string, int> Answers;

    public Confrontation()
    {
        Answers = new Dictionary<string, int>();
    }
}
