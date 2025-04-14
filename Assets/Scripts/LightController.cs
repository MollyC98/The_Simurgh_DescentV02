using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightController : MonoBehaviour
{

   public Light2D spotLight;

   public Light2D globalLight;

   // float spotIntensity = 1.5f;

    float globalIntensity =0.1f;
    
    private Color targetColor =Color.red;

    private bool isEclipseActive = false;



    //float globalIntensity = 1.5f; // very beautiful sunset color

   
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E)) {
            StartEclipse();
        }

        if (isEclipseActive)
        {

            if (globalLight != null)
            {
                globalLight.intensity = Mathf.MoveTowards(globalLight.intensity, globalIntensity, Time.deltaTime * 0.2f);
            }

            if (spotLight != null)
            {
                spotLight.color = Color.Lerp(spotLight.color, targetColor, Time.deltaTime * 0.2f);
            }

        }

    }

// public void SetIntensity(float newIntensity)
//     {
//         globalIntensity = newIntensity;
//     }


  public void SetLightColor(string hexCode)
    {
        Color newColor;
        if (ColorUtility.TryParseHtmlString(hexCode, out newColor))
        {
            targetColor = newColor;
        }
    }




    
    public void StartEclipse()
    {
        isEclipseActive = true;
        SetLightColor("#CB0000"); // Blood red eclipse color
        globalIntensity = 0.1f;

        // Optionally end eclipse after a delay
        StartCoroutine(EndEclipseAfterSeconds(20f)); // Ends after 8 seconds
    }

    private IEnumerator EndEclipseAfterSeconds(float duration)
    {
        yield return new WaitForSeconds(duration);

        // Restore normal light
        SetLightColor("#FFFFFF"); // or whatever your normal light color is
        globalIntensity = 1.0f;

        yield return new WaitForSeconds(2f); // Optional: wait for fade to finish
        isEclipseActive = false;
    }




}

//SetLightColor("CB0000"); 


    // public void Eclipse(){

    //     SetLightColor("CB0000"); 
    //     globalIntensity = 0.1f;
        
    // }


