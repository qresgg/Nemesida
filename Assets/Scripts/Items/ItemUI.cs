using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ItemUI : MonoBehaviour
{
    [SerializeField] private TMP_Text IsNewItem;
    [SerializeField] private Image itemSprite;
    [SerializeField] private TMP_Text itemName;
    [SerializeField] private TMP_Text itemDescription;
    [SerializeField] private Button selectButton;

    Player _player;
    ItemInventory _itemInventory;

    private void Start()
    {
        _itemInventory = GameObject.Find("ItemPanel").GetComponent<ItemInventory>();
        _player = GameObject.Find("Player").GetComponent<Player>();
        Debug.Log("ITEMUI STARTED");
    }

    public void SetItem(Item item)
    {
        if (item == null)
        {
            DefaultSprite();

            itemName.text = "DEF";
            itemDescription.text = "DEF";
            IsNewItem.text = "DEF";
            selectButton.onClick.RemoveAllListeners();
            selectButton.onClick.AddListener(() => GameManager.Instance.CloseItemPickerMenu());
        }
        else
        {
            IsNewItem.text = item.IsNewItem ? "new item!" : $"Level: {item.ItemLevel.Level}";
            itemSprite.sprite = Resources.Load<Sprite>(item.IconPath);

            itemName.text = item.Name;
            itemDescription.text = item.Description;

            selectButton.onClick.RemoveAllListeners();
            selectButton.onClick.AddListener(() => SelectItem(item));

            Debug.Log(item.Code);
        }
    }

    private void SelectItem(Item item)
    {
        _itemInventory.AddItem(item); // add to UI Inventory
        item.UpgradeItem(); // upgrade level

        GameManager.Instance.CloseItemPickerMenu();
    }
    private void DefaultSprite()
    {
        this.itemSprite.sprite = Resources.Load<Sprite>("Images/UI/ItemIcons/default");
    }
}
