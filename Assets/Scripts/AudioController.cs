// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.SceneManagement;

// public class AudioController : MonoBehaviour
// {



//   // public AudioSource playerSource;
//   //   public AudioClip[] audioClipsPlayer;
  
    
//   // public AudioSource rivalSource;
//   //   public AudioClip audioClipRival;

//     [SerializeField] AudioSource SFXSource;
//   //  public AudioClip button;
//      public AudioClip screech;
//      public AudioClip scroll;
//      public AudioClip omen;
//      public AudioClip feather;

//    [SerializeField] AudioSource musicSource;
//     private AudioClip currentClip;
//     public AudioClip theme;
//     // public AudioClip metal;
//     // public AudioClip home;


//     public float masterSoundVolume =1f;
      

//   // private float metalTime = 0f; //store position of metal tune
//   // private float themeTime = 0f; // store position of theme tune
 
//   private bool isMusicPaused = false; // Track pause state


// public static AudioController instance;

// private void Awake(){

//   if(instance == null){

//     instance = this;
//     DontDestroyOnLoad(gameObject);

//   } else {
//     Destroy(gameObject);
//   }
 
//   SceneManager.sceneLoaded += OnSceneLoaded;
// }
    

//  void OnSceneLoaded(Scene scene, LoadSceneMode mode)
//     {
//         Debug.Log("Scene Loaded: " + scene.name);

//         if (scene.name == "GameScene")
//         {
//             musicSource.clip = theme;
//             musicSource.Play();
//             Debug.Log("Playing theme");
//         } else if (scene.name == "IntroScene"){
//           musicSource.clip = theme;
//           musicSource.Play();
//         }
//     }








// //  private void OnEnable()
// //     {
// //         SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to scene loaded event
// //     }

// //     private void OnDisable()
// //     {
// //         SceneManager.sceneLoaded -= OnSceneLoaded; // Unsubscribe to prevent memory leaks
// //     }

// //     private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
// //     {
// //       if(scene.name == "HomePage"){
// //           StartHomeTune();
// //       }
      
// //       if(scene.name == "Win01"){
// //           WinSFX();
// //       }
// //       if(scene.name == "Lose_timeout"){
// //           TimesUpSFX();
// //       }
// //       if(scene.name == "Lose_caught"){
// //         CaughtSFX();
// //       }
           

// //  }





// public void PauseMusic()
// {
//     if (isMusicPaused)
//     {
//         musicSource.mute = false; // Unmute audio
//         musicSource.Play();       // Resume playback
//         isMusicPaused = false;
//         Debug.Log("Music Resumed");
//     }
//     else
//     {
//         musicSource.Pause();      // Pause playback
//         musicSource.mute = true;  // Mute audio to avoid weird silent bugs
//         isMusicPaused = true;
//         Debug.Log("Music Paused");
//     }
// }


//     // public void PlayPlayerSFX()
//     // {

//     //     // to ensure no overlapping of the audio
//     //     if (!playerSource.isPlaying){
//     //    // SFXSource.PlayOneShot(clip);

//     //     playerSource.clip = audioClipsPlayer[Random.Range(0, audioClipsPlayer.Length)];
//     //     playerSource.Play();

//     //     }
//     // }


//   //   public void PlayRivalSFX(){

//   //     if (!rivalSource.isPlaying){
//   //           rivalSource.clip = audioClipRival;
//   //           rivalSource.Play();
//   //     }
        
//   //   }

//   //   public void StopRivalSFX(){
//   //           rivalSource.clip = audioClipRival;
//   //           rivalSource.Stop();
        
//   //   }



//   public void BirdSFX(){
//     SFXSource.clip = screech;
//     SFXSource.Play();
//   }


//   public void ScrollSFX(){
//     SFXSource.clip = scroll;
//     SFXSource.Play();
//   }

//   public void OmenSFX(){
    
//     // if (!SFXSource.isPlaying){
//       SFXSource.clip = omen;
//       SFXSource.Play();
//     //}

//   }

//   // public void ButtonSFX(){
//   //   SFXSource.clip = button;
//   //   SFXSource.Play();
//   // }


//   // public void WinSFX(){
//   //   SFXSource.clip = win;
//   //   SFXSource.Play();
//   // }

//   // public void TimesUpSFX(){
//   //   SFXSource.clip = timesUp;
//   //   SFXSource.Play();
//   // }


//   // public void CaughtSFX(){
//   //   SFXSource.clip = caught;
//   //   SFXSource.Play();
//   // }



//   //   public void MetalTune()
//   // {

//   //   themeTime = musicSource.time; // Save theme position
//   //   currentClip = metal;
//   //   musicSource.clip = metal;
//   //   musicSource.time = metalTime; // Resume from saved position
//   //   musicSource.Play();

