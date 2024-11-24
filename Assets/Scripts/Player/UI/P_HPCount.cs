using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class P_HPCount : MonoBehaviour
{
    [SerializeField] TMP_Text m_healthCount;
    P_HPBar player_HPBar;
    // Start is called before the first frame update
    void Start()
    {
        player_HPBar = GameObject.Find("P_HPBar").GetComponent<P_HPBar>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateHealth(float health)
    {
        m_healthCount.text = health.ToString(); 
    }
}
