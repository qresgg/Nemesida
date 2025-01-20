using TMPro;
using UnityEngine;

public class P_XPLevel : MonoBehaviour
{
    [SerializeField] TMP_Text m_XPLevel;
    P_XPBar xpBar;

    int xp_level = 1;

    void Start()
    {
        xpBar = GameObject.Find("P_XPBar").GetComponent<P_XPBar>();
    }

    private void Update()
    {
        xp_level = xpBar.GetXPLevel();
        m_XPLevel.text = xp_level.ToString();
    }
}
