using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class WaitFinalCinematic : MonoBehaviour
{

    public GameObject transition;
    public VideoPlayer videoPlayer;
    public VideoClip clip1;
    public VideoClip clip2;
    public ConversationPersistanceManager conversation;

    void Start()
    {
        Cursor.visible = false;
        if (conversation.finished)
        {
            
            videoPlayer.clip = clip1;
        }
        else
        {
            
            videoPlayer.clip = clip2;
        }
        videoPlayer.loopPointReached += OnCinematicEnd;
    }

   
    void OnCinematicEnd(VideoPlayer vp)
    {
       
        transition.GetComponent<SceneController>().ChangeScene();
    }

}
