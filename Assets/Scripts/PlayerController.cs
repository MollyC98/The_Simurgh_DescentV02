using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Playables;


[RequireComponent(typeof(Rigidbody2D))]


public class PlayerController : MonoBehaviour
{

    Rigidbody2D rb;

   // private Camera mainCamera;
    public float speed = 1f;

    // public PlayableDirector timeline;
   private bool canMove = false;


// [SerializeField] private InputActionAsset input;


// public void EnableControls(){
//   input.Enable();
// }


// public void DisableControls(){
//   input.Disable();
// }




    // Start is called before the first frame update
    void Start()
    {

        Cursor.visible = false;
        
        rb = GetComponent<Rigidbody2D>();
        
        StartCoroutine(EnableInput(5f)); // Start the coroutine with a 10-second delay
      //  mainCamera = Camera.main;


    //   timeline.played += OnTimelineStart;
    //   timeline.stopped += OnTimelineEnd;
   
    // PlayerInput input = new PlayerInput();
   
   

    }

    // Coroutine to enable player movement after a delay
    IEnumerator EnableInput(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay
        canMove = true; // Enable player movement after delay
    }

    // private void OnTimelineStart(PlayableDirector pd)
    // {
    //     canMove = false;
    //      input.Disable();
        
    // }

    // private void OnTimelineEnd(PlayableDirector pd)
    // {
    //     canMove = true;
    //      input.Enable();
    // }



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
  
      private void OnTriggerEnter2D(Collider2D collider){
        Debug.Log("collision w object: " + collider.gameObject.name);
        Destroy(collider.gameObject);

        //if(object1) =100
        //if(object2) =-100
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