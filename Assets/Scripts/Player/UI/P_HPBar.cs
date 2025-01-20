using UnityEngine;
using UnityEngine.UI;

public class P_HPBar : MonoBehaviour
{
    [SerializeField] Slider healthSlider;

    P_HPController _hpController;
    Player _player;

    float maxHealth = 0;
    float health;
    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();

        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
    }
    public void UpdateHealth(float health)
    {
        healthSlider.value = health;

    }
}
