using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu (fileName = "Weapon data", menuName = "Scriptable/Weapon data")]
public class WeaponData : ScriptableObject
{
    public string equippedName;
    public WeaponID equippedWeapon;

    public List<Weapon> weaponDatabase = new List<Weapon>();
}
