using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/HP-Potion")]
public class healthPotion : Item
{
    public float healingAmount;
    
    public override void Use()
    {
        base.Use();
        //tambah darah
        float currHealth = PlayerPrefs.GetFloat("darah");
        currHealth += healingAmount;
        PlayerPrefs.SetFloat("darah",currHealth);
        
        //kurangin dari inventory
        RemoveFromInventory();
    }
}
