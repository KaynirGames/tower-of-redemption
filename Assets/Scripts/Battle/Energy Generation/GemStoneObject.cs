using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGemStone", menuName = "Scriptable Objects/Battle/Gem Stone Object")]
public class GemStoneObject : ScriptableObject
{
    [SerializeField] private float _energyGainValue = 2f;
    [SerializeField] private Sprite _gemSprite = null;
    [SerializeField] private List<GemStoneObject> _matchingGemStones = new List<GemStoneObject>();

    public float EnergyGainValue => _energyGainValue;
    public Sprite GemSprite => _gemSprite;

    public bool IsMatching(GemStoneObject gemStoneObject)
    {
        return _matchingGemStones.Contains(gemStoneObject);
    }
}