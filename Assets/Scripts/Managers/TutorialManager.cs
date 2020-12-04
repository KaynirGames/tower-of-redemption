using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [Header("Вступление:")]
    [SerializeField] private Dialogue _welcomeDialogue = null;
    [SerializeField] private Button _openMenuButton = null;
    [SerializeField] private float _dialogueDelay = 2f;
    [Header("Обучение атаке:")]
    [SerializeField] private Room _attackTutorialRoom = null;
    [SerializeField] private SpawnPoint _burrelSpawnPoint = null;
    [SerializeField] private ItemPickup _tutorialPotion = null;
    [Header("Обучение бою:")]
    [SerializeField] private Dialogue _battleEnterDialogue = null;
    [SerializeField] private Dialogue _battleExitDialogue = null;
    [SerializeField] private GameObject _tutorialExit = null;
    [SerializeField] private ItemPickup _tutorialScroll = null;
    [SerializeField] private Transform _scrollSpawnPoint = null;

    private DialogueManager _dialogueManager;
    private WaitForSecondsRealtime _waitForDialogue;

    private void Awake()
    {
        BattleManager.OnBattleEnter += TriggerBattleEnterDialogue;
        BattleManager.OnBattleExit += TriggerBattleExitDialogue;
    }

    private void Start()
    {
        _dialogueManager = DialogueManager.Instance;
        _waitForDialogue = new WaitForSecondsRealtime(_dialogueDelay);
        StartCoroutine(StartTutorialRoutine());
    }

    public void ResetOpenMenuButton()
    {
        _openMenuButton.onClick.RemoveAllListeners();
        _openMenuButton.onClick.AddListener(MenuUI.Instance.OpenMenu);
    }

    public void SpawnTutorialPotion()
    {
        var spawnList = _burrelSpawnPoint.Spawn(_tutorialPotion.gameObject, 1, true);

        spawnList[0].GetComponent<ItemPickup>().OnInteraction
                    .AddListener(() => _attackTutorialRoom.SetRoomStatus(true));
    }

    private IEnumerator StartTutorialRoutine()
    {
        Room.LoadedRooms.ForEach(room => room.PrepareRoom());

        GameMaster.Instance.TogglePause(true);
        
        yield return _waitForDialogue;

        _dialogueManager.StartDialogue(_welcomeDialogue);
    }

    private void TriggerBattleEnterDialogue()
    {
        StartCoroutine(TriggerDialogueRoutine(_battleEnterDialogue));
    }

    private void TriggerBattleExitDialogue()
    {
        GameObject scroll = Instantiate(_tutorialScroll.gameObject,
                                        _scrollSpawnPoint.position,
                                        Quaternion.identity);

        scroll.GetComponent<ItemPickup>().OnInteraction
              .AddListener(() => _tutorialExit.SetActive(true));

        StartCoroutine(TriggerDialogueRoutine(_battleExitDialogue));
    }

    private IEnumerator TriggerDialogueRoutine(Dialogue dialogue)
    {
        yield return _waitForDialogue;
        _dialogueManager.StartDialogue(dialogue);
    }
}