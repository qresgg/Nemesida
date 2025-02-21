using System;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
sealed class ItemAttribute : Attribute
{
    public ItemAttribute() { }
}
public interface Item
{
    string Name { get; }
    bool IsNewItem { get; }
    string Description { get; }
    string Code { get; }
    int Id { get; }
    string IconPath { get; }

    void UpgradeItem();
    void SetNewItem(bool value);
    void UseBonus();
    ItemLevel ItemLevel { get; }
}

public abstract class ItemBase : ScriptableObject, Item
{
    private static int nextId = 1;

    private string naming;
    private string description;
    private bool is_new_item = true;
    private int id;
    private string icon_path;
    private string info;
    public ItemBase(string name, string description)
    {
        naming = name;
        this.description = description;
        id = nextId++;
        icon_path = $"Images/UI/ItemIcons/{name.Replace(" ", "")}";
    }
    public string Name => naming;
    public bool IsNewItem { get; set; } = true;
    public string Description => description;
    public string Code { get => naming.ToLower().Replace(" ", "_"); }
    public int Id { get => id; }
    public string IconPath { get => icon_path; }

    public void UpgradeItem() {
        IsNewItem = false;
        ItemLevel.LevelUp(this);
        //UpdateAbilityStats();
    }
    public void SetNewItem(bool value)
    {
        IsNewItem = value;
    }
    public virtual void UseBonus() {

    }
    public ItemLevel ItemLevel { get; private set; } = new ItemLevel(1);
}

public class ItemLevel
{
    public int Level { get; private set; }

    public ItemLevel(int level)
    {
        Level = level;
    }

    public void LevelUp(Item item)
    {
        Level++;
        if (Level >= 2)
        {
            item.SetNewItem(false);
        }
    }
}

[Item]
class MagicQuiver : ItemBase
{
    private const string NAME = "MagicQuiver";
    private const string DESCRIPTION = "Empowers your hero with the ability to launch additional projectiles, enhancing their offensive capabilities and allowing them to overwhelm enemies with a barrage of attacks.";
    public MagicQuiver() : base(NAME, DESCRIPTION)
    {
    }
    private int ProjectileStep => 1;
    public override void UseBonus()
    {
        P_Stats.Instance.ProjectileCount = ProjectileStep;
    }
}

[Item]
class Sirnycks : ItemBase
{
    private const string NAME = "Sirnycks";
    private const string DESCRIPTION = "Ignites to significantly boost magic damage. Essential for spellcasters who want to enhance their attack power and dominate their enemies with increased magical prowess.";
    public Sirnycks() : base(NAME, DESCRIPTION)
    {
    }
    public int MagicDamageAmplifierStep { get; } = 25;
    public override void UseBonus()
    {
        P_Stats.Instance.MagicDamageAmplifier = MagicDamageAmplifierStep;
    }
}

[Item]
class Declaration : ItemBase
{
    private const string NAME = "Declaration";
    private const string DESCRIPTION = "A rare and ancient scroll that grants the user an additional ability slot. Said to be inscribed by the greatest sorcerers of old, this powerful artifact allows for unprecedented growth and mastery.";
    public Declaration() : base(NAME, DESCRIPTION)
    {
    }
    public int AbilitiesMaxCountStep { get; } = 1;
    public override void UseBonus()
    {
        P_Stats.Instance.MaxAbilitiesCount = AbilitiesMaxCountStep;
    }
}

class Smth : ScriptableObject, Item
{
    public string Name { get; } = "s";
    public bool IsNewItem { get; set; } = true;
    public string Description { get; } = "";
    public string Code { get; } = "lacker_quiver";
    public int Id { get; } = 4;
    public string IconPath { get; } = "Images/UI/ItemIcons/default_item";

    public void UpgradeItem()
    {
        IsNewItem = false;
        ItemLevel.LevelUp(this);
        //UpdateAbilityStats();
    }
    public void SetNewItem(bool value)
    {
        IsNewItem = value;
    }
    public void UseBonus()
    {
    }
    public ItemLevel ItemLevel { get; private set; } = new ItemLevel(1);
}

