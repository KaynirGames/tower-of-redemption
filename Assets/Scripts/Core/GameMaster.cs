using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    public static GameMaster Instance { get; private set; }

    [SerializeField] private CanvasGroup _loadingScreenGroup = null;
    [SerializeField] private Transform _dungeonTitlePlacement = null;

    public GameSettings GameSettings { get; private set; }
    public bool IsPause { get; private set; }

    private DungeonStageGenerator _stageGenerator;
    private AssetManager _assetManager;
    private PoolManager _poolManager;
    private MusicManager _musicManager;

    private Queue<DungeonStage> _selectedStages;
    private DungeonStage _currentStage;
    private GameObject _selectedPlayerPrefab;

    private SaveData _currentSaveData;
    private bool _continueGame;

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

        GameSettings = GetComponent<GameSettings>();
        _stageGenerator = GetComponent<DungeonStageGenerator>();
        BattleManager.OnBattleExit += PlayStageTheme;
        Credits.OnCreditsEnd += () => LoadScene(SceneType.MainMenu);

        SaveSystem.Init();
    }

    private void Start()
    {
        _assetManager = AssetManager.Instance;
        _poolManager = PoolManager.Instance;
        _musicManager = MusicManager.Instance;
    }

    public void TogglePause(bool isPause)
    {
        if (!MenuUI.Instance.InMenu)
        {
            IsPause = isPause;
            Time.timeScale = isPause ? 0f : 1f;
        }
    }

    public void LoadScene(SceneType sceneType)
    {
        switch (sceneType)
        {
            case SceneType.MainMenu:
                StartCoroutine(LoadMainMenuRoutine());
                break;
            case SceneType.Tutorial:
                StartCoroutine(LoadTutorialRoutine());
                break;
            case SceneType.Dungeon:
                StartCoroutine(LoadDungeonRoutine());
                break;
            case SceneType.Credits:
                StartCoroutine(LoadCreditsRoutine());
                break;
            default:
                break;
        }
    }

    public void StartNewGame(PlayerCharacter player)
    {
        _continueGame = false;
        _selectedStages = new Queue<DungeonStage>(_assetManager.StageDatabase.GetDungeonStageSet());
        _selectedPlayerPrefab = player.gameObject;
        LoadScene(SceneType.Dungeon);
    }

    public void ContinueGame()
    {
        _continueGame = true;
        _selectedStages = new Queue<DungeonStage>(_currentSaveData.LoadDungeonStages());
        LoadScene(SceneType.Dungeon);
    }

    public bool TryLoadSaveData()
    {
        _currentSaveData = SaveSystem.LoadData() as SaveData;
        return _currentSaveData != null;
    }

    public void SaveGame()
    {
        _currentSaveData = new SaveData(PlayerCharacter.Active,
                                        new List<DungeonStage>(_selectedStages));
        SaveSystem.SaveData(_currentSaveData);
    }

    private IEnumerator LoadMainMenuRoutine()
    {
        yield return ToggleLoadingScreenRoutine(true);
        yield return GameSettings.InitLocalizationRoutine();
        yield return AsyncLoadRoutine(SceneType.MainMenu);
        yield return ToggleLoadingScreenRoutine(false);
        _musicManager.PlayMusic("MainMenu");
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

        if (_selectedStages.Count > 0)
        {
            _currentStage = _selectedStages.Dequeue();
            yield return _stageGenerator.LoadDungeonStage(_currentStage);
            SpawnPlayer(Vector3.zero);
            ShowDungeonStageTitle(_currentStage);
            PlayStageTheme();
        }
        else
        {
            SaveSystem.DeleteSaveFile();
            yield return AsyncLoadRoutine(SceneType.Credits);
        }

        yield return ToggleLoadingScreenRoutine(false);
    }

    private IEnumerator LoadCreditsRoutine()
    {
        yield return ToggleLoadingScreenRoutine(true);
        yield return AsyncLoadRoutine(SceneType.Credits);
        yield return ToggleLoadingScreenRoutine(false);
        _musicManager.PlayMusic("Credits");
    }

    private IEnumerator ToggleLoadingScreenRoutine(bool enable)
    {
        yield return null;

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
        ClearTemporaryData();

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync((int)sceneType);

        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }

    private void SpawnPlayer(Vector3 position)
    {
        if (_continueGame)
        {
            PlayerCharacter player = _currentSaveData.LoadPlayer();
            player.transform.position = position;
        }
        else
        {
            Instantiate(_selectedPlayerPrefab,
                        position,
                        Quaternion.identity);
        }
    }

    private void ShowDungeonStageTitle(DungeonStage stage)
    {
        _poolManager.Take(PoolTags.DungeonTitlePopup.ToString())
                    .GetComponent<TextPopup>()
                    .Setup(stage.StageName, _dungeonTitlePlacement.position);
    }

    private void ClearTemporaryData()
    {
        Room.LoadedRooms.Clear();
        EnemyCharacter.ActiveEnemies.Clear();
        _currentStage = null;

        _poolManager.ClearPools();
    }

    private void PlayStageTheme()
    {
        if (_currentStage != null)
        {
            _musicManager.PlayMusic(_currentStage.StageTheme);
        }
    }
}