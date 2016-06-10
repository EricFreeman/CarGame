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

        for (var i = 0; i < 15; i++)
        {
            var temp = TempDeleteMeLaterSeriouslyFuck.Copy();
            var temp2 = TempDeleteMeLaterSeriouslyFuck2.Copy();

            temp.Id = Guid.NewGuid();
            temp2.Id = Guid.NewGuid();

            var panel = Instantiate(ItemPanel);
            panel.GetComponent<ItemPanel>().Item = temp;
            panel.transform.SetParent(ItemScroller, false);
            Items.Add(panel.GetComponent<ItemPanel>());

            var panel2 = Instantiate(ItemPanel);
            panel2.GetComponent<ItemPanel>().Item = temp2;
            panel2.transform.SetParent(ItemScroller, false);
            Items.Add(panel2.GetComponent<ItemPanel>());
        }
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