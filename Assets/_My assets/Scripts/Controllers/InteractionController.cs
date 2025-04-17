using UnityEngine;

public class InteractionController : MonoBehaviour
{
    [Header ("<b>Scriptable")]
    [SerializeField] private PlayerData playerData;
    [SerializeField] private WeaponData weaponData;

    [Header ("<b>Components")]
    [SerializeField] private Transform originPoint;
    [SerializeField] private LayerMask targetLayer;
   

    private void FixedUpdate()
    {
        FireRayCast();
    }

    /*
    private void FireRayCast()
    {
        // Fire a ray from the center of the screen
        RaycastHit hit;
        Ray ray = new Ray(originPoint.position, originPoint.forward);

        if (Physics.Raycast(ray, out hit, rayDistance, targetLayer))
        {
            // myWeapon pickup 
            IPickable pickable = hit.collider.GetComponent<IPickable>();
            if (pickable != null)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    Debug.Log($"Player picked up myWeapon {pickable.GetItemName()}");
                    pickable.OnPick();
                }
            }

            // item information 
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
    */

    private void FireRayCast()
    {
        Ray ray = new Ray(originPoint.position, originPoint.forward);
        bool hitSomething = Physics.Raycast(ray, out RaycastHit hit, playerData.detectioRayDistance, targetLayer);

        // Always draw ray (debugging)
        Debug.DrawRay(ray.origin, ray.direction * playerData.detectioRayDistance, hitSomething ? Color.black : Color.red);

        if (!hitSomething)
        {
            if (playerData.interactableValue == InteractableValue.FOUND)
            {
                ActionManager.OnInteract?.Invoke(false, string.Empty);
            }

            playerData.interactableValue = InteractableValue.NOT_FOUND;
            return;
        }

        // Check for IPickable
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (hit.collider.TryGetComponent<IPickable>(out var pickable))
            {
                if (weaponData.equippedWeapon != WeaponID.NULL)
                {
                    ActionManager.OnThrowWeapon?.Invoke(false);
                }
                pickable.OnPick();
            }
        }

        // Check for IInteractable only if not already found
        if (playerData.interactableValue == InteractableValue.NOT_FOUND &&
            hit.collider.TryGetComponent<IInteractable>(out var interactable))
        {
            interactable.OnInterace(); // Typo? Should this be "OnInteract"?
            Debug.Log($"Object found: {hit.collider.name}");

            playerData.interactableValue = InteractableValue.FOUND;
        }
    }

}
