using System;

public static class ActionManager 
{
    public static Action<bool, string> OnInteract;
    public static Action<BoundryValue> OnOutsideBoundries;
}
