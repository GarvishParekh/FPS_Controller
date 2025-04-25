using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private InputData inputData;
    [SerializeField] private PlayerData playerData;

    float xValue;
    float zValue;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Application.targetFrameRate = 120;
        QualitySettings.vSyncCount = 1;
    }

    public void Update()
    {
        // keyboard
        xValue = Input.GetAxisRaw("Horizontal");
        zValue = Input.GetAxisRaw("Vertical");

        inputData.xInput = xValue;
        inputData.zInput = zValue;

        MoveCheck();

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

    private void MoveCheck()
    {
        if (xValue == 0 && zValue == 0) playerData.isMoving = false;
        else playerData.isMoving = true;
    }
}
