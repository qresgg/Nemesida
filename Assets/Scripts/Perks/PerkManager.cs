using System;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System.Linq;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PerkManager : MonoBehaviour
{
    public static PerkManager Instance { get; private set; }
    private List<Perk> allPerks = new List<Perk>();
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
    private void LoadPerks()
    {
        var perkTypes = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => t.GetCustomAttributes(typeof(PerkAttribute), false).Length > 0);

        foreach (var type in perkTypes)
        {
            var perkInstance = Activator.CreateInstance(type) as Perk;
            if (perkInstance != null)
            {
                allPerks.Add(perkInstance);
            }
        }
    }
    public List<Perk> GetPerkList()
    {
        return allPerks;
    }
}
