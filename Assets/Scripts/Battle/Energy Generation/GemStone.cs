using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gemstone", menuName = "Scriptable Objects/Battle/Gemstone")]
public class Gemstone : ScriptableObject
{
    [SerializeField] private float _energyGainValue = 2f;
    [SerializeField] private Sprite _gemSprite = null;
    [SerializeField] private ParticleSystem _consumedGemstoneParticles = null;
    [SerializeField] private List<Gemstone> _matchingGemStones = new List<Gemstone>();

    public float EnergyGainValue => _energyGainValue;
    public Sprite GemSprite => _gemSprite;
    public ParticleSystem ConsumedGemstoneParticles => _consumedGemstoneParticles;

    public bool IsMatching(Gemstone gemStone)
    {
        return _matchingGemStones.Contains(gemStone);
    }
}