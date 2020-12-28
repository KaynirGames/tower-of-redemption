using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;

public class GameMaster : MonoBehaviour
{
    public static GameMaster Instance { get; private set; }

    [SerializeField] private CanvasGroup _loadingScreenGroup = null;
    [Header("Название этажа подземелья:")]
    [SerializeField] private CanvasGroup _dungeonTitleGroup = null;
    [SerializeField] private float _titleScaleDuration = 2f;
    [SerializeField] private float _titleFadeDuration = 4f;

    public GameSettings GameSettings { get; private set; }
    public bool IsPause { get; private set; }

    private DungeonStageGenerator _stageGenerator;
    private AssetManager _assetManager;
    private TextMeshProUGUI _dungeonTitleField;

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
        _dungeonTitleField = _dungeonTitleGroup.GetComponentInChildren<TextMeshProUGUI>();

        SaveSystem.Init();
    }

    private void Start()
    {
        _assetManager = AssetManager.Instance;
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

    private IEnumerator LoadGameMenuRoutine()
    {
        yield return ToggleLoadingScreenRoutine(true);
        yield return GameSettings.InitLocalizationRoutine();
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

        if (_selectedStages.Count > 0)
        {
            _currentStage = _selectedStages.Dequeue();
            yield return _stageGenerator.LoadDungeonStage(_currentStage);
            SpawnPlayer(Vector3.zero);
            ShowDungeonStageTitle(_currentStage);
        }
        else
        {
            // load credits scene
            yield break;
        }

        yield return ToggleLoadingScreenRoutine(false);
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
        _dungeonTitleField.SetText(stage.StageName);
        _dungeonTitleField.transform.localScale = Vector3.zero;
        _dungeonTitleGroup.alpha = 1f;

        _dungeonTitleField.transform.DOScale(Vector3.one, _titleScaleDuration)
                                    .OnComplete(() =>
                                    _dungeonTitleGroup.DOFade(0f, _titleFadeDuration));
    }
}