using UnityEngine;
using System;
using System.Collections;

public class GameCtrl : MonoBehaviour {
    [SerializeField]
    private DialogCtrl dialogCtrl;
    [SerializeField]
    private Player player;

    public static event EventHandler<CustomEventArgs> DialogueEvent;
    public static event EventHandler<CustomEventArgs> AnswerEvent;

	void Start () {
        dialogCtrl.AnswerEvent += OnAnswer;

        OnDialogue();
	}
	
	void Update () {
	
	}

    private void OnDialogue()
    {
        if (DialogueEvent == null)
            return;

        dialogCtrl.gameObject.SetActive(true);
        DialogueEvent(this, new CustomEventArgs(player.Reputation));
    }

    private void OnAnswer(object sender, CustomEventArgs e)
    {
        if (AnswerEvent == null)
            return;

        AnswerEvent(this, new CustomEventArgs(e.Value));
    }
}
