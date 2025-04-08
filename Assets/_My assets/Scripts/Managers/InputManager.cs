using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private InputData inputData;
    [SerializeField] private PlayerData playerData;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 1;
    }

    public void Update()
    {
        // keyboard
        inputData.xInput = Input.GetAxisRaw("Horizontal");
        inputData.zInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            playerData.sprintingValue = SprintingValue.IS_SPRINTING;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            playerData.sprintingValue = SprintingValue.NOT_SPRINTING;
        }

        // mouse 
        inputData.xMouse = Input.GetAxis("Mouse X");
        inputData.yMouse = Input.GetAxis("Mouse Y");

    }
}
