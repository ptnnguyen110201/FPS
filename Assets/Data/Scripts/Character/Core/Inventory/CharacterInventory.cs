using System.Collections.Generic;
using UnityEngine;

public class CharacterInventory
{
    public Dictionary<string, string> weapons = new Dictionary<string, string>();
    public Dictionary<string, int> items = new Dictionary<string, int>();

    public void AddWeapon(string weaponName, string ammoType, int ammoCount)
    {
        if (!this.weapons.ContainsKey(weaponName)) this.weapons[weaponName] = ammoType;
        this.AddItem(ammoType, ammoCount);
    }

    public void AddItem(string itemName, int quantity)
    {
        if (!this.items.ContainsKey(itemName)) this.items.Add(itemName, quantity);
        else this.items[itemName] += quantity;

    }

    public void DeleteItem(string itemName, int quantity)
    {
        if (!this.items.ContainsKey(itemName)) return;

        this.items[itemName] -= quantity;

        if (this.items[itemName] <= 0) this.items.Remove(itemName);
    }

    public string GetAmmo(string weaponName)
    {
        if (!this.weapons.ContainsKey(weaponName)) return null;
        return this.weapons[weaponName];
    }
    public int GetItemCount(string itemName)
    {
        if (!this.items.ContainsKey(itemName)) return 0;
        return this.items[itemName];

    }
}
