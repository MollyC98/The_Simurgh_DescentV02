using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
 
    public GameObject fakeObjectPrefab; // fake objects

    public GameObject omenPrefab; // only omen or turn omen
    
    public GameObject realObjectPrefab; // true objects // one game object

     public GameObject featherPrefab; // feather
    
    public float minRespawnTime = 0.1f;
    public float maxRespawnTime = 2.0f;

    public float minTransformTime = 0.0f;
    public float maxTransformTime = 3.0f; // toggle with this sand object speed to make level 2, level 3

    private Vector2 screenBounds;

    // private bool level1 = false; // one eclipse
    // private bool level2 = false; // two eclipse
    // private bool level3 = false; // three eclipse

    // or just this minRespawnTime = 0.1f and this velocity each level
    

    //public AudioController audioController;

    private List<GameObject> spawnedObjects = new List<GameObject>(); // list of spawned objects (fake objects)

    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width* 5 / 6, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(FakeObjectWave());
        StartCoroutine(OmenWave());
        StartCoroutine(TransformWave());
        StartCoroutine(RealObjectWave());
        // if eclipse phase only StartCoroutine(OmenWave()); and this minRespawnTime = 0.1f and this velocity else{
    }


    public void spawnFakeObject(){
        GameObject obj = Instantiate(fakeObjectPrefab); // clone objects as game object
        obj.transform.position = new Vector2(Random.Range(-screenBounds.x, screenBounds.x), -screenBounds.y);
        spawnedObjects.Add(obj); // add the game object to the list
    }

    IEnumerator FakeObjectWave(){
       while(true){
        yield return new WaitForSeconds(Random.Range(minRespawnTime, maxRespawnTime));
        spawnFakeObject();
       }
        
    }


  public void spawnOmen(){
        GameObject obj = Instantiate(omenPrefab); // clone objects as game object
        obj.transform.position = new Vector2(Random.Range(-screenBounds.x, screenBounds.x), -screenBounds.y);
       // spawnedObjects.Add(obj); // add the game object to the list
    }

    IEnumerator OmenWave(){
       while(true){
        yield return new WaitForSeconds(Random.Range(minRespawnTime, maxRespawnTime));
        spawnOmen();
       }
        
    }

    
public void spawnRealObject(){
        GameObject obj = Instantiate(realObjectPrefab); // clone objects as game object
        obj.transform.position = new Vector2(Random.Range(-screenBounds.x, screenBounds.x), -screenBounds.y);
       // spawnedObjects.Add(obj); // add the game object to the list
    }

public IEnumerator RealObjectWave(){
        yield return new WaitForSeconds(Random.Range(10, 20));
        spawnRealObject();


        //yield return new WaitForSeconds(5f);
        //spawnRealObject();

    }


public void spawnFeather(){
        GameObject obj = Instantiate(featherPrefab); // clone objects as game object
        obj.transform.position = new Vector2(0f, -screenBounds.y +screenBounds.y/6);
       // spawnedObjects.Add(obj); // add the game object to the list
    }

public IEnumerator FeatherWave(float delay){
        yield return new WaitForSeconds(delay);
        spawnFeather();


        //yield return new WaitForSeconds(5f);
        //spawnRealObject();

    }



    IEnumerator TransformWave(){
        while(true){
            yield return new WaitForSeconds(Random.Range(minTransformTime, maxTransformTime));
            
            // if (spawnedObjects.Count > 0)
            // {
                List<GameObject> currentObjects = new List<GameObject>(spawnedObjects);
                 spawnedObjects.Clear();

         foreach (GameObject oldObject in currentObjects){
                if (oldObject != null)
                {
                    Vector2 position = oldObject.transform.position;
                    Destroy(oldObject);
                   // spawnedObjects.RemoveAt(index);

                    GameObject newObject = Instantiate(omenPrefab, position, Quaternion.identity);
                    spawnedObjects.Add(newObject);
                    Debug.Log(newObject.transform.position.y);
                }
            }
         }

       // }
        
    }


}


///real scissor (comes scissor)

//fake scissor *omen (comes scissor/stays omen)

// omen (comes omen/stays omen )