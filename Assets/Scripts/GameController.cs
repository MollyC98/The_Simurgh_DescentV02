using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
{

    [SerializeField] Image clock;
    [SerializeField] TMP_Text timeText;

    public bool timeIsRunning = true;
    [SerializeField] float duration, currentTime;


    [SerializeField] TMP_Text scoreText;
    


void Start()
    {
        if (duration <= 0) 
        {
            duration = 100f; // Ensure it starts at a valid time
        }
        currentTime = duration;
        timeIsRunning = true;
        clock.fillAmount = Mathf.InverseLerp(0, duration, currentTime);
        
    }


 void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

       
    }


public void RemainingTime(float amount)
    {
        if (!timeIsRunning) return;

        currentTime += amount;
        currentTime = Mathf.Clamp(currentTime, 0, Mathf.Infinity);
        clock.fillAmount = Mathf.InverseLerp(0, duration, currentTime);

        if (currentTime <= 0)
        {
            Debug.Log("you lose cause bar is empty");
            timeIsRunning = false;
             StartCoroutine(LoseAfterDelay(1f));
        }

}

IEnumerator LoseAfterDelay(float delay)
{
    yield return new WaitForSeconds(delay);
    SceneManager.LoadScene("LoseScene");
}

}