using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class GameMaster : MonoBehaviour
{
    public static GameMaster Instance { get; private set; }

    [SerializeField] private SystemLanguage _gameLanguage = SystemLanguage.Russian;
    [SerializeField] private int _gameLanguageID = 0;
    [SerializeField] private GameObject _testPlayer;

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

        StartCoroutine(SetLanguage());
    }

    public void TogglePause(bool isPause)
    {
        IsPause = isPause;
        Time.timeScale = isPause ? 0f : 1f;
    }

    private IEnumerator SetLanguage()
    {
        yield return LocalizationSettings.InitializationOperation;

        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_gameLanguageID];
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        Instantiate(_testPlayer, transform.position, Quaternion.identity);
    }
}