using System.Collections.Generic;
using UnityEngine;

public class ItemPickerMenu : MonoBehaviour
{
    public static ItemPickerMenu Instance { get; private set; }

    private List<Item> items = new List<Item>();

    [SerializeField] private AbilityUI[] abilitySlots;

    private string _innateItem;

    Player _player;
    ItemManager _ItemManager;

    [SerializeField] private bool isFirstCall = true;

    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _ItemManager = GameObject.Find("ItemManager").GetComponent<ItemManager>();

        _innateItem = GameManager.Instance.GetInnateAbilityCode();
        items = _ItemManager.GetItemListFiltered();

        ShuffleAbilities();
        DisplayAbilities();
    }

    public void ShuffleAbilities()
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

    public void DisplayAbilities()
    {
        int slotIndex = 0;
        for (int i = 0; i < items.Count && slotIndex < abilitySlots.Length; i++)
        {
            if (items[i].ItemLevel.Level != 4) //|| !pickedAbilityCodes.Contains(abilities[i].Code))
            {
                //itemSlots[slotIndex].SetAbility(items[i]);
                slotIndex++;
            }
        }
        for (int i = slotIndex; i < abilitySlots.Length; i++)
        {
            abilitySlots[i].SetAbility(null);
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
            ShuffleAbilities();
            DisplayAbilities();
        }
    }
}
