using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public static SceneManagerScript instance;

    private static LanguagesState languagesState;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void LoadSceneByIndex(int index)
    {
        SetLanguageState(index);

        SceneManager.LoadScene(index);
    }

    private void SetLanguageState(int index)
    {
        switch (index)
        {
            case 1:
                languagesState = LanguagesState.English;
                break;

            case 2:
                languagesState = LanguagesState.Russian;
                break;

            case 3:
                languagesState  = LanguagesState.Azerbaijani;
                break;
        }
    }

    public LanguagesState GetLanguageState()
    {
        return languagesState;
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
