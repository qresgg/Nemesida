using UnityEngine;
using System;

public interface Ability
{
    string Name { get; } 
    string DamageType { get; }
    int DamageCount { get; }
    string Description { get; }
    //float Cooldown { get; }
    int ProjectileSpeed { get; }
    //int ProjectileCount { get; set; }
    //float Range { get; }
    //float Duration { get; }
    string Code { get; set;  }
    int Id { get; }
    public string IconPath { get; }
}

class Fireball : MonoBehaviour, Ability
{
    public string Name { get; } = "Fireball";
    public string DamageType { get; } = "Magical";
    public int DamageCount { get; } = 25;
    public string Description { get; } = "The player releases a fireball that automatically targets and damages the nearest enemy.";
    public float Cooldown { get; } = 2.5f;
    public int ProjectileSpeed { get; } = 10;
    public int ProjectileCount { get; set; } = 1;
    public float Range { get; } = 10f;
    public string Code { get; set;  } = "fireball";
    public int Id { get; } = 1;
    public string IconPath { get; } = "Images/UI/AbilityIcons/Fireball";
}

class OrbitalSpheres : MonoBehaviour, Ability
{
    public string Name { get; } = "Orbital Spheres";
    public string DamageType { get; } = "Magical";
    public int DamageCount { get; } = 15;
    public string Description { get; } = "SMysterious spheres orbit around the player, causing damage to any enemies that come into contact with them.";
    public float Cooldown { get; } = 5f;
    public int ProjectileSpeed { get; } = 5;
    public int ProjectileCount { get; set; } = 1;
    public float Radius { get; } = 2f;
    public float Duration { get; } = 5f;
    public string Code { get; set; } = "orbital_spheres";
    public int Id { get; } = 2;
    public string IconPath { get; } = "Images/UI/AbilityIcons/OrbitalSpheres";
}
class Whirligig : MonoBehaviour, Ability
{
    public string Name { get; } = "Whirligig";
    public string DamageType { get; } = "Physical";
    public int DamageCount { get; } = 50;
    public string Description { get; } = "The player surrounds themselves with a spinning sawblade, which damages enemies and pushes them away upon contact.";
    public float Cooldown { get; } = 3f;
    public int ProjectileSpeed { get; } = 0;
    public float Radius { get; } = 3f;
    public float Duration { get; } = 0.8f;
    public float PushForce { get; } = 15f;
    public float RecoveryTime { get;  } = 0.3f;
    public string Code { get; set; } = "whirligig";
    public int Id { get; } = 3;
    public string IconPath { get; } = "Images/UI/AbilityIcons/Whirligig";
}