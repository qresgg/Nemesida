using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class P_XPBar : MonoBehaviour
{
    [SerializeField] Slider xpSlider;
    P_XPLevel xpLevel;
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

        xpLevel = GameObject.Find("P_XPLVL").GetComponent<P_XPLevel>();
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
    }

    void XPChecker()
    {
        int new_level = Mathf.FloorToInt(totalXP / 100) + 1;
        if (new_level > xp_level)
        {
            xp_level = new_level;
        }
    }
    public int GetXPLevel()
    {
        return xp_level;
    }
}