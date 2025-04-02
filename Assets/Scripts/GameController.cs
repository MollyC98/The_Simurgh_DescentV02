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
        DisplayTime(currentTime);
        
    }


 void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

       
        if (timeIsRunning)
        {
            if (currentTime > 0)
            {
                currentTime -= Time.deltaTime;
                currentTime= Mathf.Clamp(currentTime, 0, Mathf.Infinity); // Ensure it never goes negative
                DisplayTime(currentTime);
                clock.fillAmount = Mathf.InverseLerp(0, duration, currentTime);
            }
            else
            {
                Debug.Log("Time ran out! Loading Lose_timeout scene...");
                timeIsRunning = false;
              //  SceneManager.LoadScene("Lose_timeout");
            }
        }
    }

    public void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }


    public void RemainingTime(float amount)
    {
        currentTime += amount;
        currentTime= Mathf.Clamp(currentTime, 0, Mathf.Infinity); // Ensure it never goes negative
        DisplayTime(currentTime);
        clock.fillAmount = Mathf.InverseLerp(0, duration, currentTime);
        
    }


}
