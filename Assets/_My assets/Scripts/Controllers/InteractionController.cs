using UnityEngine;

public class InteractionController : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private Transform originPoint;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private float rayDistance;

    private void Update()
    {
        FireRayCast();
    }

    private void FireRayCast()
    {
        // Fire a ray from the center of the screen
        RaycastHit hit;
        Ray ray = new Ray(originPoint.position, originPoint.forward);

        if (Physics.Raycast(ray, out hit, rayDistance, targetLayer))
        {
            if (playerData.interactableValue == InteractableValue.NOT_FOUND)
            {
                IInteractable interactable = hit.collider.GetComponent<IInteractable>();
                if (interactable == null) return;

                interactable.OnInterace();
                Debug.Log($"object found: {hit.collider.name}");
            }
            playerData.interactableValue = InteractableValue.FOUND;
            Debug.DrawRay(originPoint.position, originPoint.forward * rayDistance, Color.black);
        }
        else
        {
            Debug.DrawRay(originPoint.position, originPoint.forward * rayDistance, Color.red);
            if (playerData.interactableValue == InteractableValue.FOUND)
            {
                ActionManager.OnInteract?.Invoke(false, "");
            }
            playerData.interactableValue = InteractableValue.NOT_FOUND;
        }
    }
}
