using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UndefinedItem SO", menuName = "Scriptable Objects/Items/Recovery Item SO")]
public class RecoveryItemSO : ItemSO
{
    [SerializeField] private List<RecoverySO> _recoveryTypes = new List<RecoverySO>();

    public override void Use(Character owner)
    {
        _recoveryTypes.ForEach(recovery => recovery.ApplyRecovery(owner));
    }
}