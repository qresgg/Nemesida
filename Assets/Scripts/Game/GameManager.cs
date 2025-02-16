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
    [SerializeField] private string _innateAbilityCode;
    [SerializeField] private string _personalItemCode;
    [SerializeField] private int _damageMultiplier = 1; // x1
    [SerializeField] private bool _IsInvulnerability = false;
    [SerializeField] private int _xpMultiplier = 1; // x1
    [SerializeField] private int _maxEnemyCount = 5;

    [SerializeField] public bool allowedToSynchronize = false;

    private int _maxAbilitiesCount = 5;

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
        StartCoroutine(SynchronizeData());
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


    private IEnumerator SynchronizeData()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            allowedToSynchronize = true;
            Debug.Log("SYNC = TRUE");
            yield return new WaitForFixedUpdate();
            allowedToSynchronize = false;
        }
    }


    public int GetMaxAbilitiesCount()
    {
        return _maxAbilitiesCount;
    }





    public string GetPersonalItemCode()
    {
        return _personalItemCode;
    }
    public string GetInnateAbilityCode()
    {
        return _innateAbilityCode;
    }
    public int GetDamageMultiplier()
    {
        return _damageMultiplier;
    }
    public int GetXPMultiplier()
    {
        return _xpMultiplier;
    }
    public int GetMaxEnemyCount()
    {
        return _maxEnemyCount;
    }
    public bool GetIsAllowedToSync()
    {
        return allowedToSynchronize;
    }
}
