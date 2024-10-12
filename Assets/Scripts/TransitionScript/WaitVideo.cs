using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class WaitVideo : MonoBehaviour
{

    public GameObject transition;
    public VideoPlayer videoPlayer;

    void Start()
    {
        Cursor.visible = false;
        videoPlayer.loopPointReached += OnVideoEnd;
    }

   
    void OnVideoEnd(VideoPlayer vp)
    {
        
        transition.GetComponent<SceneController>().ChangeScene();
    }

}
