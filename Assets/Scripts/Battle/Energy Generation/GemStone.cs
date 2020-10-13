using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGemStone", menuName = "Scriptable Objects/Battle/Gem Stone")]
public class GemStone : ScriptableObject
{
    [SerializeField] private float _energyGainValue = 2f;
    [SerializeField] private Sprite _gemSprite = null;
    [SerializeField] private List<GemStone> _matchingGemStones = new List<GemStone>();

    public float EnergyGainValue => _energyGainValue;
    public Sprite GemSprite => _gemSprite;

    public bool IsMatching(GemStone gemStone)
    {
        return _matchingGemStones.Contains(gemStone);
    }
}