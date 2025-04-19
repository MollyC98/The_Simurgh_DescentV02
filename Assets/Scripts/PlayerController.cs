using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]


public class PlayerController : MonoBehaviour
{

  Rigidbody2D rb;
  HingeJoint2D hinge;

  public float speed = 1f;

  private bool canMove = false;

  private GameController gameController;
  public ObjectController objectController;
  public EclipsePhase eclipsePhase;
  //public AudioController audioController; this wrong i singleton



  public PlayableDirector timeline1;
  public PlayableDirector timeline2;
  public PlayableDirector timeline3;
  

  public Canvas canvas;
    public Image letter1;
    public Image letter2;
    public Image letter3;
  //  public Image letter4;

AudioController audioController;

  // Start is called before the first frame update
  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    hinge = GetComponent<HingeJoint2D>();

    gameController = FindObjectOfType<GameController>();
    
          if (AudioController.instance == null)
      {
        GameObject newAudioManager = Instantiate(Resources.Load("AudioManager")) as GameObject;
        audioController = newAudioManager.GetComponent<AudioController>();
      }
      else
      {
          audioController = AudioController.instance;
      }
    
    Cursor.visible = false;
    //StartCoroutine(PrepareForTimeline1(0f));
    
    StartCoroutine(EnableCanvasWithDelay(1,3f));

  
    //first fade in then canvas then timeline1

   // StartCoroutine(EnableInputWithDelay(7f)); // Start the coroutine with a 5 second delay

  }

  // Coroutine to enable player movement after a delay
  IEnumerator EnableInputWithDelay(float delay)
  {
    
    yield return new WaitForSeconds(delay); // Wait for the specified delay

    Debug.Log("trigger coroutine");
    objectController.TriggerCoroutine();
    canMove = true; // Enable player movement after delay

    rb.bodyType = RigidbodyType2D.Dynamic;
    rb.freezeRotation = false;

  }


    IEnumerator DisableInputWithDelay(float delay)
  {
    yield return new WaitForSeconds(delay); // Wait for the specified delay
    canMove = false; // disable player movement after delay

  }

  private IEnumerator EnableCanvasWithDelay(int index,float delay)
  {

    //
   
    yield return new WaitForSeconds(delay); // Wait for 5 seconds
    // scroll sfx
    audioController.ScrollSFX();
    canvas.enabled = true; // Enable the canvas after the delay
    Time.timeScale = 0;


    letter1.gameObject.SetActive(false);
    letter2.gameObject.SetActive(false);
    letter3.gameObject.SetActive(false);
    //letter4.gameObject.SetActive(false);



    switch (index)
        {
            case 1:
                letter1.gameObject.SetActive(true);

                // level 1
                LevelController.Instance.SetLevel(1);
                eclipsePhase.Initialize();
                Debug.Log("welcome to level1");


                timeline1.Play(); // either pay here or on awake
                StartCoroutine(EnableInputWithDelay(7f)); //level1
                break;
            case 2:
                letter2.gameObject.SetActive(true);

                // level 2
                LevelController.Instance.SetLevel(2);
                eclipsePhase.Initialize();
                Debug.Log("welcome to level2");


                timeline3.Play();
                StartCoroutine(EnableInputWithDelay(5f)); //level2
                break;
            case 3:
                letter3.gameObject.SetActive(true);

                // level 3
                LevelController.Instance.SetLevel(3);
                eclipsePhase.Initialize();
                Debug.Log("welcome to level3");

                timeline3.Play();
                StartCoroutine(EnableInputWithDelay(5f)); //level3
                break;
            // case 4:
            //       letter4.gameObject.SetActive(true);
            //     // game end 
            //       timeline3.Play();
                 
            //     break;
            default:
                Debug.LogWarning("Invalid image index");
                break;
        }
  }

  // level 1 - beginning rudabhe
  // level 2 object 1 rostam
  // level 3 object 2
  // level 4 object 3

  // end

  void FixedUpdate(){
        
    if (canMove) {

      Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

      //  Vector2 targetPos = new Vector2(mousePos.x, rb.position.y);
      //  rb.MovePosition(targetPos);
      //  rb.position = Vector2.Lerp(rb.position, targetPos, speed * Time.fixedDeltaTime);

      float direction = mousePos.x - rb.position.x;

      rb.velocity = new Vector2(direction * speed * 0.7f, rb.velocity.y);
    } 

  }

  void Update(){

     if((Input.GetKeyDown(KeyCode.Space)) && canvas.enabled == true ){
       canvas.enabled = false;
       Time.timeScale = 1;
     }


  }



