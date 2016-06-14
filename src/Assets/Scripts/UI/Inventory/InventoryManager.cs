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

    public Item TempDeleteMeLaterSeriouslyFuck;
    public Item TempDeleteMeLaterSeriouslyFuck2;

    private ItemPanel _selectedItem;

    void Start()
    {
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
            
            if(_selectedItem != null)
            {
                var oldItem = Items.FirstOrDefault(x => x.GetComponent<ItemPanel>().Item.Id == _selectedItem.Item.Id);
                oldItem.GetComponent<ItemPanel>().Deselect();
            }

            _selectedItem = item;
        }
    }
}