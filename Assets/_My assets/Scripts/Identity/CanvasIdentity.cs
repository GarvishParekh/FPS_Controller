using UnityEngine;

[RequireComponent (typeof(CanvasGroup))] 
public class CanvasIdentity : MonoBehaviour
{
    UiManager uiManager;
    CanvasGroup canvasGroup;
    ICanvasAnimation canvasAnimation;

    public CanvasIdentityDatabase database;
    public int selectedIndex = 0;

    bool isOpen = false;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasAnimation = GetComponent<ICanvasAnimation>();
    }

    private void Start()
    {
        uiManager = UiManager.instance;
        uiManager.AddToList(this);
        CloseCanvas();
    }

    public string SelectedIdentity
    {
        get
        {
            if (database != null && database.canvasIdentities.Count > selectedIndex)
                return database.canvasIdentities[selectedIndex];
            return string.Empty;
        }
    }

    public void OpenCanvas()
    {
        if (isOpen) return;

        isOpen = true;
        canvasGroup.alpha = 1;  
        canvasGroup.blocksRaycasts = true;

        if (canvasAnimation != null) canvasAnimation.AnimateCanvas();
    }

    public void CloseCanvas()
    {
        isOpen = false;
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;

    }
}
