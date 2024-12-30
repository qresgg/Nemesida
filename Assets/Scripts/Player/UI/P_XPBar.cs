using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class P_XPBar : MonoBehaviour
{
    [SerializeField] Slider xpSlider;
    void Start()
    {
        xpSlider.value = 0;
    }

    void Update()
    {
        
        
    }
    public void UpdateXP(float xp)
    {

        xpSlider.value = xp / 100;

    }
}
