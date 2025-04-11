using TMPro;
using UnityEngine;

public class MainMenuUiController : MonoBehaviour
{
    UiManager uiManager;
    [SerializeField] public TMP_Text interactableText;

    void Start() => uiManager = UiManager.instance;

    private void OnEnable()
    {
        ActionManager.OnInteract += OnInteraceConfirm;
    }

    private void OnDisable()
    {
        ActionManager.OnInteract -= OnInteraceConfirm;
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
}
