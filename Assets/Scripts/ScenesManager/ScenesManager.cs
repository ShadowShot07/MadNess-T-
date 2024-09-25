using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager instance;
    public Language CurrentLanguage;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }else
        {
            Destroy(gameObject);
        }

        CurrentLanguage = Language.Español;
    }

    public enum Scene
    {
        MenuPrincipal,
        Escena1
    }

    public enum Language
    {
        Español,
        Ingles,
        Catala
    }

    public void LoadNewGame()
    {
        SceneManager.LoadScene(Scene.Escena1.ToString());
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(Scene.MenuPrincipal.ToString());
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
