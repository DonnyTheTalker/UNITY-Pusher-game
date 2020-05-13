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
    private float _moneyChangeCountdown = 2f;

    public string[] Congratulations;
    public string[] Regrets;

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

        ChangeMoneyText("");

        SoundManager soundManager = SoundManager.Instance;

        if (soundManager != null)
            soundManager.StartSong(LevelSong);
        Setup();
    }

    void Update()
    {
        _flipCountdown -= Time.deltaTime;
        _moneyChangeCountdown -= Time.deltaTime;

        if (_moneyChangeCountdown <= 0f) {
            _moneyChangeCountdown = 2f;
            ChangeMoneyText();
        }

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

    IEnumerator ChangeGameState(bool extraSpeed = false)
    {
        StartCoroutine(FadePanelIn(extraSpeed));

        if (extraSpeed)
            yield return new WaitForSeconds(1f);
        else
            yield return new WaitForSeconds(2f);

        _subjectSprite.sprite = SubjectSprites[_currectSubject];
        yield return new WaitForSeconds(1f);


        StartCoroutine(FadePanelOut(extraSpeed));
        if (extraSpeed)
            yield return new WaitForSeconds(1f);
        else
            yield return new WaitForSeconds(2f);

        _canSwipe = true;
        _flipCountdown = 1f;
    }

    public void Violence()
    {
        if (!_canSwipe) return;
        _canSwipe = false;

        if (SubjectViolent[_currectSubject]) {
            NextStage(true);
        } else {
            NextStage(false);
        }

        Debug.Log("Left");
    }

    public void Diplomacy()
    {
        if (!_canSwipe) return;
        _canSwipe = false;
        
        if (SubjectDiplomacy[_currectSubject]) {
            NextStage(true);
        } else {
            NextStage(false);
        }

        Debug.Log("Right");
    }

    private void NextStage(bool moneyEarned)
    {

        if (moneyEarned) {

            PlayerMoney.Value += 2000;
            ChangeMoneyText("  + 2000");
            TextInPanel.text = Congratulations[Random.Range(0, Congratulations.Length)];
            _moneyChangeCountdown = 2f;
        } else {
            ChangeMoneyText();
            TextInPanel.text = Regrets[Random.Range(0, Congratulations.Length)];
        }

        if (_currectSubject == SubjectSprites.Length - 1) {

        } else {
            _currectSubject++;
            StartCoroutine(ChangeGameState(true));
        }

    }

    private void ChangeMoneyText(string addText = "")
    {
        MoneyText.text = PlayerMoney.Value.ToString() + addText;
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
            var tempV = SubjectViolent[i];
            var tempD = SubjectDiplomacy[i];

            int pos = Random.Range(0, i + 1);

            SubjectDiplomacy[i] = SubjectDiplomacy[pos];
            SubjectViolent[i] = SubjectViolent[pos];
            SubjectSprites[i] = SubjectSprites[pos];
            SubjectDiplomacy[pos] = tempD;
            SubjectViolent[pos] = tempV;
            SubjectSprites[pos] = tempSprite;

        }
    }

}