//   // }

//   // public void PauseMetalTune()
//   // {
//   //   if (musicSource.isPlaying && currentClip == metal)
//   //   {
//   //     metalTime = musicSource.time; // Save metal position
//   //     musicSource.Pause();
//   //   }
//   // }

//   // public void StartTheme()
//   // {

//   //   metalTime = musicSource.time; // Save metal position
//   //   currentClip = theme;
//   //   musicSource.clip = theme;
//   //   musicSource.time = themeTime; // Resume from saved position
//   //   musicSource.Play();
//   // }

//   // public void PauseTheme()
//   // {
//   //   if (musicSource.isPlaying && currentClip == theme)
//   //   {
//   //     themeTime = musicSource.time; // Save theme position
//   //     musicSource.Pause();
//   //   }
//   // }



//   // public void StopAudio()
//   // {
//   //   musicSource.mute = true;
//   //   rivalSource.mute = true;
//   //   playerSource.mute = true;
//   //   SFXSource.mute = true;
//   // }


// //   public void StartHomeTune(){
// //     currentClip = home;
// //     musicSource.clip = home;
// //     musicSource.Play();
// //   }






//  }



// // public void StartTheme(){

//     //   //  if (!musicSource.isPlaying){

//     //     musicSource.clip = theme;
//     //    // musicSource.volume = 0.1f;
//     //     musicSource.Play();

//     //   //'  }

  
//     // }

//     // public void PauseTheme(){
//     //     if(musicSource.clip = theme){
//     //    // musicSource.clip = theme;
//     //     musicSource.Pause();
//     //     }
//     // }


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;

    [SerializeField] private AudioSource SFXSource;
    public AudioClip screech;
    public AudioClip scroll;
    public AudioClip omen;
    public AudioClip feather;

    [SerializeField] private AudioSource musicSource;
    [Header("Scene Music Clips")]
    public AudioClip introMusic;
    public AudioClip gameMusic;
    public AudioClip winMusic;
    public AudioClip loseMusic;



    private bool isMusicPaused = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene Loaded: " + scene.name);
        if (scene.name == "IntroScene")
        {
            StartCoroutine(SwitchMusic(introMusic, 1f));
        }
        else if (scene.name == "TitleScene")
        {
            Cursor.visible = true;
            // If intro music is already playing, don’t restart it
            if (musicSource.clip != introMusic || !musicSource.isPlaying)
            {
                StartCoroutine(SwitchMusic(introMusic, 1f));
            }
        }
        else if (scene.name == "GameScene")
        {
            StartCoroutine(SwitchMusic(gameMusic, 1f));
        }
        else if (scene.name == "LoseScene")
        {
            Cursor.visible = true;
            StartCoroutine(SwitchMusic(loseMusic, 1f));
        }
        else if (scene.name == "WinScene")
        {
            Cursor.visible = true;
            StartCoroutine(SwitchMusic(winMusic, 1f));
        }

        else
        {
            StartCoroutine(FadeOutMusic(1f));
        }
    }

        private IEnumerator SwitchMusic(AudioClip newClip, float fadeDuration)
    {
        // Only fade out if something is already playing
        if (musicSource.isPlaying)
            yield return StartCoroutine(FadeOutMusic(fadeDuration));

        // Switch to the new clip
        musicSource.clip = newClip;
        musicSource.loop = true;
        musicSource.volume = 0f;
        musicSource.Play();

        // Fade in new track
        yield return StartCoroutine(FadeInMusic(fadeDuration));
    }

    private IEnumerator FadeOutMusic(float duration)
    {
        float startVol = musicSource.volume;
        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(startVol, 0f, timer / duration);
            yield return null;
        }
        musicSource.volume = 0f;
        musicSource.Stop();
    }

    private IEnumerator FadeInMusic(float duration)
    {
        float targetVol = 1f;
        musicSource.volume = 0f;
        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(0f, targetVol, timer / duration);
            yield return null;
        }
        musicSource.volume = targetVol;
    }

    public void PauseMusic()
    {
        if (isMusicPaused)
        {
            musicSource.mute = false;
            musicSource.Play();
            isMusicPaused = false;
            Debug.Log("Music Resumed");
        }
        else
        {
            musicSource.Pause();
            musicSource.mute = true;
            isMusicPaused = true;
            Debug.Log("Music Paused");
        }
    }

    public void BirdSFX()
    {
        SFXSource.clip = screech;
        SFXSource.Play();
    }

    public void ScrollSFX()
    {
        SFXSource.clip = scroll;
        SFXSource.Play();
    }

    public void OmenSFX()
    {
        SFXSource.clip = omen;
        SFXSource.Play();
    }

    public void FeatherSFX()
    {
        SFXSource.clip = feather;
        SFXSource.Play();
    }
}
