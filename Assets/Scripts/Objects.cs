using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objects : MonoBehaviour
{

    public float speed = 1f;
    Rigidbody2D rb;
    private Vector2 screenBounds;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, speed*3);
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width* 5 / 6, Screen.height, Camera.main.transform.position.z));
    }

    // Update is called once per frame
    void Update()
    {
        
        if(transform.position.y > screenBounds.y * 2 ){
            Destroy(this.gameObject);
        }


    }


    // private void OnCollisionEnter2D(Collision2D collision){
    //     Debug.Log("collision w object: " + gameObject.name);
    //     Destroy(gameObject);
    // }


}
