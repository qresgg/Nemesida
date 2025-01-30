using UnityEngine;
using System;

public interface Ability
{
    string Name { get; } 
    bool IsNewAbility { get; }
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
    string Info { get; }

    public void UpgradeAbility();
    public void SetNewAbility(bool value);
    AbilityLevel AbilityLevel { get; }
}

public class AbilityLevel
{
    public int Level { get; private set; }

    public AbilityLevel(int level)
    {
        Level = level;
    }

    public void LevelUp(Ability ability)
    {
        Level++;
        if (Level >= 2)
        {
            ability.SetNewAbility(false);
        }
    }
}

class Fireball : ScriptableObject, Ability
{
    public string Name { get; } = "Fireball";
    public bool IsNewAbility { get; private set; } = true;
    public string DamageType { get; } = "Magical";
    public int DamageCount { get; } = 25 * GameManager.Instance.GetDamageMultiplier();
    public string Description { get; } = "The player releases a fireball that automatically targets and damages the nearest enemy.";
    public float Cooldown { get; } = 2.5f;
    public int ProjectileSpeed { get; } = 10;
    public int ProjectileCount { get; set; } = 1;
    public float Range { get; } = 10f;
    public string Code { get; set;  } = "fireball";
    public int Id { get; } = 1;
    public string IconPath { get; } = "Images/UI/AbilityIcons/Fireball";
    public string Info => FormatInfo();
    private string FormatInfo()
    {
        return $"{DamageType} Damage: {DamageCount}\n" +
            $"Cooldown: {Cooldown}\n" +
            $"Projectile Count: {ProjectileCount}\n";
    }

    public void UpgradeAbility()
    {
        IsNewAbility = false;
        AbilityLevel.LevelUp(this);
        //UpdateAbilityStats();
    }
    public void SetNewAbility(bool value)
    {
        IsNewAbility = value;
    }

    public AbilityLevel AbilityLevel { get; private set; } = new AbilityLevel(1);

    /* private void UpdateAbilityStats()
    {
        switch (AbilityLevel.Level)
        {
            case 2:
                DamageCount = 60;
                Cooldown = 2.8f;
                Radius = 3.2f;
                Duration = 0.9f;
                break;
            case 3:
                DamageCount = 70;
                Cooldown = 2.6f;
                Radius = 3.4f;
                Duration = 1.0f;
                break;
            default:
                break;
        }
    }*/
}

class OrbitalSpheres : ScriptableObject, Ability
{
    public string Name { get; } = "Orbital Spheres";
    public bool IsNewAbility { get; private set; } = true;
    public string DamageType { get; } = "Magical";
    public int DamageCount { get; } = 15 * GameManager.Instance.GetDamageMultiplier();
    public string Description { get; } = "Orbital spheres orbit around the player, causing damage to any enemies that come into contact with them.";
    public float Cooldown { get; } = 5f;
    public int ProjectileSpeed { get; } = 5;
    public int ProjectileCount { get; set; } = 1;
    public float Radius { get; } = 2f;
    public float Duration { get; } = 5f;
    public string Code { get; set; } = "orbital_spheres";
    public int Id { get; } = 2;
    public string IconPath { get; } = "Images/UI/AbilityIcons/OrbitalSpheres";

    public string Info => FormatInfo();
    
    private string FormatInfo()
    {
        return $"{DamageType} Damage: {DamageCount}\n" +
            $"Cooldown: {Cooldown}\n" +
            $"Projectile Count: {ProjectileCount}\n" +
            $"Duration: {Duration}\n" +
            $"Radius: {Radius}"; 
    }

    public void UpgradeAbility()
    {
        IsNewAbility = false;
        AbilityLevel.LevelUp(this);
    }
    public void SetNewAbility(bool value)
    {
        IsNewAbility = value;
    }
    public AbilityLevel AbilityLevel { get; private set; } = new AbilityLevel(1);
}
class Whirligig : ScriptableObject, Ability
{
    public string Name { get; } = "Whirligig";
    public bool IsNewAbility { get; set; } = true;
    public string DamageType { get; } = "Physical";
    public int DamageCount { get; } = 50 * GameManager.Instance.GetDamageMultiplier();
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
    public string Info => FormatInfo();

    private string FormatInfo()
    {
        return $"{DamageType} Damage: {DamageCount}\n" +
            $"Cooldown: {Cooldown}\n" +
            $"Duration: {Duration}\n" +
            $"Push Force: {PushForce}\n" +
            $"Radius: {Radius}";
    }
    public void UpgradeAbility()
    {
        IsNewAbility = false;
        AbilityLevel.LevelUp(this);
    }
    public void SetNewAbility(bool value)
    {
        IsNewAbility = value;
    }

    public AbilityLevel AbilityLevel { get; private set; } = new AbilityLevel(1);
}

class RicochetStone : ScriptableObject, Ability
{
    public string Name { get; } = "Ricochet Stone";
    public bool IsNewAbility { get; set; } = true;
    public string DamageType { get; } = "Physical";
    public int DamageCount { get; } = 20 * GameManager.Instance.GetDamageMultiplier();
    public string Description { get; } = "A powerful stone that bounces off surfaces, striking multiple enemies in its path. Use it strategically to hit several targets with a single throw.";
    public float Cooldown { get; } = 2.8f;
    public int ProjectileSpeed { get; } = 10;
    public float Range { get; } = 10f;
    public string Code { get; set; } = "ricochet_stone";
    public int Id { get; } = 4;
    public string IconPath { get; } = "Images/UI/AbilityIcons/RicochetStone";
    public string Info => FormatInfo();

    private string FormatInfo()
    {
        return $"{DamageType} Damage: {DamageCount}\n" +
            $"Cooldown: {Cooldown}\n" +
            $"Projectile Speed: {ProjectileSpeed}\n" +
            $"Range: {Range}";
    }
    public void UpgradeAbility()
    {
        IsNewAbility = false;
        AbilityLevel.LevelUp(this);
    }
    public void SetNewAbility(bool value)
    {
        IsNewAbility = value;
    }

    public AbilityLevel AbilityLevel { get; private set; } = new AbilityLevel(1);
}