using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Options : MonoBehaviour
{

    bool paused;

    public Canvas canvas;
    AudioController audioController;

    public Image buttonImage;


    private Color musicEnabledColor; 
    private Color musicDisabledColor; 


    private void Awake()
    {
      //  audioController = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioController>();
    }

    void Start()
    {
        ColorUtility.TryParseHtmlString("#FFFFFF", out musicEnabledColor); 
        ColorUtility.TryParseHtmlString("#989898", out musicDisabledColor); 
    }

    void Update(){

        if (Input.GetKeyDown(KeyCode.Space)){
            Debug.Log("space is pressed");
            PauseGame();
        }
    }

    public void PauseMusic(){

    audioController.PauseMusic();

    // if (audioController.musicSource.isPlaying)
    //     {
    //         buttonImage.color = musicEnabledColor;
        
    //     }
    //     else
    //     {
    //     buttonImage.color = musicDisabledColor;

    //     }
    }

    public void PauseGame(){

    paused = !paused;

		if (paused == true)
		{
            canvas.enabled = true;
			Time.timeScale = 0;
            Debug.Log ("'Pause' Called");
		}

		else
		{
            canvas.enabled = false;
			Time.timeScale = 1;
            Debug.Log ("'Unpause' Called");
		}

    }


    // public void ResumeGame(){

               

    // }

    public void RetryGame(){

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        Time.timeScale=1;

    }

    public void HomePage(){

    SceneManager.LoadScene("SampleScene");

    Time.timeScale=1;

    }

}
