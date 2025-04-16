using System;

public static class ActionManager 
{
    public static Action<bool, string> OnInteract;
    public static Action<BoundryValue> OnOutsideBoundries;
    public static Action<WeaponID, string> OnWeaponPicked;
    public static Action OnThroWeapon;
}
