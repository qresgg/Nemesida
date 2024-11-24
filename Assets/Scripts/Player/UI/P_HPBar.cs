using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class P_HPBar : MonoBehaviour
{
    [SerializeField] Slider healthSlider;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateHealth(float health)
    {
        healthSlider.value = health / 100;
        Debug.Log("Updated Slider Health to: " + health);

        Time.timeScale = (health == 0) ? 0 : 1;
    }
}
