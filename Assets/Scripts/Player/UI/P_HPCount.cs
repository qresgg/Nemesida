using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class P_HPCount : MonoBehaviour
{
    [SerializeField] TMP_Text m_healthCount;
    [SerializeField] TMP_Text m_healthRegen;
    P_HPController _hpController;

    float maxHealth = 0;
    private void Start()
    {
        _hpController = GameObject.Find("HP").GetComponent<P_HPController>();
        maxHealth = _hpController.GetMaxHP();

        m_healthCount.text = maxHealth + " / " + maxHealth;
    }
    public void UpdateHealth(float health)
    {
        m_healthCount.text = health.ToString() + " / " + maxHealth; 
    }
}
