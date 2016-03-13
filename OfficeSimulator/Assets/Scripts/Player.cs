using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    private float mood;
    [SerializeField]
    private int moodtweak; //for workspeed calculation
    private float task;
    [SerializeField]
    private float catImpact;

    private bool doingTask;
    private bool watchingCats;

    public float Mood { get { return this.mood; } }
    public float Reputation { get; set; }
	
     private void DoTask()
    {
        if (!doingTask)
            return;

        watchingCats = false;

        if (task < 100)
        {
            float workspeed = mood / moodtweak;
            task += workspeed;
            Mathf.Clamp(task, 0, 100);
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
        Mathf.Clamp(mood, 0, 100);

        Invoke("WatchingCats", 1);
    }

    public void StartWatchingCats()
    {
        watchingCats = true;
        WatchingCats();
    }
}
