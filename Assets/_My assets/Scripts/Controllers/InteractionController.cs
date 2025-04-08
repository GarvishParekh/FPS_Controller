using UnityEngine;

public class InteractionController : MonoBehaviour
{
    [SerializeField] private Transform originPoint;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float rayDistance;

    private void FireRayCast()
    {
        // Fire a ray from the center of the screen
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

        if (Physics.Raycast(ray, out hit, rayDistance, targetLayer))
        {
            Debug.Log("Hit an interactable object: " + hit.collider.name);
        }
    }
}
