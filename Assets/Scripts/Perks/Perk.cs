using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
sealed class PerkAttribute : Attribute
{
    public PerkAttribute() { }
}

public interface Perk
{
    string Name { get; }
    string Description { get; }
    string Code { get; }
    int Id { get; }
    string IconPath { get; }
    void Bonus();
}

public abstract class PerkBase : ScriptableObject, Perk
{
    private static int nextId = 1;

    private string naming;
    private string description;
    private int id;
    private string iconPath;

    public PerkBase(string name, string description)
    {
        naming = name;
        this.description = description;
        id = nextId++;
        iconPath = $"Images/UI/Perks/{name.ToLower()}";
    }

    public string Name
    {
        get => naming;
    }
    public string Description
    {
        get => description;
    }
    public string Code
    {
        get => naming.ToLower();
    }
    public int Id
    {
        get => id;
    }
    public string IconPath
    {
        get => iconPath;
    }

    public virtual void Bonus()
    {
        // Override in derived classes
    }
    public virtual void FormatInfo()
    {

    }
}
[Perk]
class Wizzardy : PerkBase
{
    private const string NAME = "Wizzardy";
    private const string DESCRIPTION = "This perk grants the player the ability to perform magical spells, increasing overall spell damage and mana regeneration.";
    public Wizzardy() : base(NAME, DESCRIPTION)
    {
    }
    private float MagicalDamageAmplifier = 0.15f;
    private int ProjectileAdditionalCount = 1;
    public override void Bonus()
    {
        P_Stats.Instance.MagicDamageAmplifier += MagicalDamageAmplifier;
        P_Stats.Instance.ProjectileCount += ProjectileAdditionalCount;
    }
}

[Perk]
class Berzerk : PerkBase
{
    private const string NAME = "Berzerk";
    private const string DESCRIPTION = "Unleash the chaos within. Empowered by an untamed force, Berzerk grants you 10% additional physical damage and a 25% boost to critical hits, turning the battlefield into your playground.";
    public Berzerk() : base(NAME, DESCRIPTION)
    {
    }
    private float PhysicalDamageAmplifier = 0.10f;
    private float CriticalDamageAmplifier = 0.25f;
    private float CriticalChange = 0.25f;
    public override void Bonus()
    {
        P_Stats.Instance.PhysicDamageAmplifier += PhysicalDamageAmplifier;
        P_Stats.Instance.CriticalChance += CriticalChange;
        P_Stats.Instance.CriticalDamageAmplifier += CriticalDamageAmplifier;
    }
}

[Perk]
class Mugger : PerkBase
{
    private const string NAME = "Mugger";
    private const string DESCRIPTION = "Grants character additional experience points and increased speed. With this perk, characters become more agile and can accumulate experience faster, making them more formidable and efficient in their tasks.";
    public Mugger() : base(NAME, DESCRIPTION)
    {
    }
    private float XPAmplifier = 0.15f;
    private float AdditionalPlayerSpeed = 0.10f;
    public override void Bonus()
    {
        P_Stats.Instance.XPAmplifier += XPAmplifier;
        P_Stats.Instance.SpeedAmplifier += AdditionalPlayerSpeed;
    }
}
    