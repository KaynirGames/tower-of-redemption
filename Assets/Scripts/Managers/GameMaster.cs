using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    public static GameMaster Instance { get; private set; }

    [SerializeField] private CanvasGroup _loadingScreenGroup;

    public bool testState;

    public bool IsPause { get; private set; }

    private GameSettings _gameSettings;
    private DungeonStageGenerator _stageGenerator;
    private AssetManager _assetManager;

    private Queue<DungeonStage> _selectedStages = new Queue<DungeonStage>();
    private DungeonStage _currentStage;

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

        DontDestroyOnLoad(gameObject);

        _gameSettings = GetComponent<GameSettings>();
        _stageGenerator = GetComponent<DungeonStageGenerator>();

        if (testState)
        {
            _gameSettings.SetLanguage();
        }
    }

    private void Start()
    {
        _assetManager = AssetManager.Instance;

        if (!testState)
        {
            StartCoroutine(LoadGameMenuRoutine(true));
        }
    }

    public void TogglePause(bool isPause)
    {
        IsPause = isPause;
        Time.timeScale = isPause ? 0f : 1f;
    }

    public void LoadScene(SceneType sceneType)
    {
        switch (sceneType)
        {
            case SceneType.GameMenu:
                StartCoroutine(LoadGameMenuRoutine());
                break;
            case SceneType.Tutorial:
                StartCoroutine(LoadTutorialRoutine());
                break;
            case SceneType.Dungeon:
                StartCoroutine(LoadDungeonRoutine());
                break;
            default:
                break;
        }
    }

    private IEnumerator LoadGameMenuRoutine(bool isApplicationStart = false)
    {
        yield return ToggleLoadingScreenRoutine(true);

        if (isApplicationStart)
        {
            yield return _gameSettings.SetLanguage();
        }

        yield return AsyncLoadRoutine(SceneType.GameMenu);
        yield return ToggleLoadingScreenRoutine(false);
    }

    private IEnumerator LoadTutorialRoutine()
    {
        yield return ToggleLoadingScreenRoutine(true);
        yield return AsyncLoadRoutine(SceneType.Tutorial);
        yield return ToggleLoadingScreenRoutine(false);
    }

    private IEnumerator LoadDungeonRoutine()
    {
        yield return ToggleLoadingScreenRoutine(true);
        yield return AsyncLoadRoutine(SceneType.Dungeon);

        if (_selectedStages.Count == 0)
        {
            _assetManager.StageDatabase.GetDungeonStageSet()
                                       .ForEach(stage => _selectedStages.Enqueue(stage));
        }

        _currentStage = _selectedStages.Dequeue();
        yield return _stageGenerator.LoadDungeonStage(_currentStage);
        yield return ToggleLoadingScreenRoutine(false);
    }

    public void LoadDungeon()
    {
        // проверка на последнюю стадию, иначе грузим следующую в очереди

        StartCoroutine(LoadDungeonRoutine());
    }

    private IEnumerator ToggleLoadingScreenRoutine(bool enable)
    {
        if (enable)
        {
            _loadingScreenGroup.gameObject.SetActive(true);
            yield return ScreenFader.FadeIn(_loadingScreenGroup);
        }
        else
        {
            yield return ScreenFader.FadeOut(_loadingScreenGroup);
            _loadingScreenGroup.gameObject.SetActive(false);
        }
    }

    private IEnumerator AsyncLoadRoutine(SceneType sceneType)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync((int)sceneType);

        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }

    private void SpawnPlayer(PlayerCharacter prefab, Vector3 position)
    {
        Instantiate(prefab, position, Quaternion.identity);
    }
}