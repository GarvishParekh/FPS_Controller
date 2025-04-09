using UnityEngine;

public class SfxController : MonoBehaviour
{
    [Header("<b>Compenents")]
    [SerializeField] private Rigidbody playerRb;
    [SerializeField] private AudioSource footStepsAudioSource;

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

    private void FixedUpdate() => PlayFootSteps();

    private void PlayFootSteps()
    {
        if (playerRb.velocity == Vector3.forward * 0) return;

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
}
