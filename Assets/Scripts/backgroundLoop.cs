using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundloop : MonoBehaviour
{
    public GameObject[] levels; // Background layers (e.g., clouds, mountains, etc.)
    public float[] scrollSpeeds; // Scroll speed per layer
    public int horizontalIndex = 0; // Index of the cloud layer that should scroll horizontally
    public float choke;

    private Camera mainCamera;
    private Vector2 screenBounds;

    void Start()
    {
        mainCamera = Camera.main;
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));

        for (int i = 0; i < levels.Length; i++)
        {
            bool isHorizontal = (i == horizontalIndex);
            loadChildObjects(levels[i], isHorizontal);
        }
    }

    void loadChildObjects(GameObject obj, bool isHorizontal)
    {
        float objectSize = isHorizontal
            ? obj.GetComponent<SpriteRenderer>().bounds.size.x - choke
            : obj.GetComponent<SpriteRenderer>().bounds.size.y - choke;

        int needed = Mathf.CeilToInt(((isHorizontal ? screenBounds.x : screenBounds.y) * 2) / objectSize);
        GameObject clone = Instantiate(obj);

        for (int i = 0; i <= needed; i++)
        {
            GameObject c = Instantiate(clone);
            c.transform.SetParent(obj.transform);
            c.transform.localScale = Vector3.one;

            if (isHorizontal)
                c.transform.position = new Vector3(objectSize * i, obj.transform.position.y, obj.transform.position.z);
            else
                c.transform.position = new Vector3(obj.transform.position.x, objectSize * i, obj.transform.position.z);

            c.name = obj.name + "_" + i;
        }

        Destroy(clone);
        Destroy(obj.GetComponent<SpriteRenderer>()); // only if original is placeholder
    }

    void repositionChildObjects(GameObject obj, bool isHorizontal)
    {
        Transform[] children = obj.GetComponentsInChildren<Transform>();
        if (children.Length <= 1) return;

        List<Transform> validChildren = new List<Transform>();
        foreach (Transform t in children)
            if (t != obj.transform) validChildren.Add(t);

        validChildren.Sort((a, b) =>
            isHorizontal ? a.position.x.CompareTo(b.position.x) : a.position.y.CompareTo(b.position.y));

        GameObject firstChild = validChildren[0].gameObject;
        GameObject lastChild = validChildren[validChildren.Count - 1].gameObject;

        float halfSize = isHorizontal
            ? lastChild.GetComponent<SpriteRenderer>().bounds.size.x / 2
            : lastChild.GetComponent<SpriteRenderer>().bounds.size.y / 2;

        if (isHorizontal)
        {
            if (transform.position.x + screenBounds.x > lastChild.transform.position.x + halfSize)
            {
                firstChild.transform.SetAsLastSibling();
                firstChild.transform.position = new Vector3(
                    lastChild.transform.position.x + halfSize * 2,
                    lastChild.transform.position.y,
                    lastChild.transform.position.z);
            }
            else if (transform.position.x - screenBounds.x < firstChild.transform.position.x - halfSize)
            {
                lastChild.transform.SetAsFirstSibling();
                lastChild.transform.position = new Vector3(
                    firstChild.transform.position.x - halfSize * 2,
                    firstChild.transform.position.y,
                    firstChild.transform.position.z);
            }
        }
        else
        {
            if (transform.position.y + screenBounds.y > lastChild.transform.position.y + halfSize)
            {
                firstChild.transform.SetAsLastSibling();
                firstChild.transform.position = new Vector3(
                    lastChild.transform.position.x,
                    lastChild.transform.position.y + halfSize * 2,
                    lastChild.transform.position.z);
            }
            else if (transform.position.y - screenBounds.y < firstChild.transform.position.y - halfSize)
            {
                lastChild.transform.SetAsFirstSibling();
                lastChild.transform.position = new Vector3(
                    firstChild.transform.position.x,
                    firstChild.transform.position.y - halfSize * 2,
                    firstChild.transform.position.z);
            }
        }
    }

    void LateUpdate()
    {
        for (int i = 0; i < levels.Length; i++)
        {
            GameObject obj = levels[i];
            float speed = scrollSpeeds[i];
            bool isHorizontal = (i == horizontalIndex);

            if (isHorizontal)
            {
                obj.transform.position += Vector3.right * speed * Time.deltaTime;
            }
            else
            {
                obj.transform.position += Vector3.up * speed * Time.deltaTime;
            }

            repositionChildObjects(obj, isHorizontal);
        }
    }
}
