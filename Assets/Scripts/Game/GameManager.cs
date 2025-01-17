using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private string _innateAbilityCode;
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
}
