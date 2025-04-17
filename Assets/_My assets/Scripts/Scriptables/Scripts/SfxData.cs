using UnityEngine;

[CreateAssetMenu (fileName = "Sfx data", menuName = "Scriptable/Sfx data")]
public class SfxData : ScriptableObject
{
    public AudioClip bgMusicClip;
    public AudioClip[] footstepClips;
    public AudioClip weaponEquipClip;
    public AudioClip weaponDropClip;

    [Header ("Footsteps audio settings")]
    public float normalDelay = 0.8f;
    public float sprintDelay = 0.25f;
}