IEnumerator PrepareForTimeline2(float delay)
{
    Vector3 centerPos = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 10f));
    centerPos.z = transform.position.z;
    centerPos.y = transform.position.y;
    
    float duration = 2f;
    float elapsed = 0f;
    Vector3 startPos = transform.position;
    
    while (elapsed < duration)
    {
        transform.position = Vector3.Lerp(startPos, centerPos, elapsed/duration);
        elapsed += Time.deltaTime;
        yield return null;
    }
    
    transform.position = centerPos;
    yield return new WaitForSeconds(delay); // Wait for the specified delay
    timeline2.Play();
  }


  private void OnTriggerEnter2D(Collider2D collider)
  {
    Debug.Log("collision w object: " + collider.gameObject.name);

    if (collider.CompareTag("lotus"))
    {
      
    Destroy(collider.gameObject);

      gameController.RemainingTime(-10f); 
      audioController.OmenSFX();
    }
    else if (collider.CompareTag("omen"))
    {

      
    Destroy(collider.gameObject);

      gameController.RemainingTime(-10f); 
      audioController.OmenSFX();
    } 
   else if (collider.CompareTag("object1"))
{

    Destroy(collider.gameObject);

    Debug.Log("stop coroutine");
    objectController.StopAllCoroutines();

    audioController.BirdSFX();

    canMove = false;

    rb.velocity = Vector2.zero;
    rb.isKinematic = true;

    rb.freezeRotation = true;
    rb.rotation = 0f; // Reset to default rotation
 
    objectController.StartCoroutine(objectController.FeatherWave(2f));
     
      //StartCoroutine(TimelineWithDelay(3f));
      StartCoroutine(PrepareForTimeline2(1f));

      StartCoroutine(EnableCanvasWithDelay(2,6f)); // or 3

      // if 2 enter level 2 if 3 enter level 3

}
else if (collider.CompareTag("object2"))
{

  
    Destroy(collider.gameObject);


   // objectController.levelStarted = false;
    Debug.Log("stop coroutine");
    objectController.StopAllCoroutines();
   
    audioController.BirdSFX();

    canMove = false;

    rb.velocity = Vector2.zero;
    rb.isKinematic = true;

    rb.freezeRotation = true;
    rb.rotation = 0f; // Reset to default rotation
 
    objectController.StartCoroutine(objectController.FeatherWave(2f));
     
      //StartCoroutine(TimelineWithDelay(3f));
      StartCoroutine(PrepareForTimeline2(1f));

      StartCoroutine(EnableCanvasWithDelay(3,6f)); // 
      // if 2 enter level 2 if 3 enter level 3

} else if (collider.CompareTag("object3"))
{


    Destroy(collider.gameObject);

   // objectController.levelStarted = false;
    Debug.Log("stop coroutine");
    objectController.StopAllCoroutines();
   
    audioController.BirdSFX();

    canMove = false;

    rb.velocity = Vector2.zero;
    rb.isKinematic = true;

    rb.freezeRotation = true;
    rb.rotation = 0f; // Reset to default rotation
 
    objectController.StartCoroutine(objectController.FeatherWave(2f));
     
      //StartCoroutine(TimelineWithDelay(3f));
      StartCoroutine(PrepareForTimeline2(1f));


    //  StartCoroutine(EnableCanvasWithDelay(4,8f)); // 
      // if 2 enter level 2 if 3 enter level 3
        
        Debug.Log("you win");
        StartCoroutine(WinAfterDelay(1f));



} else if(collider.CompareTag("feather")){

   Destroy(collider.gameObject);

}






// if real object passes by you you lose

//    SceneManager.LoadScene("LoseScene");
//       Debug.Log("you lose");



    //collider object1 
    //collider object2
  }


IEnumerator WinAfterDelay(float delay)
{
    yield return new WaitForSeconds(delay);
    SceneManager.LoadScene("WinScene");
}

   
}












// movement using transform
  // // Update is called once per frame
    // void Update()
    // {
    //     FollowMousePosition(moveSpeed);

      
    // }

    // private void FollowMousePosition(float moveSpeed){
    //     transform.position = Vector2.MoveTowards(transform.position, GetWorldPositionFromMouse(), moveSpeed*Time.deltaTime);
    // }

    // private Vector2 GetWorldPositionFromMouse(){

    //     return mainCamera.ScreenToWorldPoint(Input.mousePosition);

    // }


   // every level stays the same in terms of difficulty, just more eclipse faces (more and faster objects)

   // fixing the cut of line of transforming (that could also be a difficulty factors)




