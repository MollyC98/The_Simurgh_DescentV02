using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
 
    public GameObject fakeObjectPrefab; // fake objects

    public GameObject omenPrefab; // only omen or turn omen
    public GameObject realObjectPrefab; // true objects // one game object
    public GameObject realObject2Prefab;
    public GameObject realObject3Prefab;
    public GameObject featherPrefab; // feather
    public GameObject darkSmokePrefab; // vfx
    
     float normalMinRespawnTime = 1f;
     float normalMaxRespawnTime = 5f;

     float eclipseMinRespawnTime = 0.1f;
     float eclipseMaxRespawnTime = 0.3f;

     float minTransformTime = 0.5f;
     float maxTransformTime = 1f; 

    private Vector2 screenBounds;


    // or just this minRespawnTime = 0.1f and this velocity each level

    public EclipsePhase eclipsePhase;


    public bool isRunning = false;



    private Coroutine fakeObjectCoroutine;
    private Coroutine omenCoroutine;
    private Coroutine transformCoroutine;
    private Coroutine realObjectCoroutine;



        public Sprite fakeSpriteLevel1;
        public Sprite fakeSpriteLevel2;
        public Sprite fakeSpriteLevel3;


    private List<GameObject> spawnedObjects = new List<GameObject>(); // list of spawned objects (fake objects)

    // Start is called before the first frame update
    void Start()

    {
        
     screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width*5/6, Screen.height, Camera.main.transform.position.z));


      // Vector3 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Mathf.Abs(Camera.main.transform.position.z)));

    
       // screenBounds = new Vector2(screenTopRight.x, screenTopRight.y);


        Debug.Log("screenBounds"+ screenBounds);
        Debug.Log("Camera.main: " + Camera.main);
        Debug.Log("Screen width: " + Screen.width + ", height: " + Screen.height);
        Debug.Log("Camera Z pos: " + Camera.main.transform.position.z);
        Debug.Log("ScreenToWorldPoint (Screen.width, Screen.height, 0): " + Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)));
        Camera.main.transform.position = new Vector3(-0.28f, 6.53f,-10f);
        Camera.main.orthographicSize = 0.24f;

    }


//every level roughly 1 minute 




 // if eclipse phase only StartCoroutine(OmenWave()); and this minRespawnTime = 0.1f and this velocity else{

public void TriggerCoroutine(){

    
        isRunning = true;
        
        // Stop any existing coroutines first
        StopAllCoroutines();
        
        if(eclipsePhase.isEclipseActive) {
            // Only spawn omen objects during eclipse
            omenCoroutine = StartCoroutine(OmenWave());
            Debug.Log("only omen");
        } else {
            // Normal spawning behavior when no eclipse
            fakeObjectCoroutine = StartCoroutine(FakeObjectWave());
            omenCoroutine = StartCoroutine(OmenWave());
            transformCoroutine = StartCoroutine(TransformWave());
        
        }
    int level = LevelController.Instance.currentLevel;
        switch (level) {
            case 1:
                realObjectCoroutine = StartCoroutine(RealObjectWave());
                break;
            case 2:
                realObjectCoroutine = StartCoroutine(RealObject2Wave());
                break;
            case 3:
                realObjectCoroutine = StartCoroutine(RealObject3Wave());
                break;
        }
 
   
}

private void StopAllCoroutines() {
    if(fakeObjectCoroutine != null) StopCoroutine(fakeObjectCoroutine);
    if(omenCoroutine != null) StopCoroutine(omenCoroutine);
    if(transformCoroutine != null) StopCoroutine(transformCoroutine);
    if(realObjectCoroutine != null) StopCoroutine(realObjectCoroutine);
}

public void StopCoroutine() {
    isRunning = false;
    StopAllCoroutines();
}



    public void spawnFakeObject(){
        GameObject obj = Instantiate(fakeObjectPrefab); // clone objects as game object
        obj.transform.position = new Vector2(Random.Range(-screenBounds.x, screenBounds.x), -screenBounds.y);
    

        // Set sprite based on current level
        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
        int level = LevelController.Instance.currentLevel;
        switch (level) {
            case 1:
                sr.sprite = fakeSpriteLevel1;
                break;
            case 2:
                sr.sprite = fakeSpriteLevel2;
                break;
            case 3:
                sr.sprite = fakeSpriteLevel3;
                break;
        }





        spawnedObjects.Add(obj); // add the game object to the list
    }

IEnumerator FakeObjectWave() {
    while (isRunning) {
     
       int numObjectsToSpawn = Random.Range(1, 4); // 2 or 3 objects

        for (int i = 0; i < numObjectsToSpawn; i++) {
            yield return new WaitForSeconds(Random.Range(normalMinRespawnTime, normalMaxRespawnTime));
            spawnFakeObject();
        }
    }
}

  public void spawnOmen(){
        GameObject obj = Instantiate(omenPrefab); // clone objects as game object
        obj.transform.position = new Vector2(Random.Range(-screenBounds.x, screenBounds.x), -screenBounds.y);
       // spawnedObjects.Add(obj); // add the game object to the list
    }

    IEnumerator OmenWave(){
       while(isRunning){

        

       
        if(eclipsePhase.isEclipseActive){
         int numObjectsToSpawn = Random.Range(6, 12); // 2 or 3 objects
            for (int i = 0; i < numObjectsToSpawn; i++) {
                yield return new WaitForSeconds(Random.Range(eclipseMinRespawnTime, eclipseMaxRespawnTime));
                spawnOmen();
            }
        } else{
            yield return new WaitForSeconds(Random.Range(normalMinRespawnTime, normalMaxRespawnTime));
            spawnOmen();
        }
    } 
    }  


