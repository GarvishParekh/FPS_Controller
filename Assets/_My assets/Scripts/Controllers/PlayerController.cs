using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    Rigidbody playerRb;

    [SerializeField] private PlayerData playerData;
    [SerializeField] private InputData inputData;

    [Space]
    [SerializeField] private Transform neck;
    [SerializeField] private Transform playerBody;

    float mouseX;
    float mouseY;
    float xRotation;
    float targetSpeedValue = 0;

    private void Awake() => playerRb = GetComponent<Rigidbody>();

    private void FixedUpdate()
    {
        CheckPlayerBlocked();
        Movement();
        PlayerSprinting();
        CameraMovement();
    }

    private void Movement()
    {
        Vector3 movementVec = transform.forward  * inputData.zInput + transform.right * inputData.xInput;
        playerRb.velocity = movementVec.normalized * playerData.playerCurrentSpeed;
    }

    private void CameraMovement()
    {
        mouseX = inputData.xHead * playerData.mouseSensitivity * Time.deltaTime;
        mouseY = inputData.yHead * playerData.mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Clamp vertical rotation

        neck.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // Up and down (camera only)
        playerBody.Rotate(Vector3.up * mouseX); // Left and right (whole player)
    }

    private void PlayerSprinting()
    {
        switch (playerData.sprintingValue)
        {
            case SprintingValue.NOT_SPRINTING:
                targetSpeedValue = playerData.playerNormalSpeed;
                break;
            case SprintingValue.IS_SPRINTING:
                targetSpeedValue = playerData.playerSprintingSpeed;
                break;
        }
        playerData.playerCurrentSpeed = Mathf.MoveTowards
            (
                playerData.playerCurrentSpeed, 
                targetSpeedValue, 
                playerData.playerSpeedSmoothningValue
            );
    }

    private void CheckPlayerBlocked()
    {
        playerData.recordedPlayerVelocity = playerRb.velocity.magnitude;
        if (playerRb.velocity.magnitude < 0.1f  ) playerData.playerBlocked = PlayerBlocked.IS_BLOCKED;
        else playerData.playerBlocked = PlayerBlocked.NOT_BLOCKED;
    }
}

