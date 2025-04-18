using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class VideoTransition : MonoBehaviour
{


 public VideoPlayer videoPlayer;


void Start() {
  
    videoPlayer.Prepare();
    videoPlayer.prepareCompleted += PlayVideo;
}

void PlayVideo(VideoPlayer vp) {
    vp.Play();
}
}



