using CompanyName.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum GameState
{
    GameMenu,
    GamePlay,
    GameOver,
}
[Serializable]
public class GameContext
{
    [SerializeField] private GameState _state;
    [SerializeField] private float _depth;
    [SerializeField] private float _hp;
    [SerializeField] private float _time;
    [SerializeField] private int _legacyEquipment;
    [SerializeField] private float _dropSpeed;
    [SerializeField] private EquipmentContext _equipmentContext;
    public GameState state { get => _state; set => _state = value; }
    public float depth { get => _depth; set => _depth = value; }
    public float hp { get => _hp; set => _hp = value; }
    public float time { get => _time; set => _time = value; }
    public float dropSpeed { get => _dropSpeed; set => _dropSpeed = value; }
    public EquipmentContext equipmentContext { get => _equipmentContext; set => _equipmentContext = value; }

    public SpeedAttribute speedAttribute = new SpeedAttribute();
}
public class GameManager : MonoBehaviour
{
    [SerializeField] private ApplicationConfiguration _applicationConfiguration;
    [SerializeField] private GameContext _context;

    [SerializeField] private GameMenuWindow _gameMenuWindow;
    [SerializeField] private GameOverWindow _gameOverWindow;
    [SerializeField] private GamePlayWindow _gamePlayWindow;

    public List<ItemController> itemControllers;
    public BackgroundController backgroundController;
    public movement playerMovement;

    private void Awake()
    {
        _gameMenuWindow?.closed?.AddListener(OnMenuWindowClosed);
        _gameMenuWindow.Initialize(_applicationConfiguration.scenes.mainMenu, _applicationConfiguration.scenes.game);
        _gamePlayWindow?.SetBuff("無Buff");
        _context.speedAttribute.speed = _context.dropSpeed;
        foreach (var itemController in itemControllers)
        {
            itemController.SetSpeedAttribute(_context.speedAttribute);
            itemController.SetGameContext(_context);
            itemController.StartAutoGenerate();
            itemController.triggerAlphabetTagEnter += (tag) =>
            {
                Debug.Log(tag);
                int index = 0;
                switch (tag)
                {
                    case "L":
                        index = 0;
                        break;
                    case "E":
                        index = 1; break;
                    case "G":
                        index = 2; break;
                    case "A":
                        index = 3; break;
                    case "C":
                        index = 4; break;
                    case "Y":
                        index = 5; break;
                }
                _gamePlayWindow?.ActivateLetter(index, true);
            };
            itemController.triggerEquipmentGet += () =>
            {
                // _context.legacyEquipment++;
                // _gamePlayWindow?.AddEquipment(_context.equipmentContext.equipments[0].icon);
            };
            itemController.cleanAlphabetTag += () =>
            {
                _gamePlayWindow?.CleanAllLetter();
            };
            itemController.updateBuff += (buff) =>
            {
                _gamePlayWindow?.SetBuff(buff);
            };
        }
        backgroundController.speedAttribute = _context.speedAttribute;


    }

    private void OnMenuWindowClosed()
    {
        _context.state = GameState.GamePlay;
    }

    private void Reset()
    {
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        switch (_context.state)
        {
            case GameState.GameMenu:
                break;
            case GameState.GamePlay:
                _context.time -= Time.deltaTime;
                // _context.hp -= Time.deltaTime;
                _context.depth += _context.speedAttribute.speed * Time.deltaTime;
                if (_context.time <= 0)
                {
                    _context.time = 0;
                    OnHpZero();
                }
                _gamePlayWindow?.SetStatus(_context.time, _context.depth, "無Buff");
                break;
            case GameState.GameOver:
                break;
            default:
                break;
        }
    }
    void Update()
    {
        HandleInput();

    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(_applicationConfiguration.keymap.cancel) && _context.state == GameState.GameMenu)
        {
            _gameMenuWindow?.Close();
        }
        else if (Input.GetKeyDown(_applicationConfiguration.keymap.cancel) && _context.state == GameState.GamePlay)
        {
            {
                _context.state = GameState.GameMenu;
                _gameMenuWindow?.Open();
            }
        }
    }
    private void OnHpZero()
    {
        _context.state = GameState.GameOver;
        _context.speedAttribute.ToZero();
        playerMovement.activeFlag = false;

        StartCoroutine(GameOverCoroutine());
    }

    IEnumerator GameOverCoroutine()
    {
        yield return new WaitForSeconds(2);
        var equipments = _context.equipmentContext.equipments;
        var newGainEquipments = _context.equipmentContext.GetNewGainEquipments();
        var icons = newGainEquipments.Select(i => equipments[i].icon).ToArray();

        _gameOverWindow?.Initialize(_applicationConfiguration.scenes.mainMenu, _applicationConfiguration.scenes.game);
        _gameOverWindow?.Open();
        _gameOverWindow?.SetContent(_context.time, _context.depth);
        _gameOverWindow?.PlayRandomEquipmentEffect(icons);
    }
}
