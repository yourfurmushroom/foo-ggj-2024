using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class GamePlayWindow : Window
{
    [Header("Settings")]
    [SerializeField] private Sprite[] _icons; 
    [SerializeField] private Color _letterActiveColor;
    [SerializeField] private Color _letterDeactiveColor;
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private TextMeshProUGUI _depthText;
    [SerializeField] private TextMeshProUGUI _buffText;

    [SerializeField] private Image _equipmentIconPrefab;
    [SerializeField] private Transform _equimentIconParent;

    [SerializeField] private Image[] _letterIcon;
    [SerializeField] private GameObject[] _letterGameObjects;


    private void Initialized()
    {
    }

    void Update()
    {
    }

    public void SetStatus(float time, float depth, string buff)
    {
        //顯示小數點後兩位
        _timeText.text = time.ToString("F2") + "s";
        _depthText.text = depth.ToString("F2") + "m";
        _buffText.text = buff;
    }

    public void ActivateLetter(int index, bool active)
    {
        _letterGameObjects[index].SetActive(active);
        // _letterIcon[index].color = active ? _letterActiveColor : _letterDeactiveColor;
    }

    //關掉所有字母
    public void CleanAllLetter()
    {
        foreach (var letter in _letterGameObjects)
        {
            letter.SetActive(false);
        }
    }

    public void AddEquipment(int index)
    {
        var instance = Instantiate(_equipmentIconPrefab, _equimentIconParent);
        instance.sprite = _icons[index];
    }
}