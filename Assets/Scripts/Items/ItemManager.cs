using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance { get; private set; }
    private List<Item> allItems = new List<Item>();
    private List<Item> itemsFiltered = new List<Item>();
    [SerializeField] private ItemPickerMenu _itemPickerMenu;

    private string _innateItem;

    private AbilityInventory _abilityInventory;
    private Player _player;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadItems();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();

        _innateItem = GameManager.Instance.GetInnateAbilityCode();
    }
    private void LoadItems()
    {
        allItems.Add(new MagicQuiver());
    }
    public List<Item> GetItemList()
    {
        return allItems;
    }
    public List<Item> GetItemListFiltered()
    {
        itemsFiltered = new List<Item>(allItems);
        itemsFiltered.RemoveAll(ability => ability.Code == _innateItem);
        return itemsFiltered;
    }
}
