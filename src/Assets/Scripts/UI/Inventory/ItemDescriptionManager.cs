using Assets.Scripts.Inventory;
using UnityEngine;
using UnityEngine.UI;

public class ItemDescriptionManager : MonoBehaviour
{
    public Text Name;
    public Text Description;
    public Image Image;

    public void SelectItem(Item item)
    {
        Name.text = item.Name;
        Description.text = item.Description;
        Image.sprite = item.Image;
    }
}