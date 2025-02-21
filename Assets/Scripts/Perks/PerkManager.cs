using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PerkManager : MonoBehaviour
{
    public static PerkManager Instance { get; private set; }
    private List<Perk> allPerks = new List<Perk>();
    private List<Perk> abilitiesFiltered = new List<Perk>();
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadPerks();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
    }
    private void LoadPerks()
    {
        allPerks.Add(new Prophecy());
    }
    public List<Perk> GetPerkList()
    {
        return allPerks;
    }
}
