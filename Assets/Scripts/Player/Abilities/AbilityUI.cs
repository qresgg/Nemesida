    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;
    using System.Collections.Generic;

    public class AbilityUI : MonoBehaviour
    {
        [SerializeField] private Image abilitySprite;
        [SerializeField] private TMP_Text abilityName;
        [SerializeField] private TMP_Text abilityDescription;
        [SerializeField] private Button selectButton;

        private AbilityInventory _abilityInventory;
        private AbilityPickerMenu _abilityPickerMenu;
        private GameManager _gameManager;

        Ability m_ability;

        private void Start()
        {
            _abilityInventory = GameObject.Find("AbilityInventory").GetComponent<AbilityInventory>();
            _abilityPickerMenu = GameObject.Find("AbilityPickerMenu").GetComponent<AbilityPickerMenu>();
            _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }

    public void SetAbility(Ability ability)
    {
        m_ability = ability;

        Sprite sprite = Resources.Load<Sprite>(ability.IconPath);

        if (sprite == null)
        {
            Debug.LogError($"������ �� �������� �� ������: {ability.IconPath}");
        }
        else
        {
            abilitySprite.sprite = sprite;
        }

        abilityName.text = ability.Name;
        abilityDescription.text = ability.Description;

        selectButton.onClick.RemoveAllListeners();
        selectButton.onClick.AddListener(() => SelectAbility(ability));
    }


    private void SelectAbility(Ability ability)
        {
            _abilityInventory.AddAbility(ability.Id);
            _abilityPickerMenu.SetActive(false);
            _gameManager.ResumeGame();

            Debug.Log($"Player selected ability: {ability.Name}");
            _abilityPickerMenu.AddPickedAbility(ability.Code);
        }
    }
