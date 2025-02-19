using System;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Playables;
using UnityEngine;
using UnityEngine.UI;

public class AbilityInventory : MonoBehaviour
{
    private List<Ability> abilitiesList = new List<Ability>();
    private string _innateAbility;

    List<Ability> activeAbilities = new List<Ability>();

    Dictionary<string, int> abilitiesDictionary = new Dictionary<string, int>();

    Player _player;
    AbilityManager _abilityManager;
    AbilityUICooldowns _abilityUICooldowns;
    P_AbilityUser player_abilityUser;

    [SerializeField] private GameObject[] objects;
    private string imageObjectName = "Image";
    private string abilityLVLObjectName = "AbilityLVL";
    private Image[] imageSlots;
    private TMP_Text[] abilityLvlSlots;

    void Start()
    {
        FindCooldownComponents();
        _player = GameObject.Find("Player").GetComponent<Player>();
        _abilityManager = GameObject.Find("AbilityManager").GetComponent<AbilityManager>();
        _abilityUICooldowns = GameObject.Find("UICooldowns").GetComponent<AbilityUICooldowns>();
        player_abilityUser = GameObject.Find("P_AbilityUser").GetComponent<P_AbilityUser>();

        abilitiesList = _abilityManager.GetAbilityList();
        InnateAbilityBookSlot();
    }
    private void Update()
    {
        AbilitySlotsInitializer();
    }
    public void AddAbility(Ability ability)
    {
        if (!abilitiesDictionary.ContainsKey(ability.Code))
        {
            for (int i = 0; i < P_Stats.Instance.MaxAbilitiesCount; i++)
            {
                if (imageSlots[i].sprite == null)
                {
                    Sprite sprite = Resources.Load<Sprite>(ability.IconPath);
                    if (sprite != null)
                    {
                        Color newColor = imageSlots[i].color;
                        newColor.a = 0;

                        imageSlots[i].color = newColor;
                        imageSlots[i].sprite = sprite;
                        abilityLvlSlots[i].color = GetColorByLevel(ability.AbilityLevel.Level);
                        abilityLvlSlots[i].text = ability.AbilityLevel.Level.ToString();

                        abilitiesDictionary.Add(ability.Code, i);
                        player_abilityUser.SetAbilityDictionary(abilitiesDictionary);
                    }
                    return;
                }
            }
            //Debug.Log("No empty slots available");
        } else
        {
            //Debug.Log("CONTAINS");  
            UpdateAbilityLevel(ability);
        }
    }

    private void UpdateAbilityLevel(Ability ability)
    {
        if (abilitiesDictionary.TryGetValue(ability.Code, out int slotIndex))
        {
            abilityLvlSlots[slotIndex].text = ability.AbilityLevel.Level.ToString();
            abilityLvlSlots[slotIndex].color = GetColorByLevel(ability.AbilityLevel.Level);
        }
    }
    private Color GetColorByLevel(int level)
    {
        switch (level)
        {
            case 2:
            case 3: return HexToColor("#7eff00");
            case 4:
            case 5: return HexToColor("#ffba00");
            case 6: return HexToColor("#ff2400");
            case 7: return HexToColor("#500b0b");
            default: return Color.white;
        }
    }
    private Color HexToColor(string hex)
    {
        if (ColorUtility.TryParseHtmlString(hex, out Color color))
        {
            return color;
        }
        return Color.white;
    }
    private void FindCooldownComponents()
    {
        List<Image> images = new List<Image>();
        List<TMP_Text> abilityLvls = new List<TMP_Text>();

        foreach (GameObject obj in objects)
        {
            Transform imageTransform = obj.transform.Find(imageObjectName);
            Transform abilityLvlTransform = obj.transform.Find(abilityLVLObjectName);

            if (imageTransform != null)
            {
                Image image = imageTransform.GetComponent<Image>();
                if (image != null)
                {
                    images.Add(image);
                }
            }

            if (abilityLvlTransform != null)
            {
                TMP_Text text = abilityLvlTransform.GetComponent<TMP_Text>();
                if (text != null)
                {
                    abilityLvls.Add(text);
                }
            }
        }

        imageSlots = images.ToArray();
        abilityLvlSlots = abilityLvls.ToArray();
    }

    private Sprite GetSpriteByAbilityCode(string abilityCode)
    {
        foreach (Ability ability in abilitiesList)
        {
            if (ability.Code == abilityCode)
            {
                //Debug.Log(ability.IconPath);
                return Resources.Load<Sprite>(ability.IconPath);
            }
        }
        return null;
    }
    private void InnateAbilityBookSlot()
    {
        _innateAbility = GameManager.Instance.InnateAbilityCode;
        Sprite sprite = GetSpriteByAbilityCode(_innateAbility);
        if (sprite != null)
        {
            imageSlots[0].sprite = sprite;
            abilityLvlSlots[0].text = "1";
            abilitiesDictionary.Add(_innateAbility, 0);
        }
    }
    private void AbilitySlotsInitializer()
    {
        foreach (Image img in imageSlots)
        {
            Color newColor = img.color;
            if (img.sprite == null)
            {
                newColor.a = 0;
                img.color = newColor;
            }
            else
            {
                newColor.a = 1;
                img.color = newColor;
            }
        }
    }
}
