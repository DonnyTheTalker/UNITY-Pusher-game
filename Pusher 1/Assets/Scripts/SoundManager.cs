using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{ 
    public AudioSource MusicSource; 

    public static SoundManager Instance = null; 

    private void Awake()
    {
        if (Instance == null) {
            Instance = this; 
        } else if (Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void StartSong(AudioClip song)
    {
        MusicSource.clip = song;
        MusicSource.Play();
    }

    public void StopSong()
    {
        StartCoroutine(StopSongCO());
    }

    private IEnumerator StopSongCO()
    {
        float volumeOffset = MusicSource.volume / 10f;

        for (int i = 0; i < 10; i++) {
            MusicSource.volume -= volumeOffset;
            yield return new WaitForSeconds(0.03f);
        }

        MusicSource.Stop();  
    }

}