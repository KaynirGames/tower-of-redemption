using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    [SerializeField] private string _specID = null;

    [SerializeField] private string[] _activeSkillsID = null;
    [SerializeField] private string[] _passiveSkillsID = null;
    [SerializeField] private string[] _specialSkillsID = null;

    [SerializeField] private string[] _consumablesID = null;
    [SerializeField] private string[] _keyItemsID = null;

    [SerializeField] private string[] _dungeonStagesID = null;

    public SaveData(PlayerCharacter player, List<DungeonStage> dungeonStages)
    {
        _specID = player.PlayerSpec.ID;

        _activeSkillsID = GetSkillsID(player.SkillBook, SkillSlot.Active);
        _passiveSkillsID = GetSkillsID(player.SkillBook, SkillSlot.Passive);
        _specialSkillsID = GetSkillsID(player.SkillBook, SkillSlot.Special);

        _consumablesID = GetItemsID(player.Inventory, ItemSlot.Consumable);
        _keyItemsID = GetItemsID(player.Inventory, ItemSlot.KeyItem);

        _dungeonStagesID = GetDungeonStagesID(dungeonStages);
    }

    public PlayerCharacter LoadPlayer()
    {
        GameObject playerObject = AssetManager.Instance.PlayerDatabase.CreatePlayer(_specID);
        PlayerCharacter player = playerObject.GetComponent<PlayerCharacter>();

        SkillSO[] activeSkills = GetSkillsByID(_activeSkillsID);
        SkillSO[] passiveSkills = GetSkillsByID(_passiveSkillsID);
        SkillSO[] specialSkills = GetSkillsByID(_specialSkillsID);

        player.SkillBook.LoadSkills(activeSkills);
        player.SkillBook.LoadSkills(passiveSkills);
        player.SkillBook.LoadSkills(specialSkills);

        List<ItemSO> consumables = GetItemsByID(_consumablesID);
        List<ItemSO> keyItems = GetItemsByID(_keyItemsID);

        consumables.ForEach(item => player.Inventory.AddItem(item));
        keyItems.ForEach(item => player.Inventory.AddItem(item));

        return player;
    }

    public List<DungeonStage> LoadDungeonStages()
    {
        List<DungeonStage> stages = new List<DungeonStage>();
        DungeonStageDatabase database = AssetManager.Instance.StageDatabase;

        for (int i = 0; i < _dungeonStagesID.Length; i++)
        {
            stages.Add(database.Find(stage => stage.ID == _dungeonStagesID[i]));
        }

        return stages;
    }

    private SkillSO[] GetSkillsByID(string[] skillsID)
    {
        SkillSO[] skills = new SkillSO[skillsID.Length];
        SkillDatabase database = AssetManager.Instance.SkillDatabase;

        for (int i = 0; i < skillsID.Length; i++)
        {
            skills[i] = skillsID[i] == string.Empty
                ? null
                : database.Find(skill => skill.ID == skillsID[i]);
        }

        return skills;
    }

    private List<ItemSO> GetItemsByID(string[] itemsID)
    {
        List<ItemSO> items = new List<ItemSO>();
        ItemDatabase database = AssetManager.Instance.ItemDatabase;

        for (int i = 0; i < itemsID.Length; i++)
        {
            items.Add(database.Find(item => item.ID == itemsID[i]));
        }

        return items;
    }

    private string[] GetItemsID(Inventory inventory, ItemSlot slot)
    {
        var items = inventory.GetInventorySlots(slot);
        string[] array = new string[items.Count];

        for (int i = 0; i < items.Count; i++)
        {
            array[i] = items[i].ItemSO.ID;
        }

        return array;
    }

    private string[] GetSkillsID(SkillBook skillBook, SkillSlot slot)
    {
        var skills = skillBook.GetSkillSlots(slot);
        string[] array = new string[skills.Length];

        for (int i = 0; i < skills.Length; i++)
        {
            array[i] = skills[i] == null
                ? string.Empty
                : skills[i].SkillSO.ID;
        }

        return array;
    }

    private string[] GetDungeonStagesID(List<DungeonStage> dungeonStages)
    {
        string[] array = new string[dungeonStages.Count];

        for (int i = 0; i < dungeonStages.Count; i++)
        {
            array[i] = dungeonStages[i].ID;
        }

        return array;
    }
}
