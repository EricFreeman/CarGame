using Assets.Scripts.Inventory;
using UnityEngine;
using UnityEngine.UI;

public class EquippedPanel : MonoBehaviour
{
    public Item EquippedItem;
    public ItemType ItemType;
    public Image SelectionImage;

    public Sprite SelectedSprite;

    public void EquipItem(Item item)
    {
        if (item.Type == ItemType)
        {
            EquippedItem = item;
            SelectionImage.sprite = SelectedSprite;
        }
    }
}