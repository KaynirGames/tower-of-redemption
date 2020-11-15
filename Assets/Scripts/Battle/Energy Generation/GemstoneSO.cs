using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Undefined Gemstone SO", menuName = "Scriptable Objects/Battle/Gemstone SO")]
public class GemstoneSO : ScriptableObject
{
    [SerializeField] private float _energyGainValue = 2f;
    [SerializeField] private Sprite _gemSprite = null;
    [SerializeField] private ParticleSystem _consumedGemstoneParticles = null;
    [SerializeField] private List<GemstoneSO> _matchingGemStones = new List<GemstoneSO>();

    public float EnergyGainValue => _energyGainValue;
    public Sprite GemSprite => _gemSprite;
    public ParticleSystem ConsumedGemstoneParticles => _consumedGemstoneParticles;

    public bool IsMatching(GemstoneSO gemStoneSO)
    {
        return _matchingGemStones.Contains(gemStoneSO);
    }
}