using System.Collections;
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
    [SerializeField] public ContentSizeFitter interactableTextSizeFitter;
    [SerializeField] public TMP_Text outsideTimerCountText;
    [SerializeField] public TMP_Text weaponNameText;
    [SerializeField] public TMP_Text FpsCounterText;

    [Header("<b>Controls")]
    [SerializeField] public GameObject touchControls;

    [Header("<b>User interface")]
    [SerializeField] public Image weaponIconImage;
    [SerializeField] public Image crossHairImage;
    [SerializeField] public Transform crossHairTransform;

    void Start()
    {
        uiManager = UiManager.instance;
        ActionManager.OnWeaponPicked?.Invoke(weaponData.weaponDatabase[(int)WeaponID.NULL]);
        uiManager.OpenCanvas("Gameplay");
        UpdateTouchControls();
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
            StartCoroutine(PopupFunction(check, itemInformation));

            /*
            interactableText.text = itemInformation;
            uiManager.OpenPopup("ItemInfo");
            interactableTextSizeFitter.enabled = check;
            */
        }
        else
        {
            StartCoroutine(PopupFunction(check, itemInformation));
            /*
            interactableText.text = string.Empty;
            uiManager.ClosePopup("ItemInfo");
            interactableTextSizeFitter.enabled = check;
            */
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
            crossHairTransform.localScale = Vector3.MoveTowards(crossHairTransform.localScale, Vector3.one, 0.06f);
            return;
        }
        
        switch (playerData.sprintingValue)
        {
            case SprintingValue.NOT_SPRINTING:
                crossHairTransform.localScale = Vector3.MoveTowards(crossHairTransform.localScale, Vector3.one * 1.4f, 0.1f);
                crossHairImage.sprite = gameData.walkingCrosshair;
                break;
            case SprintingValue.IS_SPRINTING:
                crossHairTransform.localScale = Vector3.MoveTowards(crossHairTransform.localScale, Vector3.one * 2f, 0.1f);
                crossHairImage.sprite = gameData.sprintingCrosshair;
                break;
        }
    }

    IEnumerator PopupFunction(bool check, string itemInfo)
    {
        interactableText.text = itemInfo;
        if (check) uiManager.OpenPopup("ItemInfo");
        else uiManager.ClosePopup("ItemInfo");

        interactableTextSizeFitter.enabled = true;
        yield return null;
        interactableTextSizeFitter.enabled = false;
    }

    private void UpdateTouchControls()
    {
        if (touchControls == null) return;

        if (inputData.inputType == InputType.TOUCH) touchControls.SetActive(true);
        else touchControls.SetActive(false);
    }

}
