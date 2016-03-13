using UnityEngine;
using System;
using System.Collections;

public class GameCtrl : MonoBehaviour {
    [SerializeField]
    private DialogCtrl dialogCtrl;

    public static event EventHandler DialogueEvent;

	void Start () {
        dialogCtrl.gameObject.SetActive(false);
	}
	
	void Update () {
	
	}

    private void OnDialogue()
    {
        if (DialogueEvent == null)
            return;

        DialogueEvent(this, new EventArgs());
    }
}
