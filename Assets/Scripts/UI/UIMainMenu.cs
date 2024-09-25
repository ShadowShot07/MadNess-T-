using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    [Header("Botones")]
    [SerializeField] private Button _startGame;
    [SerializeField] private Button _options;
    [SerializeField] private Button _credits;
    [SerializeField] private Button _closeGamePanel;
    [SerializeField] private Button _optionsToMenu;
    [SerializeField] private Button _creditsToMenu;
    [SerializeField] private Button _closeGame;
    [SerializeField] private Button _exitToMenu;

    [Header("Menus")]
    [SerializeField] private GameObject _mainMenuCanvas;
    [SerializeField] private GameObject _menuOptions;
    [SerializeField] private GameObject _menuCredits;
    [SerializeField] private GameObject _exitPanel;

    [Header("Botones Idioma")]
    [SerializeField] private Button _spanish;
    [SerializeField] private Button _english;
    [SerializeField] private Button _catala;

    [Header("Textos")]
    [SerializeField] private TextMeshProUGUI _language;
    [SerializeField] private TextMeshProUGUI _sound;

    void Start()
    {
        ActiveTrueFalse.ActiveTrue(_mainMenuCanvas);
        ActiveTrueFalse.Activefalse(_menuOptions);
        ActiveTrueFalse.Activefalse(_menuCredits);
        ActiveTrueFalse.Activefalse(_exitPanel);
        Listeners();

        AudioManager.instance.StartMusic();
    }

    private void StartGame()
    {
        ScenesManager.instance.LoadNewGame();
    }

    private void StartOptions()
    {
        ActiveTrueFalse.Activefalse(_mainMenuCanvas);
        ActiveTrueFalse.ActiveTrue(_menuOptions);
    }

    private void StartCredits()
    {
        ActiveTrueFalse.Activefalse(_mainMenuCanvas);
        ActiveTrueFalse.ActiveTrue(_menuCredits);
    }

    private void OptionsToMenu()
    {
        ActiveTrueFalse.ActiveTrue(_mainMenuCanvas);
        ActiveTrueFalse.Activefalse(_menuOptions);
    }
    
    private void CreditsToMenu()
    {
        ActiveTrueFalse.ActiveTrue(_mainMenuCanvas);
        ActiveTrueFalse.Activefalse(_menuCredits);
    }

    private void StartExitPanel()
    {
        ActiveTrueFalse.Activefalse (_mainMenuCanvas);
        ActiveTrueFalse.ActiveTrue(_exitPanel);
    }

    private void ExitPanelToMenu()
    {
        ActiveTrueFalse.Activefalse(_exitPanel);
        ActiveTrueFalse.ActiveTrue(_mainMenuCanvas);
    }

    private void CloseGame()
    {
        ScenesManager.instance.ExitGame();
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

    private void Listeners()
    {
        _startGame.onClick.AddListener(StartGame);

        //Iniciar Menus Ocultos
        _options.onClick.AddListener(StartOptions);
        _credits.onClick.AddListener(StartCredits);
        _closeGamePanel.onClick.AddListener(StartExitPanel);

        //Botones Menus Ocultos
        _closeGame.onClick.AddListener(CloseGame);
        _optionsToMenu.onClick.AddListener(OptionsToMenu);
        _creditsToMenu.onClick.AddListener(CreditsToMenu);
        _exitToMenu.onClick.AddListener(ExitPanelToMenu);

        //Botones de Idioma
        _spanish.onClick.AddListener(SpanishLanguage);
        _english.onClick.AddListener(EnglishLanguage);
        _catala.onClick.AddListener(CatalaLanguage);
    }
}