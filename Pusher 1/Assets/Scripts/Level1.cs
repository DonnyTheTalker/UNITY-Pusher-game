using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level1 : MonoBehaviour
{
    public AudioClip LevelSong;
    public Text MoneyText;
    public Stat PlayerMoney;

    public GameObject TextPanel;
    private Image _panelImage;
    public Text TextInPanel;

    public GameObject Subject;
    private SpriteRenderer _subjectSprite;

    public Sprite[] SubjectSprites;
    public bool[] SubjectViolent;
    public bool[] SubjectDiplomacy;

    private int _currectSubject;
    private bool _canSwipe;
    private float _flipCountdown = 1f;

    void Start()
    {
        _subjectSprite = Subject.GetComponent<SpriteRenderer>();
        _panelImage = TextPanel.GetComponent<Image>();

        Color temp = _panelImage.color;
        temp.a = 0f;
        _panelImage.color = temp;

        temp = TextInPanel.color;
        temp.a = 0f;
        TextInPanel.color = temp;

        ChangeMoneyText();
        //SoundManager.Instance.StartSong(LevelSong);
        Setup();
    }

    void Update()
    {
        _flipCountdown -= Time.deltaTime;

        if (_flipCountdown <= 0f) {
            _flipCountdown = 1f;
            if (_canSwipe) {

                Vector3 temp = Subject.transform.localScale;
                temp.x *= -1;
                Subject.transform.localScale = temp;

            }
        }

    }

    void Setup()
    {
        _currectSubject = 0;
        _canSwipe = false;
        ShuffleSubjects();
        StartCoroutine(ChangeGameState());
    }

    IEnumerator ChangeGameState()
    {
        StartCoroutine(FadePanelIn());
        yield return new WaitForSeconds(2f);

        _subjectSprite.sprite = SubjectSprites[0];
        yield return new WaitForSeconds(1f);

        StartCoroutine(FadePanelOut());
        yield return new WaitForSeconds(2f);
        _canSwipe = true;
        _flipCountdown = 1f;
    }

    public void Violence()
    {
        Debug.Log("Left");
    }

    public void Diplomacy()
    {
        Debug.Log("Right");
    }

    private void ChangeMoneyText()
    {
        MoneyText.text = PlayerMoney.Value.ToString();
    }

    private IEnumerator FadePanelIn(bool extraSpeed = false)
    { 
        while (_panelImage.color.a <= 1f) {
             
            Color temp = _panelImage.color;
            temp.a += (Time.deltaTime * 0.5f);
            if (extraSpeed)
                temp.a += (Time.deltaTime * 0.5f);
            _panelImage.color = temp;

            temp = TextInPanel.color;
            temp.a += (Time.deltaTime * 0.5f);
            if (extraSpeed)
                temp.a += (Time.deltaTime * 0.5f);
            TextInPanel.color = temp;
            
            yield return null;
        }
    }

    private IEnumerator FadePanelOut(bool extraSpeed = false)
    {
        while(_panelImage.color.a >= 0.01f) {

            Color temp = _panelImage.color;
            temp.a -= (Time.deltaTime * 0.5f);
            if (extraSpeed)
                temp.a -= (Time.deltaTime * 0.5f);
            _panelImage.color = temp;

            temp = TextInPanel.color;
            temp.a -= (Time.deltaTime * 0.5f);
            if (extraSpeed)
                temp.a -= (Time.deltaTime * 0.5f);
            TextInPanel.color = temp;

            yield return null;
        }
    }

    private void ShuffleSubjects()
    {
        for (int i = 0; i < SubjectSprites.Length; i++) {

            var tempSprite = SubjectSprites[i];
 //           var tempV = SubjectViolent[i];
   //         var tempD = SubjectDiplomacy[i];

            int pos = Random.Range(0, i + 1);

     //       SubjectDiplomacy[i] = SubjectDiplomacy[pos];
       //     SubjectViolent[i] = SubjectViolent[pos];
            SubjectSprites[i] = SubjectSprites[pos];
         //   SubjectDiplomacy[pos] = tempD;
           // SubjectViolent[pos] = tempV;
            SubjectSprites[pos] = tempSprite;

        }
    }

}
