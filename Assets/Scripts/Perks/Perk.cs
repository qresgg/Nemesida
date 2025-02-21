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
}
[Perk]
class Wizzardy : PerkBase
{
    private const string NAME = "Wizzardy";
    private const string DESCRIPTION = "This perk grants the player the ability to perform magical spells, increasing overall spell damage and mana regeneration.";
    public Wizzardy() : base(NAME, DESCRIPTION)
    {
    }

    public override void Bonus()
    {
    }
}

[Perk]
class Berzerk : PerkBase
{
    private const string NAME = "Berzerk";
    private const string DESCRIPTION = "3123123";
    public Berzerk() : base(NAME, DESCRIPTION)
    {
    }

    public override void Bonus()
    {
    }
}