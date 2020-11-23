using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private DungeonStage _tutorialStage = null;
    [SerializeField] private Room _startRoom = null;

    private void Start()
    {
        PrepareTutorialRooms();

        _startRoom.ToggleDoors(false);
    }

    private void PrepareTutorialRooms()
    {
        Room.LoadedRooms.ForEach(room => room.PrepareRoom(_tutorialStage));
    }
}