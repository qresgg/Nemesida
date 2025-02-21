using System.Collections.Generic;
using UnityEngine;

public class ItemPickerMenu : MonoBehaviour
{
    private List<Item> items = new List<Item>();

    [SerializeField] private ItemUI[] itemSlots;

    private string _personalItem;

    Player _player;
    ItemManager _ItemManager;

    [SerializeField] private bool isFirstCall = true;

    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _ItemManager = GameObject.Find("ItemManager").GetComponent<ItemManager>();

        _personalItem = GameManager.Instance.PersonalItemCode;
        items = _ItemManager.GetItemListFiltered();
        foreach (Item item in items )
        {
            Debug.Log(item.Code);
        }

        ShuffleItems();
        DisplayItems();
    }

    public void ShuffleItems()
    {
        System.Random random = new System.Random();
        int n = items.Count;

        for (int i = 0; i < n; i++)
        {
            int randomIndex = random.Next(i, n);
            Item temp = items[i];
            items[i] = items[randomIndex];
            items[randomIndex] = temp;
        }
    }

    public void DisplayItems()
    {
        int slotIndex = 0;
        for (int i = 0; i < items.Count && slotIndex < itemSlots.Length; i++)
        {
            if (items[i].ItemLevel.Level < 6) //|| !pickedAbilityCodes.Contains(abilities[i].Code))
            {
                itemSlots[slotIndex].SetItem(items[i]);
                slotIndex++;
            }
        }
        for (int i = slotIndex; i < itemSlots.Length; i++)
        {
            itemSlots[i].SetItem(null);
        }

        if (isFirstCall)
        {
            isFirstCall = false;
            Debug.Log("First call of DisplayAbilities completed.");
        }
    }

    public void SetActive(bool arg)
    {
        this.gameObject.SetActive(arg);
        if (arg)
        {
            ShuffleItems();
            DisplayItems();
        }
    }
}
