using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Item")]
public class Item : ScriptableObject
{
    public Sprite Sprite;
    public ItemType Type;

    public enum ItemType
    {
        SmallKey,
        BigKey,
        Weapon,
        Food,
        Potion
    }
}
