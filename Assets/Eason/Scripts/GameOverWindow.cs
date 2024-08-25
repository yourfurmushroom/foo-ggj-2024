using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverWindow : Window
{
    [Header("Configuration")]
    [SerializeField] private string _mainMenuSceneName;
    [SerializeField] private string _gameSceneName;
    [Header("Settings")]
    [SerializeField] private Sprite[] _equipmentIcons;
    [SerializeField] private float _randomEffectTick;
    [SerializeField] private float _randomEffectDuration;
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI _time;
    [SerializeField] private TextMeshProUGUI _depth;
    [SerializeField] private Image _legacyEquipmentIcon;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _exitButton;



    private IEnumerator _playRandomEquipmentEffect;

    private void Awake()
    {
        _restartButton?.onClick?.AddListener(OnRestartButtonClick);
        _exitButton?.onClick?.AddListener(OnExitButtonClick);
    }


    private void OnRestartButtonClick()
    {
        SceneManager.LoadScene(_gameSceneName);
    }
    private void OnExitButtonClick()
    {
        SceneManager.LoadScene(_mainMenuSceneName);
    }

    public void Initialize(string mainMenuSceneName, string gameSceneName)
    {
        this._mainMenuSceneName = mainMenuSceneName;
        this._gameSceneName = gameSceneName;
    }
    public void SetContent(float time, float depth)
    {
        this._time.text = time.ToString();
        this._depth.text = depth.ToString();

    }
    public void PlayRandomEquipmentEffect(Sprite[] icons)
    {
        this._equipmentIcons = icons;
        _playRandomEquipmentEffect = RandomEquipmentEffectCorroutine();
        this.StartCoroutine(_playRandomEquipmentEffect);
    }
    private IEnumerator RandomEquipmentEffectCorroutine()
    {
        var count = _equipmentIcons.Length;
        if (count == 0)
        {
            _legacyEquipmentIcon.sprite = null;
            yield break;
        }
        var lap = this._randomEffectTick * count;
        var elapsedTime = 0f;
        while(elapsedTime < this._randomEffectDuration)
        {
            yield return null;
            elapsedTime += Time.deltaTime;
            var number = (int)((elapsedTime % lap) / this._randomEffectTick);
            _legacyEquipmentIcon.sprite = this._equipmentIcons[number];
        }
        _legacyEquipmentIcon.sprite = _equipmentIcons[0];
    }
}
