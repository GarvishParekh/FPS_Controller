using UnityEngine;

public class BoundryController : MonoBehaviour
{
    [Header("Components")]
    public Transform player; // Assign points manually in inspector
    [SerializeField] private BoundryValue boundryValue;

    [Header("Boundary Points")]
    public Transform[] boundaryPoints; // Assign points manually in inspector

    [Header("Gizmos Settings")]
    public Color activeBoundryColor = Color.green;
    public bool showGizmosAlways = true;

    private void Update()
    {
        IsInsideVillage();
    }

    // Check if player is inside the boundary
    public void IsInsideVillage()
    {
        Vector2 playerPos2D = new Vector2(player.position.x, player.position.z);

        boundryValue = BoundryValue.OUTSIDE;
        activeBoundryColor = Color.red;
        int j = boundaryPoints.Length - 1;

        for (int i = 0; i < boundaryPoints.Length; i++)
        {
            Vector2 pi = new Vector2(boundaryPoints[i].position.x, boundaryPoints[i].position.z);
            Vector2 pj = new Vector2(boundaryPoints[j].position.x, boundaryPoints[j].position.z);

            // check if the player is inside the boundry
            if ((pi.y > playerPos2D.y) != (pj.y > playerPos2D.y) &&
                (playerPos2D.x < (pj.x - pi.x) * (playerPos2D.y - pi.y) / (pj.y - pi.y) + pi.x))
            {
                boundryValue = BoundryValue.INSIDE;
                activeBoundryColor = Color.green; 
            }

            j = i;
        }
    }

    // Draw Gizmos in Scene View
    private void OnDrawGizmos()
    {
        if (!showGizmosAlways || boundaryPoints == null || boundaryPoints.Length < 2)
            return;

        Gizmos.color = activeBoundryColor;
        for (int i = 0; i < boundaryPoints.Length; i++)
        {
            if (boundaryPoints[i] != null)
            {
                Vector3 currentPoint = boundaryPoints[i].position;
                Vector3 nextPoint = boundaryPoints[(i + 1) % boundaryPoints.Length].position;
                Gizmos.DrawLine(currentPoint, nextPoint);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (showGizmosAlways) return; // Already drawn

        OnDrawGizmos(); // Draw only when selected
    }
}
