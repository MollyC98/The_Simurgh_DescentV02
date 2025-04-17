using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{

    // Singleton instance
    public static LevelController Instance { get; private set; }
    
    // Your global level data
    public int currentLevel = 1;
    public string currentLevelName;
    public float levelProgress = 0f;
    // Add any other level-related data you need
    
    private void Awake()
    {
        // Singleton pattern implementation
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
            // Initialize level data
            currentLevelName = SceneManager.GetActiveScene().name;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    // Method to update the level
    public void SetLevel(int level)
    {
        currentLevel = level;
        // You can add other logic here, like saving to PlayerPrefs
    }
    
    // Called when a new scene is loaded
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentLevelName = scene.name;
        // You could parse the level number from the scene name if needed
        // Or perform other initialization for the new level
    }
    
    private void OnEnable()
    {
        // Subscribe to scene change events
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    
    private void OnDisable()
    {
        // Unsubscribe to prevent memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}