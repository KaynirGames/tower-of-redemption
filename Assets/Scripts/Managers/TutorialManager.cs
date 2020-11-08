using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private DungeonStage _tutorialStage = null;

    private void Start()
    {
        PrepareTutorialRooms();
    }

    private void PrepareTutorialRooms()
    {
        Room.LoadedRooms.ForEach(room => room.PrepareRoom(_tutorialStage));
    }
}
