using System;
using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEngine;
using UnityEngine.UI;

public class ItemInventory : MonoBehaviour
{
    [SerializeField] private Image[] slots;
    private List<Item> itemsList = new List<Item>();
    private string _innateItem;

    List<Item> activeItems = new List<Item>();

    Dictionary<string, int> itemsDictionary = new Dictionary<string, int>();

    Player _player;
    ItemManager _itemManager;

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _itemManager = GameObject.Find("ItemManager").GetComponent<ItemManager>();

        itemsList = _itemManager.GetItemList();
        InnateItemBookSlot();
    }
    private void Update()
    {
        ItemSlotsInitializer();
    }
    public void AddItem(Item item)
    {
        if (!itemsDictionary.ContainsKey(item.Code))
        {
            for (int i = 1; i < slots.Length; i++)
            {
                if (slots[i].sprite == null)
                {
                    Sprite sprite = Resources.Load<Sprite>(item.IconPath);
                    if (sprite != null)
                    {
                        Color newColor = slots[i].color;
                        newColor.a = 0;
                        slots[i].color = newColor;

                        slots[i].sprite = sprite;
                        itemsDictionary.Add(item.Code, i);
                    }
                    return;
                }
            }
            Debug.Log("No empty slots available");
        }
        else
        {
            Debug.Log("CONTAINS");
        }
    }

    private Sprite GetSpriteByItemCode(string itemCode)
    {
        foreach (Item item in itemsList)
        {
            if (item.Code == itemCode)
            {
                //Debug.Log(ability.IconPath);
                return Resources.Load<Sprite>(item.IconPath);
            }
        }
        return null;
    }
    private void InnateItemBookSlot()
    {
        _innateItem = GameManager.Instance.GetPersonalItemCode();
        Sprite sprite = GetSpriteByItemCode(_innateItem);
        if (sprite != null)
        {
            slots[0].sprite = sprite;
            itemsDictionary.Add(_innateItem, 0);
        }
    }
    private void ItemSlotsInitializer()
    {
        foreach (Image img in slots)
        {
            Color newColor = img.color;
            if (img.sprite == null)
            {
                newColor.a = 0;
                img.color = newColor;
            }
            else
            {
                newColor.a = 1;
                img.color = newColor;
            }
        }
    }
}
