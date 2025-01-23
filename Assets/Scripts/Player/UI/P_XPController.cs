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

    float totalXP = 0;
    int xp_level = 1;

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _abilityPickerMenu = GameObject.Find("AbilityPickerMenu").GetComponent<AbilityPickerMenu>();
    }

    void Update()
    {
        XPLogic();
        XPChecker();
    }

    void XPLogic()
    {
        totalXP = _player.GetXP();
        xpSlider.value = (totalXP % 100) / 100f;

        m_XPLevel.text = xp_level.ToString();
    }

    void XPChecker()
    {
        int new_level = Mathf.FloorToInt(totalXP / 100) + 1;
        if (new_level > xp_level)
        {
            xp_level = new_level;
        }
    }
}
