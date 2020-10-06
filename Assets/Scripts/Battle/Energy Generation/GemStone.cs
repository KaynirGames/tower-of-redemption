using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GemStone
{
    public GemStoneObject GemObject { get; private set; }

    public int PositionX { get; set; }
    public int PositionY { get; set; }

    public GemStone(GemStoneObject gemObject, int posX, int posY)
    {
        GemObject = gemObject;

        PositionX = posX;
        PositionY = posY;
    }

    public bool TryMatch(GemStone gemStone)
    {
        return GemObject.IsMatching(gemStone.GemObject);
    }
}