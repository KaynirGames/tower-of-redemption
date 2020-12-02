using UnityEngine;

[CreateAssetMenu(fileName = "UndefinedItem SO", menuName = "Scriptable Objects/Items/Skill Scroll SO")]
public class SkillScrollSO : ItemSO
{
    [SerializeField] private SkillSO _skillSO = null;

    public override bool TryUse(PlayerCharacter player, Item item)
    {
        if (!player.SkillBook.TryAddSkill(_skillSO))
        {
            MenuUI.Instance.ShowSkillReplaceWindow(_skillSO,
                                                 (slotID) => {
                                                     player.SkillBook.ReplaceSkill(slotID, _skillSO);
                                                     player.Inventory.RemoveItem(item);
                                                 });
            return false;
        }

        return true;
    }
}
