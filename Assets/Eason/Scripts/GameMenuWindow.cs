using CompanyName.Domain;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace CompanyName.Application
{
}
public class GameMenuWindow : Window
{
    [SerializeField] private string _mainMenuSceneName;
    [SerializeField] private string _gameSceneName;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _exitButton;

    private void Awake()
    {
        _restartButton?.onClick?.AddListener(OnRestartButtonClick);
        _continueButton?.onClick?.AddListener(OnContinueButtonClick);
        _exitButton?.onClick?.AddListener(OnExitButtonClick);
    }

    private void OnRestartButtonClick()
    {
        SceneManager.LoadScene(_gameSceneName);
    }

    private void OnContinueButtonClick()
    {
        base.Close();
    }
    private void OnExitButtonClick()
    {
        SceneManager.LoadScene(_mainMenuSceneName);
    }
    public void Initialize(string mainMenuSceneName, string gameSceneName)
    {
        _mainMenuSceneName = mainMenuSceneName;
        _gameSceneName = gameSceneName;
    }

}
