using UnityEngine;

public class MainMenuUiController : MonoBehaviour
{
    UiManager uiManager;
    void Start()
    {
        uiManager = UiManager.instance;
    }

    public void B_OpenItemInfo() => uiManager.OpenCanvas("ItemInfo");
}
