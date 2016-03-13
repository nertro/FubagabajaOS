using UnityEngine;
using System;
using System.Collections;

public class GameCtrl : MonoBehaviour {
    [SerializeField]
    private DialogCtrl dialogCtrl;
    [SerializeField]
    private Player player;

    public static event EventHandler<CustomEventArgs> DialogueEvent;

	void Start () {
        dialogCtrl.gameObject.SetActive(false);
	}
	
	void Update () {
	
	}

    private void OnDialogue()
    {
        if (DialogueEvent == null)
            return;

        DialogueEvent(this, new CustomEventArgs(player.Reputation));
    }
}
