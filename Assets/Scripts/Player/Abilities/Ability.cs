using UnityEngine;
using System;

public interface Ability
{
    string Name { get; } 
    string DamageType { get; }
    float DamageCount { get; }
    string Description { get; }
    float Cooldown { get; }
    int ProjectileCount { get; }
    float Range { get; }
    float Duration { get; }
    string Code { get; set;  }
    int Id { get; }
}

class ArcaneBolt : MonoBehaviour, Ability
{
    public string Name { get; } = "ArcaneBolt";
    public string DamageType { get; } = "Magical";
    public float DamageCount { get; } = 25f;
    public string Description { get; } = "Shoots a damaging projectile at the closest enemy.";
    public float Cooldown { get; } = 2.5f;
    public int ProjectileCount { get; } = 3;
    public float Range { get; } = 10f;
    public float Duration { get; } = 99f;
    public string Code { get; set;  } = "arcane_bolt";
    public int Id { get; } = 1;
}

class OrbitalSpirits : MonoBehaviour, Ability
{
    public string Name { get; } = "Orbital Spirits";
    public string DamageType { get; } = "Magical";
    public float DamageCount { get; } = 15f;
    public string Description { get; } = "Spirits are spinning around player.";
    public float Cooldown { get; } = 5f;
    public int ProjectileCount { get; } = 3;
    public float Range { get; } = 2f;
    public float Duration { get; } = 5f;
    public string Code { get; set; } = "orbital_spirits";
    public int Id { get; } = 2;
}
class Copy : MonoBehaviour, Ability
{
    public string Name { get; } = "12321";
    public string DamageType { get; } = "Magical";
    public float DamageCount { get; } = 15f;
    public string Description { get; } = "Spirits are spinning around player.";
    public float Cooldown { get; } = 5f;
    public int ProjectileCount { get; } = 3;
    public float Range { get; } = 2f;
    public float Duration { get; } = 5f;
    public string Code { get; set; } = "brit";
    public int Id { get; } = 3;
}