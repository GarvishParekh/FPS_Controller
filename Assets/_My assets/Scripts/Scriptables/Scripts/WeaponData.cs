using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu (fileName = "Weapon data", menuName = "Scriptable/Weapon data")]
public class WeaponData : ScriptableObject
{
    public string equippedName;
    public WeaponID equippedWeapon;

    [Header ("<b>Weapon data")]
    public List<Weapon> weaponDatabase = new List<Weapon>();

    [Header("Sway Settings")]
    public bool addSway = true;
    public float swayAmount = 5f;
    public float maxSway = 5f;
    public float smoothAmount = 10f;
}
