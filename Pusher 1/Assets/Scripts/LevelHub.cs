using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelHub : MonoBehaviour
{
    [SerializeField] private Stat _currectLevel;
    [SerializeField] private string[] _scenes;
    [SerializeField] private string[] _levelNames; 
    [SerializeField] private SceneFader _sceneFader;

    public Text CurrectLevelText;

    private void Start()
    {
        CurrectLevelText.text = _levelNames[_currectLevel.Value];
        if (_currectLevel.Value == 0) {
            //ShuffleLevels();
        }
        StartCoroutine(LoadNewLevel());
    }

    private void ShuffleLevels()
    {
        for (int i = 0; i < _scenes.Length - 1; i++) {
            string tempScene = _scenes[i]; 
            int pos = Random.Range(0, i + 1);
            _scenes[i] = _scenes[pos];
            _scenes[pos] = tempScene; 
        }    
    }

    private IEnumerator LoadNewLevel()
    { 
        yield return new WaitForSeconds(2f);
        SoundManager.Instance.StopSong(); 
        _currectLevel.Value++;
        _sceneFader.FadeTo(_scenes[_currectLevel.Value - 1]);
    }




}
