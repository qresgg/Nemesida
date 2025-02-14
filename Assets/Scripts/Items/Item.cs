using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEngine;

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

class MagicQuiver : ScriptableObject, Item
{
    public string Name { get; } = "Magic Quiver";
    public bool IsNewItem { get; set; } = true;
    public string Description { get; } = "Empowers your hero with the ability to launch additional projectiles, enhancing their offensive capabilities and allowing them to overwhelm enemies with a barrage of attacks.";
    int ProjectileStep { get; } = 1;
    public string Code { get; } = "magic_quiver";
    public int Id { get; } = 1;
    public string IconPath { get; } = "Images/UI/ItemIcons/MagicQuiver";

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

    public ItemLevel ItemLevel { get; private set; } = new ItemLevel(1);
    public void UseBonus()
    {
        P_Stats.Instance.SetProjectileCount(ProjectileStep);
    }
    /* private void UpdateItemStats()
    {
        switch (ItemLevel.Level)
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
class Sirnycks : ScriptableObject, Item
{
    public string Name { get; } = "Sirnycks";
    public bool IsNewItem { get; set; } = true;
    public string Description { get; } = "Ignites to significantly boost magic damage. Essential for spellcasters who want to enhance their attack power and dominate their enemies with increased magical prowess.";
    public int MagicDamageAmplifierStep { get; } = 25; 
    public string Code { get; } = "sirnycks";
    public int Id { get; } = 2;
    public string IconPath { get; } = "Images/UI/ItemIcons/Sirnycks";

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
        P_Stats.Instance.SetMagicDamageAmplifier(MagicDamageAmplifierStep);
    }
    public ItemLevel ItemLevel { get; private set; } = new ItemLevel(1);
}
class Smth : ScriptableObject, Item
{
    public string Name { get; } = "M2r";
    public bool IsNewItem { get; set; } = true;
    public string Description { get; } = "";
    public string Code { get; } = "lacker_quiver";
    public int Id { get; } = 1;
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
