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

    [Header("Walking sway settings")]
    public bool addWalkSway = true;
    public float swayheight = 1f;
    public float swayTimer = 0f;
    public float SwaySpeed = 0.001f;
    public float normalSwaySpeed = 0.001f;
    public float sprintingSwaySpeed = 0.005f;
}
