using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Puzzle2 : MonoBehaviour
{
    public AudioClip sound;
    public AudioClip reset;
    public List<Sprite> circles;
    Sprite currentImage;
    int id = 0;
    void Start()
    {
        id = 0;
        gameObject.GetComponent<Image>().sprite = circles[id];
    }

    public void NextImage()
    {
        id ++;
        if (id > circles.Count-1)
        {
            id = 0;
        }
        SoundManager.instance.playSound(sound, 0.5f);
        currentImage = circles[id];
        gameObject.GetComponent<Image>().sprite = currentImage;
    }

    public void PreviousImage()
    {
        id--;
        if (id < 0)
        {
            id = circles.Count - 1;
        }
        SoundManager.instance.playSound(sound, 0.5f);
        currentImage = circles[id];
        gameObject.GetComponent<Image>().sprite = currentImage;
    }

    public void Reset()
    {
        SoundManager.instance.playSound(reset, 0.3f);
        Start();
    }
}
