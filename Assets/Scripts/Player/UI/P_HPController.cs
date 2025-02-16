using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class P_HPController : MonoBehaviour
{
    private float _maxHealth;
    private float _currentHealth;
    [SerializeField] private float _healthRegeneration = 0.1f;

    [SerializeField] TMP_Text m_healthCount; // HP
    [SerializeField] TMP_Text m_healthRegen; // HPREGEN
    [SerializeField] Slider m_healthSlider; // HPBAR

    Player _player;

    private void Start()
    {
        _maxHealth = P_Stats.Instance.MaxHealthPoints;
        _currentHealth = _maxHealth;
        _player = GameObject.Find("Player").GetComponent<Player>();
        m_healthSlider = GameObject.Find("SliderHPBar").GetComponent<Slider>();

        Setup();
    }
    private void Update()
    {
        HealthRegeneration();
    }
    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        UpdateHealth();
        if (_currentHealth <= 0)
        {
            _player.Death();
        }
    }
    private void HealthRegeneration()
    {
        if (_currentHealth < _maxHealth)
        {
            _currentHealth += _healthRegeneration * Time.deltaTime;
            if (_currentHealth > _maxHealth)
            {
                _currentHealth = _maxHealth;
            }
            UpdateHealth();
        }
    }
    private void Setup()
    {
        _currentHealth = _maxHealth;
        m_healthCount.text = _maxHealth + " / " + _maxHealth;

        m_healthRegen.text = "+ " + _healthRegeneration.ToString();

        m_healthSlider.maxValue = _maxHealth;
        m_healthSlider.minValue = 0;
        m_healthSlider.value = _currentHealth;
    }
    private void UpdateHealth()
    {
        float roundedCurrentHealth = (float)Math.Round(_currentHealth, 0);
        m_healthCount.text = roundedCurrentHealth.ToString() + " / " + _maxHealth;
        m_healthSlider.value = _currentHealth;
    }
    public float GetMaxHP()
    {
        return _maxHealth;
    }
    public float GetCurrentHP()
    {
        return _currentHealth;
    }
}
