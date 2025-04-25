using UnityEngine;

public class SfxController : MonoBehaviour
{
    [Header("<b>Compenents")]
    [SerializeField] private AudioSource footStepsAudioSource;
    [SerializeField] private AudioSource weaponEquipAudioSource;

    [Header ("<b>Scriptable")]
    [SerializeField] private SfxData sfxData;
    [SerializeField] private InputData inputData;
    [SerializeField] private PlayerData playerData;

    float counter = 0;
    int footStepRandomIndex = 0;
    float footstepsDelay = 0;

    private void Awake()
    {
        footstepsDelay = sfxData.normalDelay;
    }

    private void OnEnable()
    {
        ActionManager.OnWeaponPicked += WeaponPickUpSound;
    }

    private void OnDisable()
    { 
        ActionManager.OnWeaponPicked -= WeaponPickUpSound;
    }

    private void FixedUpdate() => PlayFootSteps();

    private void PlayFootSteps()
    {
        if (playerData.playerBlocked == PlayerBlocked.IS_BLOCKED) return;

        if (inputData.xInput == 0 && inputData.zInput == 0)
        {
            counter = footstepsDelay;
            return;
        }

        switch (playerData.sprintingValue)
        {
            case SprintingValue.NOT_SPRINTING:
                footstepsDelay = sfxData.normalDelay;
                break;
            case SprintingValue.IS_SPRINTING:
                footstepsDelay = sfxData.sprintDelay;
                break;
        }

        if (counter >= footstepsDelay)
        {
            footStepRandomIndex = Random.Range (0, sfxData.footstepClips.Length);
            footStepsAudioSource.PlayOneShot(sfxData.footstepClips[footStepRandomIndex]);

            counter = 0;
        }
        else
        {
            counter += Time.deltaTime;
        }
    }

    private void WeaponPickUpSound(Weapon weapon)
    {
        if (weapon.
            weaponID == WeaponID.NULL)
        {
            weaponEquipAudioSource.PlayOneShot(sfxData.weaponDropClip);
        }
        else  weaponEquipAudioSource.PlayOneShot(sfxData.weaponEquipClip);
    }
}
