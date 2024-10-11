using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSceneManager : MonoBehaviour
{
    public PlayerData playerData;
    public int sceneId;
    void Start()
    {
        playerData.idScene = sceneId;
    }
}
