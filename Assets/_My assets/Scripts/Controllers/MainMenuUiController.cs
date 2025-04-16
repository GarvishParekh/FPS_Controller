using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class MainMenuUiController : MonoBehaviour
{
    UiManager uiManager;

    [Header ("<b>Scriptable")]
    [SerializeField] public PlayerData playerData;
    [SerializeField] public TimerData timerData;

    [Header ("<b>Components")]
    [SerializeField] public TMP_Text interactableText;
    [SerializeField] public TMP_Text outsideTimerCountText;
    [SerializeField] public TMP_Text weaponNameText;

    [Header("<b>Weapon images")]
    [SerializeField] public List<GameObject> weaponImages = new List<GameObject>();

    void Start()
    {
        uiManager = UiManager.instance;
        ActionManager.OnWeaponPicked?.Invoke(WeaponID.NULL, "Hands");
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
    }

    public void B_OpenItemInfo() => uiManager.OpenCanvas("ItemInfo");

    private void WeaponPicked(WeaponID weaponID, string weaponName)
    {
        foreach (GameObject weaponImage in weaponImages)
        {
            if (weaponImage.transform.GetSiblingIndex() == (int)weaponID)
            {
                weaponImage.SetActive(true);
            }
            else weaponImage.SetActive(false);
        }

        weaponNameText.text = weaponName;
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
}
