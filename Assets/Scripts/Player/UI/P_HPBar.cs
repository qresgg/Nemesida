using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class P_HPBar : MonoBehaviour
{
    [SerializeField] Slider healthSlider;
    void Start()
    {
        healthSlider.value = 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateHealth(float health)
    {
        healthSlider.value = health / 100;

        Time.timeScale = (health == 0) ? 0 : 1;
    }
}
