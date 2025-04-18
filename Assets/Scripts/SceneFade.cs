using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneFade : MonoBehaviour
{


private Image fadeImage;
private Color startColor;
private Color targetColor;

private float timer = 0f;
float fadeDuration = 6f;

private bool fading = true;

private void Awake(){
    fadeImage = GetComponent<Image>();
    
}

void Update(){
        if (fading){
            timer += Time.deltaTime;
            startColor = new Color(fadeImage.color.r,fadeImage.color.g,fadeImage.color.b, 1);
            targetColor = new Color(fadeImage.color.r,fadeImage.color.g,fadeImage.color.b, 0);
            
            fadeImage.color = Color.Lerp(startColor, targetColor, timer/fadeDuration);
            
           // Debug.Log("Final alpha: " + fadeImage.color.a);


            if (timer >= fadeDuration)
            {
                fading = false;
            }
        }
}


}
