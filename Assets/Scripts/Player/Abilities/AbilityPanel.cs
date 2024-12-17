using UnityEngine;
using UnityEngine.UI;

public class AbilityInventory : MonoBehaviour
{
    [SerializeField] private Sprite[] abilityImages; 
    [SerializeField] private Image[] slots;

    void Start()
    {
    }

    public void AddAbility(int abilityIndex)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].sprite == null)
            {
                slots[i].sprite = abilityImages[abilityIndex - 1];
                Debug.Log($"Ability image added to slot {i + 1}");
                return;
            }
        }
        Debug.Log("No empty slots available");
    }
}
