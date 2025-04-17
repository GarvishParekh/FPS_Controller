using UnityEngine;

[CreateAssetMenu (fileName = "Game Data", menuName = "Scriptable/Game Data")]
public class GameData : ScriptableObject
{
    public float FpsCount = 0;
    public Sprite steadyCrosshair;
    public Sprite walkingCrosshair;
    public Sprite sprintingCrosshair;
}
