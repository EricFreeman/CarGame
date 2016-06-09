using Assets.Scripts.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public ItemPanel SelectedItem;
    public List<ItemPanel> Items;

    public ItemDescriptionManager ItemDescriptionManager;

    public void SelectItem(Guid id)
    {
        var item = Items.FirstOrDefault(x => x.GetComponent<Item>().Id == id);
        if (item != null)
        {
            ItemDescriptionManager.SelectItem(item.Item);
            
            if(SelectedItem != null)
            {
                var oldItem = Items.FirstOrDefault(x => x.GetComponent<Item>().Id == id);
                oldItem.GetComponent<ItemPanel>().Deselect();
            }

            SelectedItem = item;
        }
    }
}