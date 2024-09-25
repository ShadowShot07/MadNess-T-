using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIGameMenu : MonoBehaviour
{
    [Header("Botones del menu")]
    [SerializeField] private Button _backToGame;
    [SerializeField] private Button _backToMainMenu;
    [SerializeField] private Button _exitToGame;

    [Header("Menu")]
    [SerializeField] private GameObject _optionMenu;

    [Header("Botones Idioma")]
    [SerializeField] private Button _spanishbutton;
    [SerializeField] private Button _Englishbutton;
    [SerializeField] private Button _Catalahbutton;

    [Header("Textos")]
    [SerializeField] private TextMeshProUGUI _language;
    [SerializeField] private TextMeshProUGUI _sound;

    private Movement _pauseInput;
    private bool isPaused;

    private void Awake()
    {
        _pauseInput = new();
        _pauseInput.Player.Enable();
        isPaused = false;
        ActiveTrueFalse.Activefalse(_optionMenu);
    }
    void Start()
    {
        _backToGame.onClick.AddListener(BackToGame);
        _backToMainMenu.onClick.AddListener(BackToMainMenu);
        _exitToGame.onClick.AddListener(CloseGame);

        _spanishbutton.onClick.AddListener(SpanishLanguage);
        _Englishbutton.onClick.AddListener(EnglishLanguage);
        _Catalahbutton.onClick.AddListener(CatalaLanguage);
    }

    private void Update()
    {
        OpenMenu();
    }

    private void OpenMenu()
    {
        if (_pauseInput.Player.Esc.WasPressedThisFrame())
        {
            if (isPaused)
            {
                BackToGame();
            } else
            {
                PauseGame();
            }
        }
    }

    private void PauseGame()
    {
        ActiveTrueFalse.ActiveTrue(_optionMenu);
        Time.timeScale = 0f;
        isPaused = true;
    }

    private void BackToGame()
    {
        ActiveTrueFalse.Activefalse(_optionMenu);
        Time.timeScale = 1f;
        isPaused = false;
    }

    private void SpanishLanguage()
    {
        ScenesManager.instance.CurrentLanguage = ScenesManager.Language.Español;
        _language.text = "Idioma";
        _sound.text = "Sonido";
    }

    private void EnglishLanguage()
    {
        ScenesManager.instance.CurrentLanguage = ScenesManager.Language.Ingles;
        _language.text = "Language";
        _sound.text = "Sound";
    }

    private void CatalaLanguage()
    {
        ScenesManager.instance.CurrentLanguage = ScenesManager.Language.Catala;
        _language.text = "Idioma";
        _sound.text = "So";
    }

    private void BackToMainMenu()
    {
        ScenesManager.instance.LoadMainMenu();
    }

    private void CloseGame()
    {
        ScenesManager.instance.ExitGame();
    }
}
