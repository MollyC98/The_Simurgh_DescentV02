using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroTransition : MonoBehaviour
{


void OnEnable()
{
    SceneManager.LoadScene("TitleScene");
}

}


