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
        if (item == null)
        {
            Name.text = "";
            Description.text = "";
            Image.sprite = null;
        }
        else
        {
            Name.text = item.Name;
            Description.text = item.Description;
            Image.sprite = item.Image;
        }
    }
}