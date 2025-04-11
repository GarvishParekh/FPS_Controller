using UnityEngine;

public class InteractableIdentity : MonoBehaviour, IInteractable
{
    [SerializeField] private string itemInformation;

    private void Awake()
    {
        gameObject.layer = 6;
    }

    public void OnInterace()
    {
        ActionManager.OnInteract(true, itemInformation);
    }
}
