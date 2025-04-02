using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{

    public GameObject object1Prefab;
    public GameObject object2Prefab;
    public GameObject object3Prefab; // feather 
    
    
    public float minRespawnTime = 1.0f;
    public float maxRespawnTime = 3.0f;

    public float minTransformTime = 5.0f;
    public float maxTransformTime = 10.0f;

    private Vector2 screenBounds;

    private List<GameObject> spawnedObjects = new List<GameObject>(); 

    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width* 5 / 6, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(objectWave());
        StartCoroutine(featherWave());
        StartCoroutine(transformWave());
    }

// normal objects
    public void spawnObject(){
        GameObject obj = Instantiate(object1Prefab); // clone objects as game object
        obj.transform.position = new Vector2(Random.Range(-screenBounds.x, screenBounds.x), -screenBounds.y);
        spawnedObjects.Add(obj); // add the game object to the list
    }

    IEnumerator objectWave(){
       while(true){
        yield return new WaitForSeconds(Random.Range(minRespawnTime, maxRespawnTime));
        spawnObject();
       }
        
    }

// feather 
  public void spawnFeather(){
        GameObject obj = Instantiate(object3Prefab); // clone objects as game object
        obj.transform.position = new Vector2(Random.Range(-screenBounds.x, screenBounds.x), -screenBounds.y);
        spawnedObjects.Add(obj); // add the game object to the list
    }

    IEnumerator featherWave(){
       while(true){
        yield return new WaitForSeconds(Random.Range(20f, 30f));
        spawnFeather();
       }
        
    }











    IEnumerator transformWave(){
        while(true){
            yield return new WaitForSeconds(Random.Range(minTransformTime, maxTransformTime));
            
            if (spawnedObjects.Count > 0)
            {
                int index = Random.Range(0, spawnedObjects.Count); // pick a random object from list
                GameObject oldObject = spawnedObjects[index];

                if (oldObject != null)
                {
                    Vector2 position = oldObject.transform.position;
                    Destroy(oldObject);
                    spawnedObjects.RemoveAt(index);

                    GameObject newObject = Instantiate(object2Prefab, position, Quaternion.identity);
                    spawnedObjects.Add(newObject);
                }
            }

        }
        
    }


}
