using Assets.Scripts.Inventory;
using UnityEngine;
using UnityEngine.UI;

public class ItemPanel : MonoBehaviour
{
    public Item Item;
    public Text Name;
    public Image SelectedOutline;

    private InventoryManager _inventoryManager;

    void Start()
    {
        Name.text = Item.Name;
        _inventoryManager = FindObjectOfType<InventoryManager>();
    }

    public void OnSelect()
    {
        Select();
        _inventoryManager.SelectItem(Item.Id);
    }

    public void Select()
    {
        SelectedOutline.color = new Color(5 / 255f, 101 / 255f, 101 / 255f, 1);
    }

    public void Deselect()
    {
        SelectedOutline.color = new Color(0, 0, 0, 0);
    }
}