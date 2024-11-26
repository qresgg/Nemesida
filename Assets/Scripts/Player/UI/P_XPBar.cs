using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class P_XPBar : MonoBehaviour
{
    [SerializeField] Slider xpSlider;
    // Start is called before the first frame update
    void Start()
    {
        xpSlider.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateXP(float xp)
    {
        xpSlider.value = xp / 100;

        Time.timeScale = (xp == 0) ? 0 : 1;
    }
}
