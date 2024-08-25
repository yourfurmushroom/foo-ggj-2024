using CompanyName.Domain;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuWindow : MonoBehaviour
{
    [SerializeField] private ApplicationConfiguration _applicationConfiguration;
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _exitButton;



    private void Awake()
    {
        _startButton?.onClick?.AddListener(OnStartButtonClick);
        _exitButton?.onClick?.AddListener(OnExitButtonClick);
    }

    private void OnStartButtonClick()
    {
        SceneManager.LoadScene(_applicationConfiguration.scenes.game);
    }
    private void OnExitButtonClick()
    {
        UnityEngine.Application.Quit();
    }
}
