using CompanyName.Domain;
using System;
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
}
public class GameManager : MonoBehaviour
{
    [SerializeField] private ApplicationConfiguration _applicationConfiguration;
    [SerializeField] private GameContext _context;
    
    [SerializeField] private GameMenuWindow _gameMenuWindow;
    [SerializeField] private GameOverWindow _gameOverWindow;
    [SerializeField] private GamePlayWindow _gamePlayWindow;

    private void Awake()
    {
        _gameMenuWindow?.closed?.AddListener(OnMenuWindowClosed);
        _gameMenuWindow.Initialize(_applicationConfiguration.scenes.mainMenu, _applicationConfiguration.scenes.game);
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
                _context.time += Time.deltaTime;
                _context.hp -= Time.deltaTime;
                _context.depth += _context.dropSpeed * Time.deltaTime;
                if(_context.hp <= 0)
                {
                    OnHpZero();
                }
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
        var equipments = _context.equipmentContext.equipments;
        var newGainEquipments = _context.equipmentContext.GetNewGainEquipments();
        var icons = newGainEquipments.Select(i => equipments[i].icon).ToArray();

        _gameOverWindow?.Initialize(_applicationConfiguration.scenes.mainMenu, _applicationConfiguration.scenes.game);
        _gameOverWindow?.Open();
        _gameOverWindow?.SetContent(_context.time, _context.depth);
        _gameOverWindow?.PlayRandomEquipmentEffect(icons);
    }
}
