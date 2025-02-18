using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using System.Collections.Generic;

public class AbilityUICooldowns : MonoBehaviour
{
    [SerializeField] private GameObject[] objects;
    private string CooldownsObjectName = "Cooldowns";
    private string cdObjectName = "CD";
    private string imageObjectName = "image";
    private Image[] cooldownSlot;
    private TMP_Text[] cooldownText;

    Dictionary<string, int> abilitiesDictionary = new Dictionary<string, int>();

    Player _player;
    AbilityInventory _abilityInventory;

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _abilityInventory = GameObject.Find("AbilityPanel").GetComponent<AbilityInventory>();

        FindCooldownComponents();
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
    private void FindCooldownComponents()
    {
        List<Image> images = new List<Image>();
        List<TMP_Text> texts = new List<TMP_Text>();

        foreach (GameObject obj in objects)
        {
            Transform CooldownsTransform = obj.transform.Find(CooldownsObjectName);
            Transform cdTransform = obj.transform.Find(cdObjectName);

            if (CooldownsTransform != null)
            {
                Image image = CooldownsTransform.GetComponent<Image>();
                if (image != null)
                {
                    images.Add(image);
                }
            }

            if (cdTransform != null)
            {
                TMP_Text text = cdTransform.GetComponent<TMP_Text>();
                if (text != null)
                {
                    texts.Add(text);
                }
            }
        }

        cooldownSlot = images.ToArray();
        cooldownText = texts.ToArray();
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
