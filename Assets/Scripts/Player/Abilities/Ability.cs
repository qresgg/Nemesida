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
    public string IconPath { get; }
}

class Fireball : MonoBehaviour, Ability
{
    public string Name { get; } = "Fireball";
    public string DamageType { get; } = "Magical";
    public float DamageCount { get; } = 25f;
    public string Description { get; } = "Shoots a damaging fireball at the closest enemy.";
    public float Cooldown { get; } = 2.5f;
    public int ProjectileCount { get; } = 3;
    public float Range { get; } = 10f;
    public float Duration { get; } = 99f;
    public string Code { get; set;  } = "fireball";
    public int Id { get; } = 1;
    public string IconPath { get; } = "Images/UI/AbilityIcons/Fireball";
}

class OrbitalSpheres : MonoBehaviour, Ability
{
    public string Name { get; } = "Orbital Spheres";
    public string DamageType { get; } = "Magical";
    public float DamageCount { get; } = 15f;
    public string Description { get; } = "Spheres are spinning around player.";
    public float Cooldown { get; } = 5f;
    public int ProjectileCount { get; } = 3;
    public float Range { get; } = 2f;
    public float Duration { get; } = 5f;
    public string Code { get; set; } = "orbital_spheres";
    public int Id { get; } = 2;
    public string IconPath { get; } = "Images/UI/AbilityIcons/OrbitalSpheres";
}
class Whirligig : MonoBehaviour, Ability
{
    public string Name { get; } = "Whirligig";
    public string DamageType { get; } = "Physical";
    public float DamageCount { get; } = 15f;
    public string Description { get; } = "Uses whirligig around player";
    public float Cooldown { get; } = 5f;
    public int ProjectileCount { get; } = 3;
    public float Range { get; } = 2f;
    public float Duration { get; } = 5f;
    public string Code { get; set; } = "whirligig";
    public int Id { get; } = 3;
    public string IconPath { get; } = "Images/UI/AbilityIcons/Whirligig";
}