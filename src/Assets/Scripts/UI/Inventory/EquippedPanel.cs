using Assets.Scripts.Inventory;
using UnityEngine;
using UnityEngine.UI;

public class EquippedPanel : MonoBehaviour
{
    [HideInInspector]
    public Item EquippedItem;

    public ItemType ItemType;
    public Image SelectionImage;
    public Image EquippedItemImage;

    public Sprite SelectedSprite;
    public Sprite EmptySprite;

    private InventoryManager _inventoryManager;

    void Start()
    {
        _inventoryManager = FindObjectOfType<InventoryManager>();
    }

    public void EquipItem()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            UnequipItem();
        }
        else
        {
            if(_inventoryManager.SelectedItem == null || _inventoryManager.SelectedItem.Item == null)
            {
                return;
            }

            var item = _inventoryManager.SelectedItem.Item;

            if (item != null && item.Type == ItemType)
            {
                if (EquippedItem != null)
                {
                    _inventoryManager.AddItem(EquippedItem);
                }

                EquippedItem = item;
                SelectionImage.sprite = SelectedSprite;
                EquippedItemImage.sprite = item.Image;
                EquippedItemImage.color = new Color(1, 1, 1, 1);
                _inventoryManager.RemoveItem(EquippedItem);
            }
        }
    }

    public void UnequipItem()
    {
        _inventoryManager.AddItem(EquippedItem);
        EquippedItem = null;
        SelectionImage.sprite = EmptySprite;
        EquippedItemImage.sprite = null;
        EquippedItemImage.color = new Color(1, 1, 1, 0);
    }
}