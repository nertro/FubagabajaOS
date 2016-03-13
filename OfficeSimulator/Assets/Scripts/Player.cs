using UnityEngine;
using System;
using System.Collections;

public class Player : MonoBehaviour {

    private float mood;
    [SerializeField]
    private int moodtweak; //for workspeed calculation
    private float task;
    [SerializeField]
    private float catImpact;
    private float estimatedTaskTime; //in seconds/units

    private bool doingTask;
    private bool watchingCats;

    public float Mood { get { return this.mood; } }
    public float Reputation { get; set; }

    void Start()
    {
        GameCtrl.DialogueEvent += OnDialogue;
        //answer Event with cost here
    }

    private void OnDialogue(object sender, CustomEventArgs e)
    {
        watchingCats = false;
        doingTask = false;
    }

     private void DoTask()
    {
        if (!doingTask)
            return;

        watchingCats = false;

        if (task < 100)
        {
            float workspeed = mood / moodtweak;
            task += workspeed;
            task = Mathf.Clamp(task, 0, 100);

            if (workspeed > 0)
                estimatedTaskTime = (100 - task) / workspeed;
        }

        Invoke("DoTask", 1);
    }

     public void StartTask()
     {
         doingTask = true;
         DoTask();
     }

    private void WatchingCats()
    {
        if (!watchingCats)
            return;

        doingTask = false;

        mood += catImpact;
        mood = Mathf.Clamp(mood, 0, 100);

        Invoke("WatchingCats", 1);
    }

    public void StartWatchingCats()
    {
        watchingCats = true;
        WatchingCats();
    }
}
