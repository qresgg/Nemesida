using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    [SerializeField] private string _innateAbilityCode;
    [SerializeField] AbilityPickerMenu _abilityPickerMenu;

    [Header("Game Changer")]
    [SerializeField] private int _damageMultiplier = 1;
    
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
        _abilityPickerMenu.SetActive(false);
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
    
    public string GetInnateAbilityCode()
    {
        return _innateAbilityCode;
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
    public int GetDamageMultiplier()
    {
        return _damageMultiplier;
    }
}
