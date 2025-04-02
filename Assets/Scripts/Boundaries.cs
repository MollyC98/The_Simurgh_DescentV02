// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.Events;


// [RequireComponent(typeof(BoxCollider2D))]


// public class Boundaries : MonoBehaviour
// {
//     public Camera mainCamera;
//     BoxCollider2D bc;

//     public UnityEvent<Collider2D> ExittriggerFired;

//     // private Vector2 screenBounds;
//     // private float objectWidth;
//     // private float objectHeight;




//     private void Awake(){
//         boxCollider = GetComponent<BoxCollider2D>();
//         boxCollider.isTrigger = true;
//     }
//     // Start is called before the first frame update
//     void Start()
//     {
//         screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
//         objectWidth = transform.GetComponent<SpriteRenderer>().bounds.size.x/2;
//         objectHeight = transform.GetComponent<SpriteRenderer>().bounds.size.y/2;

//     }

//     // Update is called once per frame
//     void Update()
//     {
//         Vector3 viewPos = transform.position;
//         viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x , screenBounds.x * -1 );
//         viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y , screenBounds.y * -1 );
//         transform.position = viewPos;
//     }
// }
