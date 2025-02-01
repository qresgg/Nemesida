using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using System.Collections.Generic;

public class AbilityUICooldowns : MonoBehaviour
{
    [SerializeField] private Image[] cooldownSlot;
    [SerializeField] private TMP_Text[] cooldownText;

    Dictionary<string, int> abilitiesDictionary = new Dictionary<string, int>();

    Player _player;
    AbilityInventory _abilityInventory;

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _abilityInventory = GameObject.Find("AbilityPanel").GetComponent<AbilityInventory>();
        

        CooldownClear();
    }
    private void CooldownClear()
    {
        for (int i = 0; i < cooldownSlot.Length; i++)
        {
            cooldownSlot[i].enabled = false;
            cooldownText[i].text = "";
        }
    }

    public void SetCooldown(int index, float cooldownTime, string code)
    {
        if (index >= 0 && index < cooldownSlot.Length)
        {
            cooldownSlot[index].enabled = true;
            StartCoroutine(CooldownTimer(index, cooldownTime, code));
        }
    }

    private IEnumerator CooldownTimer(int index, float cooldownTime, string code)
    {
        float remainingTime = cooldownTime;

        if (code == "plasma_spheres")
        {
            cooldownSlot[index].enabled = false;
            yield return new WaitForSeconds(cooldownTime);
            cooldownSlot[index].enabled = true;
        }
        while (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            cooldownText[index].text = remainingTime.ToString("F1") + "s";
            yield return null;
        }

        cooldownSlot[index].enabled = false;
        cooldownText[index].text = "";
    }

}