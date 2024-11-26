using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class P_XPLevel : MonoBehaviour
{
    [SerializeField] TMP_Text m_XPLevel;
    P_XPBar player_HPBar;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    public void UpdateXPLevel(int xp_level)
    {
        m_XPLevel.text = xp_level.ToString();
    }
}
