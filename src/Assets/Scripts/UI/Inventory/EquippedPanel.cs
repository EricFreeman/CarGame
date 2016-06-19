using Assets.Scripts.Inventory;
using UnityEngine;
using UnityEngine.UI;

public class EquippedPanel : MonoBehaviour
{
    public Item EquippedItem;
    public ItemType ItemType;
    public Image SelectionImage;

    public Sprite SelectedSprite;

    private InventoryManager _inventoryManager;

    void Start()
    {
        _inventoryManager = FindObjectOfType<InventoryManager>();
    }

    public void EquipItem()
    {
        var item = _inventoryManager.SelectedItem.Item;

        if (item != null && item.Type == ItemType)
        {
            EquippedItem = item;
            SelectionImage.sprite = SelectedSprite;
        }
    }
}