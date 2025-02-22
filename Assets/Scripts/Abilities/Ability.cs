using UnityEngine;
using System;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
sealed class AbilityAttribute : Attribute
{
    public AbilityAttribute() { }
}
public interface Ability
{
    string Name { get; } 
    bool IsNewAbility { get; }
    string DamageType { get; }
    float DamageCount { get; }
    string Description { get; }
    //float Cooldown { get; }
    //int ProjectileSpeed { get; }
    //int ProjectileCount { get; set; }
    //float Range { get; }
    //float Duration { get; }
    string Code { get; }
    int Id { get; }
    string IconPath { get; }
    string Info { get; }

    void UpgradeAbility();
    void SetNewAbility(bool value);
    string FormatInfo();
    AbilityLevel AbilityLevel { get; }
}
public enum DamageType
{
    Physical,
    Magical
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

public abstract class AbilityBase : ScriptableObject, Ability
{
    private static int nextId = 1;

    private string naming;
    private string description;
    private DamageType damage_type;
    private float damage_count;
    private float cooldown;
    private bool is_new_ability = true;
    private int id;
    private string icon_path;
    private string info;

    public AbilityBase(string name, string description, DamageType damage_type, float damage_count, float cooldown)
    {
        naming = name;
        this.description = description;
        this.damage_type = damage_type;
        this.damage_count = damage_count;
        id = nextId++;
        icon_path = $"Images/UI/AbilityIcons/{name.Replace(" ", "")}";
        this.cooldown = cooldown;
    }
    // NAME, ISNEW, DAMAGETYPE, DAMAGECOUNT, DESCRIPTION, CODE, ID, ICONPATH, INFO, UPGRADE, SETNEWAB
    public string Name => naming;
    public bool IsNewAbility { get; set; }
    public string DamageType => damage_type.ToString();
    public float DamageCount => damage_count;
    public string Description => description;
    public float Cooldown { get => cooldown; }
    public string Code => naming.ToLower().Replace(" ", "_");
    public int ProjectileCount => 1 + P_Stats.Instance.ProjectileCount;
    public int Id => id;
    public string IconPath => icon_path;
    public string Info => FormatInfo();

    public virtual float Radius { get; set; }
    public virtual float PushForce { get; set; }

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
    public virtual string FormatInfo()
    {
        return "";
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

[Ability]
class Fireball : AbilityBase
{
    private const string NAME = "Fireball";
    private static float COUNT = 25;
    private const DamageType TYPE = global::DamageType.Magical;
    private const string DESCRIPTION = "The player releases a fireball that automatically targets and damages the nearest enemy.";
    private const float COOLDOWN = 2.5f;
    public Fireball() : base(NAME, DESCRIPTION, TYPE, COUNT, COOLDOWN) {
    }
    public float ProjectileSpeed => 10f;
    public float Range => 10f;
    public override string FormatInfo()
    {
        return $"{DamageType} Damage: {DamageCount}\n" +
            $"Cooldown: {Cooldown}\n" +
            $"Projectile Count: {ProjectileCount}\n";
    }
}

[Ability]
class PlasmaSpheres : AbilityBase
{
    private const string NAME = "Plasma Spheres";
    private static float COUNT = 15;
    private const DamageType TYPE = global::DamageType.Magical;
    private const string DESCRIPTION = "Plasma spheres orbit around the player, causing damage to any enemies that come into contact with them.";
    private const float COOLDOWN = 5f;
    public PlasmaSpheres() : base(NAME, DESCRIPTION, TYPE, COUNT, COOLDOWN)
    {
    }
    public float ProjectileSpeed => 5f;
    public override float Radius => 2f;
    public float Duration => COOLDOWN;
    public override string FormatInfo()
    {
        return $"{DamageType} Damage: {DamageCount}\n" +
            $"Cooldown: {Cooldown}\n" +
            $"Projectile Count: {ProjectileCount}\n" +
            $"Duration: {Duration}\n" +
            $"Radius: {Radius}";
    }
}

[Ability]
class Whirligig : AbilityBase
{
    private const string NAME = "Whirligig";
    private static float COUNT = 50;
    private const DamageType TYPE = global::DamageType.Physical;
    private const string DESCRIPTION = "The player surrounds themselves with a spinning sawblade, which damages enemies and pushes them away upon contact.";
    private const float COOLDOWN = 3f;
    public Whirligig() : base(NAME, DESCRIPTION, TYPE, COUNT, COOLDOWN)
    {
    }
    public override float Radius => 4f;
    public float Duration => 0.8f;
    public override float PushForce => 15f;
    public float RecoveryTime => 0.3f;
    public override string FormatInfo()
    {
        return $"{DamageType} Damage: {DamageCount}\n" +
            $"Cooldown: {Cooldown}\n" +
            $"Duration: {Duration}\n" +
            $"Push Force: {PushForce}\n" +
            $"Radius: {Radius}";
    }
}

[Ability]
class RicochetStone : AbilityBase
{
    private const string NAME = "Ricochet Stone";
    private static float COUNT = 20;
    private const DamageType TYPE = global::DamageType.Physical;
    private const string DESCRIPTION = "Upon striking an enemy, the stone shatters into smaller fragments, dealing additional damage to nearby foes.";
    private const float COOLDOWN = 2.8f;
    public RicochetStone() : base(NAME, DESCRIPTION, TYPE, COUNT, COOLDOWN)
    {
    }
    public float Range => 10f;
    public float ProjectileSpeed => 10f;
    public int FragmentsMaxCount => 3;

    public override string FormatInfo()
    {
        return $"{DamageType} Damage: {DamageCount}\n" +
            $"Cooldown: {Cooldown}\n" +
            $"Projectile Speed: {ProjectileSpeed}\n" +
            $"Range: {Range}\n" +
            $"Fragment Damage: {DamageCount / 2}\n" +
            $"Fragments Max Count: {FragmentsMaxCount}";
    }
}

[Ability]
class LaserBeam : AbilityBase
{
    private const string NAME = "Laser Beam";
    private static float COUNT = 100;
    private const DamageType TYPE = global::DamageType.Magical;
    private const string DESCRIPTION = "This ability releases a powerful laser beam that pierces through enemies in its path, dealing massive damage and destroying obstacles.";
    private const float COOLDOWN = 2f;
    public LaserBeam() : base(NAME, DESCRIPTION, TYPE, COUNT, COOLDOWN)
    {
    }
    public float Length => 16f;
    public float ProjectileSpeed => 10f;

    public override string FormatInfo()
    {
        return $"{DamageType} Damage: {DamageCount}\n" +
            $"Cooldown: {Cooldown}\n" +
            $"Projectile Speed: {ProjectileSpeed}\n" +
            $"Length: {Length}\n";
    }
}

[Ability]
class UFORay : AbilityBase
{
    private const string NAME = "UFO Ray";
    private static float COUNT = 25;
    private const DamageType TYPE = global::DamageType.Magical;
    private const string DESCRIPTION = "A powerful beam descends from the spaceship, pulling enemies upward into the ship. This ability imediatelly kills any enemy.";
    private const float COOLDOWN = 15f;
    public UFORay() : base(NAME, DESCRIPTION, TYPE, COUNT, COOLDOWN)
    {
    }
    public float Range => 10f;
    public float ProjectileSpeed => 5f;

    public override string FormatInfo()
    {
        return $"{DamageType} Damage: ?UNLIMITED?\n" +
            $"Cooldown: {Cooldown}\n" +
            $"Projectile Count: {ProjectileCount}\n" +
            $"Lift Speed: {ProjectileSpeed}\n";
    }
}
