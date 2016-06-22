using System;
using UnityEngine;

namespace Assets.Scripts.Inventory
{
    public class Item : MonoBehaviour
    {
        public string Name;
        public string Description;
        public ItemType Type;
        public Guid Id = Guid.NewGuid();
        public Sprite Image;
    }

    public enum ItemType
    {
        None,
        Weapon,
        Armor,
        Engine,
        Wheels,
        Junk
    }
}