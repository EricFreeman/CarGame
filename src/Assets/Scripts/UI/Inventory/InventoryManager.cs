using Assets.Scripts.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public List<ItemPanel> Items;
    public GameObject ItemPanel;
    public Transform ItemScroller;

    public ItemDescriptionManager ItemDescriptionManager;

    public Sprite EquippableSlotEmpty;
    public Sprite EquippableSlotCanEquip;
    public Sprite EquippableSlotEquipped;

    private ItemPanel _selectedItem;
    private List<EquippedPanel> _equippedPanel;

    void Start()
    {
        _equippedPanel = FindObjectsOfType<EquippedPanel>().ToList();

        // TODO: serialize/deserialize player inventory later
        CreateItem("GenericWeapon");
        CreateItem("JunkItem");
        CreateItem("MetalArmor");
        CreateItem("OffRoadWheels");
        CreateItem("RacingWheels");
        CreateItem("ScrapArmor");
        CreateItem("SuperMachineGun");
        CreateItem("V8Engine");
    }

    private void CreateItem(string name)
    {
        var item = Instantiate(Resources.Load<Item>("Items/Weapons/" + name));
        var panel = Instantiate(ItemPanel);
        panel.GetComponent<ItemPanel>().Item = item;
        panel.transform.SetParent(ItemScroller, false);
        Items.Add(panel.GetComponent<ItemPanel>());
    }

    public void SelectItem(Guid id)
    {
        var item = Items.FirstOrDefault(x => x.GetComponent<ItemPanel>().Item.Id == id);
        if (item != null)
        {
            item.Select();
            ItemDescriptionManager.SelectItem(item.Item);
            UpdateEquippableSlots(item.Item.Type);
            
            if(_selectedItem != null)
            {
                var oldItem = Items.FirstOrDefault(x => x.GetComponent<ItemPanel>().Item.Id == _selectedItem.Item.Id);
                oldItem.GetComponent<ItemPanel>().Deselect();
            }

            _selectedItem = item;
        }
    }

    private void UpdateEquippableSlots(ItemType itemType)
    {
        var equippable = _equippedPanel.Where(x => x.ItemType == itemType);
        var resettable = _equippedPanel.Where(x => x.ItemType != itemType);

        foreach (var item in equippable)
        {
            item.SelectionImage.sprite = EquippableSlotCanEquip;
        }

        foreach (var item in resettable)
        {
            if (true) // not equipped
            {
                item.SelectionImage.sprite = EquippableSlotEmpty;
            }
            else
            {
                item.SelectionImage.sprite = EquippableSlotEquipped;
            }
        }

    }
}