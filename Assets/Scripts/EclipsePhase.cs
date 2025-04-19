using System.Collections; 
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class EclipsePhase : MonoBehaviour
{
    public Light2D spotLight;
    public Light2D globalLight;

    float targetIntensity;
    private Color targetColor;
    public bool isEclipseActive = false;

    public ObjectController objectController;
   

    //float globalIntensity = 1.5f; // very beautiful sunset color

    private List<float> eclipseTimes = new List<float>();


    public void Initialize()
        {
            int level = LevelController.Instance.currentLevel;

            switch (level)
            {
                case 1:
                    ScheduleEclipses(1, 10f, 30f);
                    Debug.Log("Start Eclipse 1");
                    break;
                case 2:
                    ScheduleEclipses(2, 40f, 80f);
                    Debug.Log("Start Eclipse 2");
                    break;
                case 3:
                    ScheduleEclipses(3, 90f, 130f);
                    Debug.Log("Start Eclipse 3");
                    break;
            }

            StartCoroutine(EclipseScheduler());
        }

    void Update()
    {
        if (isEclipseActive)
        {
            if (globalLight != null)
                globalLight.intensity = Mathf.MoveTowards(globalLight.intensity, targetIntensity, Time.deltaTime);

            if (spotLight != null)
                spotLight.color = Color.Lerp(spotLight.color, targetColor, Time.deltaTime);
        }
    }

    public void SetLightColor(string hexCode)
    {
        if (ColorUtility.TryParseHtmlString(hexCode, out Color newColor))
            targetColor = newColor;
    }

    public void StartEclipse()
    {
        isEclipseActive = true;
        SetLightColor("#FF0000");
        targetIntensity = 0.1f;

        if (objectController.isRunning){
            objectController.TriggerCoroutine();
            Debug.Log("trigger coroutine");
        }

        StartCoroutine(EndEclipseAfterSeconds(15f)); 
    }

    private IEnumerator EndEclipseAfterSeconds(float duration)
    {
        yield return new WaitForSeconds(duration);
        SetLightColor("#FFFFFF");
        targetIntensity = 1.0f;

        yield return new WaitForSeconds(1f);
        isEclipseActive = false;

        if (objectController.isRunning){
            objectController.TriggerCoroutine();
            Debug.Log("trigger coroutine");
        }
        
    }

    // void ScheduleEclipses(int eclipseCount, float startRange, float endRange)
    // {
    //     eclipseTimes.Clear();
    //     float segmentSize = (endRange - startRange) / eclipseCount;

    //     for (int i = 0; i < eclipseCount; i++)
    //     {
    //         float minTime = startRange + i * segmentSize; 
    //         float maxTime = minTime + segmentSize;
    //         float randomTime = Random.Range(minTime, maxTime);
    //         eclipseTimes.Add(randomTime);
    //         Debug.Log("random time:"+randomTime);
    //     }
    //     // eclipseCount = 1, start=10, end=20, segment = 10
    //             //i=0, min= 10 max=20
            

    //     // eclipseCount = 2, start=30, end=40, segment = 5
    //             //i=0, min=30 max= 35
    //             //i=1, min=35 max= 40
    // }

    void ScheduleEclipses(int eclipseCount, float startRange, float endRange, float minSpacing = 15f)
{
    eclipseTimes.Clear();

    float totalWindow = endRange - startRange;
    float requiredSpace = minSpacing * (eclipseCount - 1);

    // Make sure there’s enough room for the spacing
    if (totalWindow <= requiredSpace)
    {
        Debug.LogError($"Not enough time ({totalWindow}s) to fit {eclipseCount} eclipses with {minSpacing}s spacing.");
        return;
    }

    // The “free” time we can distribute randomly
    float availableTime = totalWindow - requiredSpace;
    float segmentSize = availableTime / eclipseCount;

    float cursor = startRange;
    for (int i = 0; i < eclipseCount; i++)
    {
        // Pick a random moment in [cursor, cursor + segmentSize]
        float pick = Random.Range(cursor, cursor + segmentSize);
        eclipseTimes.Add(pick);
        Debug.Log($"Scheduled eclipse {i + 1} at {pick:F1}s");

        
        cursor = pick + minSpacing;
    }
}

    IEnumerator EclipseScheduler()
    {
        float timer = 0f;

        while (eclipseTimes.Count > 0)
        {
            timer += Time.deltaTime;

            for (int i = eclipseTimes.Count - 1; i >= 0; i--)
            {
                if (timer >= eclipseTimes[i])
                {
                    StartEclipse();
                    Debug.Log("eclipse has started");
                    eclipseTimes.RemoveAt(i);
                }
            }

            yield return null;
        }
    }
}