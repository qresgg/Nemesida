using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class AbilityUI : MonoBehaviour
{
    [SerializeField] private TMP_Text IsNewAbility;
    [SerializeField] private Image abilitySprite;
    [SerializeField] private TMP_Text abilityName;
    [SerializeField] private TMP_Text abilityDescription;
    [SerializeField] private Button selectButton;

    private AbilityInventory _abilityInventory;
    private AbilityPickerMenu _abilityPickerMenu;
    private GameManager _gameManager;
    private Player _player;
    private P_AbilityUser player_abilityUser;

    private void Start()
    {
        _abilityInventory = GameObject.Find("AbilityInventory").GetComponent<AbilityInventory>();
        _abilityPickerMenu = GameObject.Find("AbilityPickerMenu").GetComponent<AbilityPickerMenu>();
        player_abilityUser = GameObject.Find("P_AbilityUser").GetComponent<P_AbilityUser>();

        _player = GameObject.Find("Player").GetComponent<Player>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Debug.Log("ABILITYUI STARTED");
    }

    public void SetAbility(Ability ability)
    {
        if (ability == null)
        {
            DefaultSprite();

            abilityName.text = "DEF";
            abilityDescription.text = "DEF";
            selectButton.onClick.RemoveAllListeners();
            selectButton.onClick.AddListener(() => CloseAbilityPickerMenu());
        }
        else
        {
            IsNewAbility.text = ability.IsNewAbility ? "new ability!" : $"Level: {ability.AbilityLevel.Level}";
            abilitySprite.sprite = Resources.Load<Sprite>(ability.IconPath);

            abilityName.text = ability.Name;
            abilityDescription.text = ability.Info;

            selectButton.onClick.RemoveAllListeners();
            selectButton.onClick.AddListener(() => SelectAbility(ability));

            Debug.Log(ability.Code);
            //_abilitiesDisplayed.Add(ability);
        }
    }

    private void SelectAbility(Ability ability)
    {
        _abilityInventory.AddAbility(ability); // add to UI Inventory
        player_abilityUser.AddAbilityToActiveList(ability.Code); // add to player AbilityUser
        ability.UpgradeAbility(); // upgrade level

        CloseAbilityPickerMenu();

        Debug.Log($"Гравець обрав здатність: {ability.Name}");
        if(ability.AbilityLevel.Level <= 5)
        {
            _abilityPickerMenu.AddPickedAbility(ability.Code);
        }
    }

    private void CloseAbilityPickerMenu()
    {
        _abilityPickerMenu.SetActive(false);
        _gameManager.ResumeGame();
    }
    private void DefaultSprite()
    {
        this.abilitySprite.sprite = Resources.Load<Sprite>("Images/UI/AbilityIcons/default");
    }
}
