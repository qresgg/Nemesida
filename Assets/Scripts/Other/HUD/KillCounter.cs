using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KillCounter : MonoBehaviour
{
    [SerializeField] TMP_Text KillCountText;
    void Start()
    {
        P_Stats.Instance.OnEnemyKilledChanged += UpdateKillCount;
    }
    void UpdateKillCount(int newCount)
    {
        KillCountText.text = newCount.ToString();
    }
}
