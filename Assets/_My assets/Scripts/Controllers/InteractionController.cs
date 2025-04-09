using UnityEngine;

public class InteractionController : MonoBehaviour
{
    UiManager uiManager;

    [SerializeField] private Transform originPoint;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private float rayDistance;

    private void Start()
    {
        uiManager = UiManager.instance;
    }

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
            Debug.Log("Hit an interactable object: " + hit.collider.name);
            Debug.DrawRay(originPoint.position, originPoint.forward * rayDistance, Color.green);
        }
        else
        {
            Debug.DrawRay(originPoint.position, originPoint.forward * rayDistance, Color.red);
        }
    }
}
