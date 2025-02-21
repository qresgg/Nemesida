using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    [SerializeField] AbilityPickerMenu _abilityPickerMenu;
    [SerializeField] ItemPickerMenu _itemPickerMenu;

    [Header("Game Changer")]
    [SerializeField] private string _perkCode;
    [SerializeField] private string _innateAbilityCode;
    [SerializeField] private string _personalItemCode;
    [SerializeField] private int _damageMultiplier = 1; // x1
    [SerializeField] private bool _IsInvulnerability = false;
    [SerializeField] private int _xpMultiplier = 1; // x1
    [SerializeField] private int _maxEnemyCount = 5;

    public bool _abilityPickerMenuOpened = false;

    private Queue<System.Action> abilityPickerQueue = new Queue<Action>();
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Start()
    {
    }

    private void Update()
    {
        _abilityPickerMenuOpened = GameObject.Find("AbilityPickerMenu") != null;

        if (abilityPickerQueue.Count > 0 && !_abilityPickerMenuOpened)
        {
            Action nextAbilityPicker = abilityPickerQueue.Dequeue();
            nextAbilityPicker();
        }
    }
    public void LevelUp()
    {
        abilityPickerQueue.Enqueue(() => OpenAbilityPickerMenu());
    }

    public void PauseGame()
    {
        Debug.Log("Game paused.");
        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        Debug.Log("Game resumed.");
        Time.timeScale = 1;
    }

    public void OpenAbilityPickerMenu()
    {
        _abilityPickerMenu.SetActive(true);
        PauseGame();
    }
    public void CloseAbilityPickerMenu()
    {
        _abilityPickerMenu.SetActive(false);
        ResumeGame();
    }
    public void OpenItemPickerMenu()
    {
        _itemPickerMenu.SetActive(true);
        PauseGame();
    }
    public void CloseItemPickerMenu()
    {
        _itemPickerMenu.SetActive(false);
        ResumeGame();
    }


    public string PersonalItemCode
    {
        get => _personalItemCode;
    }
    public string InnateAbilityCode
    {
        get => _innateAbilityCode;
    }
    public string PerkCode
    {
        get => _perkCode;
    }
    public int DamageMultiplier
    {
        get => _damageMultiplier;
    }
    public int XPMultiplier
    {
        get => _xpMultiplier;
    }
    public int MaxEnemyCount
    {
        get => _maxEnemyCount;
    }
}
