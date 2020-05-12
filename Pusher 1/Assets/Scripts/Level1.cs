using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : MonoBehaviour
{
    public AudioClip LevelSong;
    
    void Start()
    {
        SoundManager.Instance.StartSong(LevelSong);
    }

    void SwipeLeft()
    {

    }

    void SwipeRight()
    {

    }


}
