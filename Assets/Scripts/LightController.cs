using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightController : MonoBehaviour
{

   public Light2D spotLight;

   public float targetIntensity = 10f;
   

    void Start()
    {

        if (spotLight != null)
        {
            spotLight.intensity = Mathf.MoveTowards(spotLight.intensity, targetIntensity, Time.deltaTime * 2f);
        }

    }



public void SetIntensity(float newIntensity)
    {
        targetIntensity = newIntensity;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
