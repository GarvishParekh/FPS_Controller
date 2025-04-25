using UnityEngine;

[CreateAssetMenu(fileName = "Player data", menuName = "Scriptable/Player data")]
public class PlayerData : ScriptableObject
{
    public InteractableValue interactableValue;

    [Header("<b>Weapon information")]
    public WeaponID equipedWeaponID;
    public float detectioRayDistance = 3.0f;

    [Header("<b>Position")]
    public BoundryValue currentBoundryValue;
    public BoundryValue previousBoundryValue;

    [Header("<b>Foot movement")]
    public float playerCurrentSpeed = 2;
    public float playerNormalSpeed = 2;
    public float playerSprintingSpeed = 5;
    public float playerSpeedSmoothningValue = 0.25f;
    public SprintingValue sprintingValue;

    [Space]
    public bool isMoving = false;
    public PlayerBlocked playerBlocked;
    public float recordedPlayerVelocity;
     
    [Header ("<b>Head movement")]
    public float mouseSensitivity = 100;
}

