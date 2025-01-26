using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private string _innateAbilityCode;
    [SerializeField] AbilityPickerMenu _abilityPickerMenu;
    public bool _abilityPickerMenuOpened = false;

    private Queue<System.Action> abilityPickerQueue = new Queue<Action>();

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
}
