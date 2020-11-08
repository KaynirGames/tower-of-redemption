using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster Instance { get; private set; }

    [SerializeField] private SystemLanguage _gameLanguage = SystemLanguage.Russian;

    public SystemLanguage GameLanguage => _gameLanguage;

    public bool IsPause { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        Translator.SetTranslation(_gameLanguage);
        Debug.Log("Current language is set to " + _gameLanguage.ToString());
    }

    public void TogglePause(bool isPause)
    {
        IsPause = isPause;
        Time.timeScale = isPause ? 0f : 1f;
    }
}
