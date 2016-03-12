using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    private int reputation;
    private float mood;
    [SerializeField]
    private int moodtweak; //for workspeed calculation
    private float task;
    private bool doingTask;
    private bool watchingCats;
    [SerializeField]
    private float catImpact;

	// Use this for initialization
	void Start () {
	
	}
	
    private void DoTask()
    {
        watchingCats = false;

        if (task < 100)
        {
            float workspeed = (mood / moodtweak) * Time.deltaTime;
            task += workspeed;
            Mathf.Clamp(task, 0, 100);
        }
    }


    private void WatchingCats()
    {
        doingTask = false;

        mood += catImpact * Time.deltaTime;
        Mathf.Clamp(mood, 0, 100);
    }
}
