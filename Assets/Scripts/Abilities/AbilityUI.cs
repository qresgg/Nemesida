using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEditor.Playables;

public class AbilityUI : MonoBehaviour
{
    [SerializeField] private TMP_Text IsNewAbility;
    [SerializeField] private Image abilitySprite;
    [SerializeField] private TMP_Text abilityName;
    [SerializeField] private TMP_Text abilityDescription;
    [SerializeField] private Button selectButton;

    private AbilityInventory _abilityInventory;
    private AbilityPickerMenu _abilityPickerMenu;
    private Player _player;
    private P_AbilityUser player_abilityUser;

    private void Start()
    {
        _abilityInventory = GameObject.Find("AbilityPanel").GetComponent<AbilityInventory>();
        _abilityPickerMenu = GameObject.Find("AbilityPickerMenu").GetComponent<AbilityPickerMenu>();
        player_abilityUser = GameObject.Find("P_AbilityUser").GetComponent<P_AbilityUser>();

        _player = GameObject.Find("Player").GetComponent<Player>();
        Debug.Log("ABILITYUI STARTED");
    }

    public void SetAbility(Ability ability)
    {
        if (ability == null)
        {
            DefaultSprite();

            abilityName.text = "DEF";
            abilityDescription.text = "DEF";
            IsNewAbility.text = "DEF";
            selectButton.onClick.RemoveAllListeners();
            selectButton.onClick.AddListener(() => GameManager.Instance.CloseAbilityPickerMenu());
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
        }
    }

    private void SelectAbility(Ability ability)
    {
        _abilityInventory.AddAbility(ability); // add to UI Inventory
        player_abilityUser.AddAbilityToActiveList(ability.Code); // add to player AbilityUser
        ability.UpgradeAbility(); // upgrade level

        _abilityPickerMenu.AddPickedAbility(GameManager.Instance.InnateAbilityCode);
        _abilityPickerMenu.AddPickedAbility(ability.Code);
        GameManager.Instance.CloseAbilityPickerMenu();
    }
    private void DefaultSprite()
    {
        this.abilitySprite.sprite = Resources.Load<Sprite>("Images/UI/AbilityIcons/default");
    }
}
