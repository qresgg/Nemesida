using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilityUI : MonoBehaviour
{
    [SerializeField] private Image abilityImage;
    [SerializeField] private TMP_Text abilityName;
    [SerializeField] private TMP_Text abilityDescription;
    [SerializeField] private Button selectButton;

    private Ability ability;

    public void SetAbility(Ability ability)
    {
        if (ability == null)
        {
            Debug.LogWarning("Ability is null!");
            return;
        }

        this.ability = ability;
        abilityName.text = ability.abilityName;
        abilityDescription.text = ability.abilityDescription;

        selectButton.onClick.RemoveAllListeners();
        selectButton.onClick.AddListener(SelectAbility);
    }

    private void SelectAbility()
    {
        Debug.Log($"Ability {ability.abilityName} selected!");
    }
}
