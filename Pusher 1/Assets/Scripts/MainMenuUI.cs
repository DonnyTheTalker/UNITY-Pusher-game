using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    public string MainLevelScene;
    [SerializeField] private SceneFader _sceneFader;
    [SerializeField] private AudioClip _mainMenuSong;

    public void Push()
    {
        SoundManager.Instance.StopSong();
        _sceneFader.FadeTo(MainLevelScene);
    }

    public void Start()
    {
        Invoke("ChangeMainSong", 0.3f);
    }

    public void ChangeMainSong()
    {
        SoundManager.Instance.StartSong(_mainMenuSong);
    }

}
