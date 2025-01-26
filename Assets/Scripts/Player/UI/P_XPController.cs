using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class P_XPController : MonoBehaviour
{
    [SerializeField] TMP_Text m_XPLevel;
    [SerializeField] Slider xpSlider;

    Player _player;
    GameManager _gameManager;
    AbilityPickerMenu _abilityPickerMenu;

    [Header("XP Settings")]
    [SerializeField] float totalXP = 0;
    [SerializeField] int xp_level = 1;
    [SerializeField] int xp_multiplier;

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        XPLogic();
        XPChecker();
    }

    void XPLogic()
    {
        xpSlider.value = (totalXP % 100) / 100f;

        m_XPLevel.text = xp_level.ToString();
    }

    void XPChecker()
    {
        int new_level = Mathf.FloorToInt(totalXP / 100) + 1;
        if (new_level > xp_level)
        {
            int difference = new_level - xp_level;
            xp_level = new_level;
            for (int i = 0; i < difference; i++)
            {
                _gameManager.LevelUp();
            }
        }
    }
    public void TakeXP(int takenXP)
    {
        totalXP += takenXP;
    }

    public int GetXPMultiplier()
    {
        return xp_multiplier;
    }
}
