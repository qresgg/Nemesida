using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Perk
{
    string Name { get; }
    string Description { get; }
    string Code { get; }
    int Id { get; }
    string IconPath { get; }
    void Bonus();
}

class Prophecy : ScriptableObject, Perk
{
    public string Name { get; } = "Prophecy";
    public string Description { get; } = "213123";
    public string Code { get; } = "prophecy";
    public int Id { get; } = 1;
    public string IconPath { get; } = "Images/UI/Perks/Prophecy";
    public void Bonus()
    {

    }
}