public void spawnRealObject(){
        GameObject obj = Instantiate(realObjectPrefab); // clone objects as game object
        obj.transform.position = new Vector2(Random.Range(-screenBounds.x, screenBounds.x), -screenBounds.y);
       // spawnedObjects.Add(obj); // add the game object to the list
    }

//0-80


//0-50
public IEnumerator RealObjectWave(){
        yield return new WaitForSeconds(Random.Range(45f, 50f));
        spawnRealObject();
        //yield return new WaitForSeconds(5f);
        //spawnRealObject();
    }



public void spawnRealObject2(){
        GameObject obj = Instantiate(realObject2Prefab); // clone objects as game object
        obj.transform.position = new Vector2(Random.Range(-screenBounds.x, screenBounds.x), -screenBounds.y);
       // spawnedObjects.Add(obj); // add the game object to the list
    }

//50-100
public IEnumerator RealObject2Wave(){
        yield return new WaitForSeconds(Random.Range(95f, 100f));
        spawnRealObject2();      
    }
    



public void spawnRealObject3(){
        GameObject obj = Instantiate(realObject3Prefab); // clone objects as game object
        obj.transform.position = new Vector2(Random.Range(-screenBounds.x, screenBounds.x), -screenBounds.y);
       // spawnedObjects.Add(obj); // add the game object to the list
    }

//100-150
public IEnumerator RealObject3Wave(){
        yield return new WaitForSeconds(Random.Range(145f, 150f));
        spawnRealObject3();      
    }





public void spawnFeather(){
        GameObject obj = Instantiate(featherPrefab); // clone objects as game object
        obj.transform.position = new Vector2(0f, -screenBounds.y +screenBounds.y/3);
       // spawnedObjects.Add(obj); // add the game object to the list
    }

public IEnumerator FeatherWave(float delay){
        yield return new WaitForSeconds(delay);
        spawnFeather();
        //yield return new WaitForSeconds(5f);
        //spawnRealObject();

    }



    // IEnumerator TransformWave(){
    //     while(isRunning){
    //         yield return new WaitForSeconds(Random.Range(minTransformTime, maxTransformTime));
            
    //         // if (spawnedObjects.Count > 0)
    //         // {
    //             List<GameObject> currentObjects = new List<GameObject>(spawnedObjects);
    //              spawnedObjects.Clear();

    //      foreach (GameObject oldObject in currentObjects){
    //             if (oldObject != null)
    //             {
    //                 Vector2 position = oldObject.transform.position;

    //                // Spawn smoke effect at the same position
    //             GameObject smoke = Instantiate(darkSmokePrefab, position, Quaternion.identity);

    //              Destroy(oldObject);
    //                // spawnedObjects.RemoveAt(index);

    //             // Optional: Destroy smoke after 1 second
    //             Destroy(smoke, 0.5f); // adjust duration if needed


    //                 GameObject newObject = Instantiate(omenPrefab, position, Quaternion.identity);
    //                 spawnedObjects.Add(newObject);
    //                 //Debug.Log(newObject.transform.position.y);
    //             }
    //         }
    //      }

    //    // }
        
    // }

    //SmokeEffect happens here 

//     IEnumerator TransformWave(){
//     while(isRunning){
//         yield return new WaitForSeconds(Random.Range(minTransformTime, maxTransformTime));

//         List<GameObject> currentObjects = new List<GameObject>(spawnedObjects);
//         spawnedObjects.Clear();

//         foreach (GameObject oldObject in currentObjects){
//             if (oldObject != null){
//                 Vector2 position = oldObject.transform.position;

//                 // Spawn smoke effect at the same position
//                 GameObject smoke = Instantiate(darkSmokePrefab, position, Quaternion.identity);

//                 // Optional: Destroy smoke after 1 second
//                 Destroy(smoke, 1f); // adjust duration if needed

//                 Destroy(oldObject); // remove fake object

//                 // Replace with omen
//                 GameObject newObject = Instantiate(omenPrefab, position, Quaternion.identity);
//                 spawnedObjects.Add(newObject);
//             }
//         }
//     }
// }

IEnumerator TransformWave(){
        while(isRunning){
            yield return new WaitForSeconds(Random.Range(minTransformTime, maxTransformTime));
            // if (spawnedObjects.Count > 0)
            // {
  
            List<GameObject> currentObjects = new List<GameObject>(spawnedObjects);
            spawnedObjects.Clear();
            int index = 0;

         foreach (GameObject oldObject in currentObjects)
            {
             
               
                if (oldObject != null)
                {
                    Vector2 position = oldObject.transform.position;
                    // Spawn smoke effect at the same position
                 GameObject smoke = Instantiate(darkSmokePrefab, position, Quaternion.identity);

                    Destroy(oldObject, 0.0f);
                    currentObjects.RemoveAt(index);

                    //  // Optional: Destroy smoke after 1 second
                     Destroy(smoke, 0.5f); // adjust duration if needed
               


                     GameObject newObject = Instantiate(omenPrefab, position, Quaternion.identity);
                    //spawnedObjects.Add(newObject); -> DO NOT ADD THE OMEN PREFAB
                    break;
                    //Debug.Log(newObject.transform.position.y);
                }
                index++;
             
            }
         }

       // }
        
    }


}
