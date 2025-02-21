using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityController : MonoBehaviour
{
    [SerializeField] GameObject _abilityCont6;
    private bool _checked = false;
    void Start()
    {
        _abilityCont6.SetActive(false);
    }
    void Update()
    {
        if (P_Stats.Instance.MaxAbilitiesPicked == true && _checked == false)
        {
            _abilityCont6.SetActive(true);
            _checked = true;
        }
    }
}
