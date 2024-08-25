using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class GamePlayWindow : Window
{
    [Header("Settings")]
    [SerializeField] private Color _letterActiveColor;
    [SerializeField] private Color _letterDeactiveColor;
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI _hpText;
    [SerializeField] private TextMeshProUGUI _depthText;
    [SerializeField] private TextMeshProUGUI _buffText;

    [SerializeField] private Image _equipmentIconPrefab;
    [SerializeField] private Transform _equimentIconParent;

    [SerializeField] private Image[] _letterIcon;

    private void Initialized()
    {
    }

    public void SetStatus(float hp, float depth, string buff)
    {
        _hpText.text = hp.ToString();
        _depthText.text = depth.ToString() + "m";
        _buffText.text = buff;
    }

    public void ActivateLetter(int index, bool active)
    {
        _letterIcon[index].color = active ? _letterActiveColor : _letterDeactiveColor;
    }

    public void AddEquipment(Sprite icon)
    {
        var instance = Instantiate(_equipmentIconPrefab, _equimentIconParent);
        instance.sprite = icon;
    }
}