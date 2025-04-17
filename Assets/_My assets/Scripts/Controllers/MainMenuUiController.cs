using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUiController : MonoBehaviour
{
    UiManager uiManager;

    [Header ("<b>Scriptable")]
    [SerializeField] public PlayerData playerData;
    [SerializeField] public InputData inputData;
    [SerializeField] public TimerData timerData;
    [SerializeField] public GameData gameData;
    [SerializeField] public WeaponData weaponData;

    [Header ("<b>Components")]
    [SerializeField] public TMP_Text interactableText;
    [SerializeField] public TMP_Text outsideTimerCountText;
    [SerializeField] public TMP_Text weaponNameText;
    [SerializeField] public TMP_Text FpsCounterText;

    [Header("<b>User interface")]
    [SerializeField] public Image weaponIconImage;
    [SerializeField] public Image crossHairImage;

    void Start()
    {
        uiManager = UiManager.instance;
        ActionManager.OnWeaponPicked?.Invoke(weaponData.weaponDatabase[(int)WeaponID.NULL]);
        uiManager.OpenCanvas("Gameplay");
    }

    private void OnEnable()
    {
        ActionManager.OnInteract += OnInteraceConfirm;
        ActionManager.OnOutsideBoundries += BoundryCheck;
        ActionManager.OnWeaponPicked += WeaponPicked;
    }

    private void OnDisable()
    {
        ActionManager.OnInteract -= OnInteraceConfirm;
        ActionManager.OnOutsideBoundries -= BoundryCheck;
        ActionManager.OnWeaponPicked -= WeaponPicked;
    }


    private void Update()
    {
        outsideTimerCountText.text = $"0{timerData.outsideTimer.ToString("0")}";
        FpsCounterText.text = Mathf.Ceil(gameData.FpsCount).ToString() + " FPS";

        UpdateCrosshair();
    }

    public void B_OpenItemInfo() => uiManager.OpenCanvas("ItemInfo");

    private void WeaponPicked(Weapon weapon)
    {
        weaponIconImage.sprite = weapon.weaponIcon;
        weaponNameText.text = weapon.weaponName;
    }


    private void OnInteraceConfirm(bool check, string itemInformation)
    {
        if (check)
        {
            interactableText.text = itemInformation;
            uiManager.OpenPopup("ItemInfo");
        }
        else
        {
            interactableText.text = string.Empty;
            uiManager.ClosePopup("ItemInfo");
        }
    }

    private void BoundryCheck(BoundryValue boundryValue)
    {
        Debug.Log("Action call");
        switch (boundryValue)
        {
            case BoundryValue.INSIDE:
                uiManager.ClosePopup("OutOfBoundry");
                break;
            case BoundryValue.OUTSIDE:
                uiManager.OpenPopup("OutOfBoundry");
                break;
        }
    }

    private void UpdateCrosshair()
    {
        if (!playerData.isMoving)
        {
            crossHairImage.sprite = gameData.steadyCrosshair;
            return;
        }
        
        switch (playerData.sprintingValue)
        {
            case SprintingValue.NOT_SPRINTING:
                crossHairImage.sprite = gameData.walkingCrosshair;
                break;
            case SprintingValue.IS_SPRINTING:
                crossHairImage.sprite = gameData.sprintingCrosshair;
                break;
        }
    }
}
