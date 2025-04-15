using UnityEngine;

[CreateAssetMenu (fileName = "Timer Data", menuName = "Scriptable/Timer Data")]
public class TimerData : ScriptableObject
{
    public float outsideTimer;
    public float outsideTimerThreshold = 3;
}
