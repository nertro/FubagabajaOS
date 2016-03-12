using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    private int reputation;
    private int mood;
    [SerializeField]
    private int moodtweak; //for workspeed calculation
    private float task;
    private bool doingTask;
    private bool watchingCats;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
    void Update()
    {
        if (doingTask)
        {
            DoTask();
        }
        else if (watchingCats)
        {
            
        }
    }

    private void DoTask()
    {
        if (task < 100)
        {
            float workspeed = (mood / moodtweak) * Time.deltaTime;
            task += workspeed;
            Mathf.Clamp(task, 0, 100);
        }
    }

}
