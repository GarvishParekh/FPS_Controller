using UnityEngine;

public class ApplyForce : MonoBehaviour
{
    MeshCollider boxCollider;

    public AudioSource audioSoruce;
    public AudioClip[] stoneFalling;
    public Transform forceOrigin; // The point to push away from
    public float forceMagnitude = 10f;
    public Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        boxCollider = GetComponent<MeshCollider>();
        rb.isKinematic = true;
        rb.useGravity = false;
    }

    private void OnMouseDown()
    {
        rb.isKinematic = false;
        rb.useGravity = true;
        boxCollider.isTrigger = true;
        transform.SetParent(null);
        Apply();
    }

    int index = 0;
    public void Apply()
    {
        if (forceOrigin == null || rb == null)
            return;

        // Direction from the point to this object
        Vector3 direction = (transform.position - forceOrigin.position).normalized;

        // Apply the force
        rb.AddForce(direction * forceMagnitude, ForceMode.Impulse);

        index = Random.Range(0, stoneFalling.Length);
        audioSoruce.PlayOneShot(stoneFalling[index]);
    }
}
