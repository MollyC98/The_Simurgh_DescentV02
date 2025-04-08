using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundloop : MonoBehaviour
{
    public GameObject[] levels; // Array of background objects to loop
    private Camera mainCamera;
    private Vector2 screenBounds;

    public float choke; // overlap adjustment
    public float[] scrollSpeeds; // Array of scroll speeds for different layers

    void Start()
    {
        mainCamera = Camera.main;
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        
        foreach (GameObject obj in levels)
        {
            loadChildObjects(obj);
        }
    }

    void loadChildObjects(GameObject obj)
    {
        float objectHeight = obj.GetComponent<SpriteRenderer>().bounds.size.y - choke;
        int childsNeeded = (int)Mathf.Ceil(screenBounds.y * 2 / objectHeight);
        GameObject clone = Instantiate(obj) as GameObject;

        for (int i = 0; i <= childsNeeded; i++)
        {
            GameObject c = Instantiate(clone) as GameObject;
            c.transform.SetParent(obj.transform);
            c.transform.position = new Vector3(obj.transform.position.x, objectHeight * i, obj.transform.position.z);
            c.name = obj.name + i;
        }

        Destroy(clone);
        Destroy(obj.GetComponent<SpriteRenderer>()); // remove original renderer to avoid overlap
    }

    void repositionChildObjects(GameObject obj)
    {
        Transform[] children = obj.GetComponentsInChildren<Transform>();
        if (children.Length > 1)
        {
            GameObject firstChild = children[1].gameObject;
            GameObject lastChild = children[children.Length - 1].gameObject;

            float halfObjectHeight = lastChild.GetComponent<SpriteRenderer>().bounds.extents.y - choke;

            if (transform.position.y + screenBounds.y > lastChild.transform.position.y + halfObjectHeight)
            {
                // Reposition first child to bottom
                firstChild.transform.SetAsLastSibling();
                firstChild.transform.position = new Vector3(lastChild.transform.position.x, lastChild.transform.position.y + halfObjectHeight * 2, lastChild.transform.position.z);
            }
            else if (transform.position.y - screenBounds.y < firstChild.transform.position.y - halfObjectHeight)
            {
                // Reposition last child to top
                lastChild.transform.SetAsFirstSibling();
                lastChild.transform.position = new Vector3(firstChild.transform.position.x, firstChild.transform.position.y - halfObjectHeight * 2, firstChild.transform.position.z);
            }
        }
    }

    void LateUpdate()
    {
        for (int i = 0; i < levels.Length; i++)
        {
            GameObject obj = levels[i];
            float speed = scrollSpeeds[i]; // Get the scroll speed for this layer

            // Scroll background with respective scroll speed
            obj.transform.position += Vector3.up * speed * Time.deltaTime;

            // Reposition the background objects after scrolling
            repositionChildObjects(obj);
        }
    }
}
