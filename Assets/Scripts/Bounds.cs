using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class Bounds : MonoBehaviour
{
   
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;

                //     public float minX, maxX, minY, maxY;

                // void LateUpdate() 
                // { 
                //     Vector3 pos = transform.position; 
                //     pos.x = Mathf.Clamp(pos.x, minX, maxX); 
                //     pos.y = Mathf.Clamp(pos.y, minY, maxY); 
                //     transform.position = pos; 
                // } 


    // private void Awake(){
    //     boxCollider = GetComponent<BoxCollider2D>();
    //     boxCollider.isTrigger = true;
    // }
    // // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.size.x/2;
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.size.y/2;

    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 viewPos = transform.position;
       // viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x , screenBounds.x * -1 );
     viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1 + objectWidth, screenBounds.x - objectWidth);
        //viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y , screenBounds.y * -1 );
        //viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y * -1 + objectHeight, screenBounds.y - objectHeight);
        
        transform.position = viewPos;
    }
}
