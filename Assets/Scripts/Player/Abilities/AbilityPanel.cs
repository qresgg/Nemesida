using UnityEngine;
using UnityEngine.UI;

public class AbilityInventory : MonoBehaviour
{
    [SerializeField] private Image[] slots;

    void Start()
    {
    }

    public void AddAbility(Ability ability)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].sprite == null)
            {
                Sprite sprite = Resources.Load<Sprite>(ability.IconPath);
                if (sprite != null)
                {
                    slots[i].sprite = sprite;
                }
                return;
            }
        }
        Debug.Log("No empty slots available");
    }
}
