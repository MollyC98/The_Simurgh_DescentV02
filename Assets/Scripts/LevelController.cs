using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public static LevelController Instance;

    public int currentLevel = 1; // 1 = Level 1, 2 = Level 2, 3 = Level 3

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void SetLevel(int level)
    {
        currentLevel = level;
        Debug.Log("Level set to: " + level);
    }
}