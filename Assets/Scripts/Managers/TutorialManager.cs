using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private Button _openMenuButton = null;
    [SerializeField] private Dialogue _welcomeDialogue = null;
    [SerializeField] private Room _attackTutorialRoom = null;
    [SerializeField] private SpawnPoint _burrelSpawnPoint = null;
    [SerializeField] private ItemPickup _tutorialPotion = null;
    [SerializeField] private Dialogue _battleDialogue = null;
    [SerializeField] private float _battleDialogueDelay = 2f;

    private DialogueManager _dialogueManager;

    private void Awake()
    {
        //BattleManager.OnBattleStart += TriggerBattleTutorial;
    }

    private void Start()
    {
        _dialogueManager = DialogueManager.Instance;
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
        yield return new WaitForSeconds(2f);

        //_dialogueManager.StartDialogue(_startDialogue);
    }

    private void TriggerBattleTutorial()
    {
        StartCoroutine(BattleTutorialRoutine());
    }

    private IEnumerator BattleTutorialRoutine()
    {
        yield return new WaitForSecondsRealtime(_battleDialogueDelay);

        _dialogueManager.StartDialogue(_battleDialogue);
    }
}