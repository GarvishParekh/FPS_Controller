using TMPro;
using UnityEngine;

public class MainMenuUiController : MonoBehaviour
{
    UiManager uiManager;

    [Header ("<b>Scriptable")]
    [SerializeField] public PlayerData playerData;
    [SerializeField] public TimerData timerData;

    [Header ("<b>Components")]
    [SerializeField] public TMP_Text interactableText;
    [SerializeField] public TMP_Text outsideTimerCountText;

    void Start() => uiManager = UiManager.instance;

    private void OnEnable()
    {
        ActionManager.OnInteract += OnInteraceConfirm;
        ActionManager.OnOutsideBoundries += BoundryCheck;
    }

    private void OnDisable()
    {
        ActionManager.OnInteract -= OnInteraceConfirm;
        ActionManager.OnOutsideBoundries -= BoundryCheck;
    }

    private void Update()
    {
        outsideTimerCountText.text = $"0{timerData.outsideTimer.ToString("0")}";
    }

    public void B_OpenItemInfo() => uiManager.OpenCanvas("ItemInfo");

   

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
