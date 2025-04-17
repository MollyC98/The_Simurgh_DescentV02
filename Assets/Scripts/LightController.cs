using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightController : MonoBehaviour
{
    public Light2D spotLight;
    public Light2D globalLight;
    public int currentLevel = 1;

    private float targetIntensity = 0.1f;
    private Color targetColor = Color.red;
    private bool isEclipseActive = false;
    
    private bool isInitialized = false;
    
    // Track our own level timer
    private float levelTimer = 0f;
    private int lastTrackedLevel = 0;

    // Arrays to store eclipse times for each level
    private float[] firstLevelTimes;
    private float[] secondLevelTimes;
    private float[] thirdLevelTimes;
    
    // Track which eclipses have been triggered
    private bool[] firstLevelTriggered;
    private bool[] secondLevelTriggered;
    private bool[] thirdLevelTriggered;

    void Start()
    {
        // Initialize the arrays with appropriate sizes
        firstLevelTimes = new float[1];  // Level 1 has 1 eclipse
        secondLevelTimes = new float[2]; // Level 2 has 2 eclipses
        thirdLevelTimes = new float[3];  // Level 3 has 3 eclipses
        
        firstLevelTriggered = new bool[1];
        secondLevelTriggered = new bool[2];
        thirdLevelTriggered = new bool[3];
        
        GenerateEclipseTimes();
        isInitialized = true;
        
        // Set initial level
        if (LevelController.Instance != null)
            lastTrackedLevel = LevelController.Instance.currentLevel;
        else
            lastTrackedLevel = currentLevel;
            
        // Debug log the generated times
        DebugEclipseTimes();
    }

    void Update()
    {
        // Check if level has changed
        int currentLevelValue = LevelController.Instance != null ? LevelController.Instance.currentLevel : currentLevel;
        if (currentLevelValue != lastTrackedLevel)
        {
            // Level changed, reset timer and triggers for the new level
            Debug.Log("Level changed from " + lastTrackedLevel + " to " + currentLevelValue + ". Resetting timer.");
            levelTimer = 0f;
            lastTrackedLevel = currentLevelValue;
            
            // Reset the appropriate level triggers
            switch (currentLevelValue)
            {
                case 1:
                    ResetTriggers(firstLevelTriggered);
                    break;
                case 2:
                    ResetTriggers(secondLevelTriggered);
                    break;
                case 3:
                    ResetTriggers(thirdLevelTriggered);
                    break;
            }
        }
        
        // Update our custom level timer
        levelTimer += Time.deltaTime;
        
        // Handle lighting effects during eclipse
        if (isEclipseActive)
        {
            if (globalLight != null)
                globalLight.intensity = Mathf.MoveTowards(globalLight.intensity, targetIntensity, Time.deltaTime * 0.2f);

            if (spotLight != null)
                spotLight.color = Color.Lerp(spotLight.color, targetColor, Time.deltaTime * 0.2f);
        }
        
        // Check if it's time for an eclipse
        if (!isEclipseActive && isInitialized)
        {
            CheckForEclipseTrigger();
        }

        Debug.Log("Level: " + currentLevelValue + ", Time: " + levelTimer);
    }
    
    private void ResetTriggers(bool[] triggers)
    {
        for (int i = 0; i < triggers.Length; i++)
        {
            triggers[i] = false;
        }
    }

private void GenerateEclipseTimes()
{
    // Generate times for Level 1
    // For level 1, just place the eclipse somewhere in the middle of the range
    firstLevelTimes[0] = Random.Range(12f, 18f);
    
    // Generate times for Level 2
    float level2Duration = Random.Range(20f, 30f);
    // Create more space between eclipses by using smaller segments
    secondLevelTimes[0] = Random.Range(level2Duration * 0.15f, level2Duration * 0.3f);
    secondLevelTimes[1] = Random.Range(level2Duration * 0.7f, level2Duration * 0.85f);
    
    // Generate times for Level 3
    float level3Duration = Random.Range(30f, 40f);
    // Place eclipses at the beginning, middle, and end with good spacing
    thirdLevelTimes[0] = Random.Range(level3Duration * 0.1f, level3Duration * 0.2f);
    thirdLevelTimes[1] = Random.Range(level3Duration * 0.45f, level3Duration * 0.55f);
    thirdLevelTimes[2] = Random.Range(level3Duration * 0.8f, level3Duration * 0.9f);
}
    
    private void CheckForEclipseTrigger()
    {
        int level = LevelController.Instance != null ? LevelController.Instance.currentLevel : currentLevel;
        
        switch (level)
        {
            case 1:
                CheckLevelEclipses(firstLevelTimes, firstLevelTriggered, levelTimer);
                break;
            case 2:
                CheckLevelEclipses(secondLevelTimes, secondLevelTriggered, levelTimer);
                break;
            case 3:
                CheckLevelEclipses(thirdLevelTimes, thirdLevelTriggered, levelTimer);
                break;
            default:
                break;
        }
    }
    
    private void CheckLevelEclipses(float[] times, bool[] triggered, float currentTime)
    {
        for (int i = 0; i < times.Length; i++)
        {
            // If not yet triggered and the current time has passed the eclipse time
            if (!triggered[i] && currentTime >= times[i])
            {
                triggered[i] = true;
                StartEclipse();
                Debug.Log("Eclipse triggered at time: " + currentTime + " (scheduled for: " + times[i] + ")");
                break; // Only trigger one eclipse at a time
            }
        }
    }

    public void SetLightColor(string hexCode)
    {
        Color newColor;
        if (ColorUtility.TryParseHtmlString(hexCode, out newColor))
            targetColor = newColor;
    }

    public void StartEclipse()
    {
        isEclipseActive = true;
        SetLightColor("#CB0000");
        targetIntensity = 0.1f;
        StartCoroutine(EndEclipseAfterSeconds(5f));
    }

    private IEnumerator EndEclipseAfterSeconds(float duration)
    {
        yield return new WaitForSeconds(duration);
        SetLightColor("#FFFFFF");
        targetIntensity = 1.0f;
        yield return new WaitForSeconds(2f);
        isEclipseActive = false;
    }
    
    private void DebugEclipseTimes()
    {
        string level1Debug = "Level 1 eclipse at: " + firstLevelTimes[0] + " seconds";
        
        string level2Debug = "Level 2 eclipses at: ";
        for (int i = 0; i < secondLevelTimes.Length; i++)
        {
            level2Debug += secondLevelTimes[i] + (i < secondLevelTimes.Length - 1 ? ", " : "");
        }
        level2Debug += " seconds";
        
        string level3Debug = "Level 3 eclipses at: ";
        for (int i = 0; i < thirdLevelTimes.Length; i++)
        {
            level3Debug += thirdLevelTimes[i] + (i < thirdLevelTimes.Length - 1 ? ", " : "");
        }
        level3Debug += " seconds";
        
        Debug.Log(level1Debug);
        Debug.Log(level2Debug);
        Debug.Log(level3Debug);
    }
}