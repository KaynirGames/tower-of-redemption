using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : ScriptableObject
{
    [SerializeField] private TranslatedText _name = new TranslatedText("Item.TextID.Name");
    [SerializeField] private TranslatedText _flavorText = new TranslatedText("Item.TextID.Flavor");
    [SerializeField] private TranslatedText _description = new TranslatedText("Item.TextID.Description");
}
