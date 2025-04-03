using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Playables;


[RequireComponent(typeof(Rigidbody2D))]


public class PlayerController : MonoBehaviour
{

  Rigidbody2D rb;

  public float speed = 1f;

  private bool canMove = false;

  private GameController gameController;

  public Canvas canvas;


  // Start is called before the first frame update
  void Start()
  {
    rb = GetComponent<Rigidbody2D>();

    gameController = FindObjectOfType<GameController>();
    
    Cursor.visible = false;
    
    StartCoroutine(EnableInputWithDelay(5f)); // Start the coroutine with a 5 second delay
  
  }

  // Coroutine to enable player movement after a delay
  IEnumerator EnableInputWithDelay(float delay)
  {
    yield return new WaitForSeconds(delay); // Wait for the specified delay
    canMove = true; // Enable player movement after delay
  }

  private IEnumerator EnableCanvasWithDelay(float delay)
  {
    yield return new WaitForSeconds(delay); // Wait for 5 seconds
    canvas.enabled = true; // Enable the canvas after the delay
  }

  void FixedUpdate(){
        
    if (canMove) {

      Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

      //  Vector2 targetPos = new Vector2(mousePos.x, rb.position.y);
      //  rb.MovePosition(targetPos);
      //  rb.position = Vector2.Lerp(rb.position, targetPos, speed * Time.fixedDeltaTime);

      float direction = mousePos.x - rb.position.x;

      rb.velocity = new Vector2(direction * speed * 0.5f, rb.velocity.y);
    }

  }
  
 

  private void OnTriggerEnter2D(Collider2D collider)
  {
    Debug.Log("collision w object: " + collider.gameObject.name);

    if (collider.CompareTag("lotus"))
    {
      gameController.RemainingTime(10f); 
    }
    else if (collider.CompareTag("lotus2"))
    {
      gameController.RemainingTime(-40f); 
    } 
    else if (collider.CompareTag("feather"))
    {
      gameController.RemainingTime(30f);
      // sfx
      // wait a few seconds
    
      StartCoroutine(EnableCanvasWithDelay(2f));
      //canMove = false;
    }
    
    Destroy(collider.gameObject);

    //collider object1 
    //collider object2
  }

   
}



// make movement only horizontal
// make speead slower so than it glides
// make cursor invisible 










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