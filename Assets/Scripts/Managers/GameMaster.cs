using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    public static GameMaster Instance { get; private set; }

    [SerializeField] private GameObject _loadingScreen;

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
    }

    private void Start()
    {
        _assetManager = AssetManager.Instance;
        StartCoroutine(LoadGameRoutine());
    }

    public void TogglePause(bool isPause)
    {
        IsPause = isPause;
        Time.timeScale = isPause ? 0f : 1f;
    }

    public void LoadScene(SceneType sceneType)
    {
        StartCoroutine(LoadSceneRoutine(sceneType));
    }

    public void LoadDungeon()
    {
        // проверка на последнюю стадию, иначе грузим следующую в очереди

        StartCoroutine(LoadDungeonRoutine());
    }

    private IEnumerator LoadGameRoutine()
    {
        _loadingScreen.SetActive(true);

        yield return _gameSettings.SetLanguage();

        yield return LoadSceneRoutine(SceneType.GameMenu);
    }

    private IEnumerator LoadDungeonRoutine()
    {
        _loadingScreen.SetActive(true);

        yield return AsyncLoadRoutine(SceneType.Dungeon);

        if (_selectedStages.Count == 0)
        {
            _assetManager.StageDatabase.GetDungeonStageSet()
                                       .ForEach(stage => _selectedStages.Enqueue(stage));
        }

        _currentStage = _selectedStages.Dequeue();

        yield return _stageGenerator.LoadDungeonStage(_currentStage);

        _loadingScreen.SetActive(false);
    }

    private IEnumerator LoadSceneRoutine(SceneType sceneType)
    {
        _loadingScreen.SetActive(true);

        yield return AsyncLoadRoutine(sceneType);

        _loadingScreen.SetActive(false);
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