using UnityEngine;

[CreateAssetMenu(fileName = "Player data", menuName = "Scriptable/Player data")]
public class PlayerData : ScriptableObject
{
    public InteractableValue interactableValue;

    [Header("<b>Weapon information")]
    public WeaponID equipedWeaponID;   

    [Header("<b>Position")]
    public BoundryValue currentBoundryValue;
    public BoundryValue previousBoundryValue;

    [Header("<b>Foot movement")]
    public SprintingValue sprintingValue;
    public float playerCurrentSpeed = 2;
    public float playerNormalSpeed = 2;
    public float playerSprintingSpeed = 5;
    public float playerSpeedSmoothningValue = 0.25f;

    [Header ("<b>Head movement")]
    public float mouseSensitivity = 100;
}

