using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class KillZone : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
{
     Debug.Log("Trigger detected with: " + other.gameObject.tag);
    if (other.CompareTag("object1") || other.CompareTag("object2") || other.CompareTag("object3") )
    {
        
        Debug.Log("you lose cuz didnt catch object");
         StartCoroutine(LoseAfterDelay(1f));
    }

}

IEnumerator LoseAfterDelay(float delay)
{
    yield return new WaitForSeconds(delay);
    SceneManager.LoadScene("LoseScene");
}


}
