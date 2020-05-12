using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class LevelDeparture : MonoBehaviour
{
    public GameObject IntroVideo;
    public AudioClip BackgroundSong;

    public Text NarrativeText;
    private CanvasGroup _textCanvasGroup;

    public string[] NarrativeMessages;
    public float[] NarrativeMessagesDelay;
    public AudioClip[] NarrativeMessagesAudio;

    [SerializeField] private SceneFader _sceneFader;

    public string LevelHubScene;

    private void Start()
    {
        _textCanvasGroup = NarrativeText.GetComponent<CanvasGroup>();
        _textCanvasGroup.alpha = 0f; 
        Invoke("StartNarrative", 5.05f);
        //Invoke("StartNarrative", 75f);
    }

    private void StartNarrative()
    {  
        SoundManager.Instance.StartSong(BackgroundSong);
        StartCoroutine(DoNarrative());
    }

    private IEnumerator DoNarrative()
    {
        yield return new WaitForSeconds(0.1f); 
        IntroVideo.SetActive(false);
        for (int i = 0; i < NarrativeMessages.Length; i++) {
            NarrativeText.text = NarrativeMessages[i];
    //        SoundManager.Instance.StartEffect(NarrativeMessagesAudio[i]);
            StartCoroutine(FadeTextIn());
            yield return new WaitForSeconds(NarrativeMessagesDelay[i]);
            StartCoroutine(FadeTextOut());
            yield return new WaitForSeconds(0.3f);
        }

        _sceneFader.FadeTo(LevelHubScene);
    }

    private IEnumerator FadeTextIn()
    {
        for (int i = 0; i < 10; i++) {
            _textCanvasGroup.alpha += 0.1f;
            yield return new WaitForSeconds(0.03f);
        }
    }

    private IEnumerator FadeTextOut()
    {
        for (int i = 0; i < 10; i++) {
            _textCanvasGroup.alpha -= 0.1f;
            yield return new WaitForSeconds(0.03f);
        }
    }

}
