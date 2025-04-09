using UnityEngine;
using UnityEngine.Playables;

public class TimelinePauseAndWait : MonoBehaviour
{
    public PlayableDirector director; // refrencing the canvas
    private bool waitingForInput = false;

    public void PauseTimeline()
    {
        director.Pause();
        waitingForInput = true;
    }

    void Update()
    {
        if (waitingForInput && Input.GetKeyDown(KeyCode.Space))
        {
            director.Resume();
            waitingForInput = false;
        }
    }
}