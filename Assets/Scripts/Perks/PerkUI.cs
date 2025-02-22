using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Playables;
using UnityEngine;
using UnityEngine.UI;

public class PerkUI : MonoBehaviour
{
    private List<Perk> allPerks = new List<Perk>();
    [SerializeField] Image _perkImage;
    private string pickedPerk;

    void Start()
    {
        allPerks = PerkManager.Instance.GetPerkList();
        pickedPerk = GameManager.Instance.PerkCode;

        SetPerk(FindPerkByCode(pickedPerk));
    }

    void SetPerk(Perk perk)
    {
        _perkImage.sprite = Resources.Load<Sprite>(perk.IconPath);
        perk.Bonus();
    }

    private Perk FindPerkByCode(string code)
    {
        foreach (Perk perk in allPerks)
        {
            if (perk.Code == code)
            {
                return perk;
            }
        }
        return null;
    }
}
