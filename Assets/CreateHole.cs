using UnityEngine;

public class CreateHole : MonoBehaviour
{
    public GameObject[] planes; // Array to store references to multiple planes
    public float holeRadius = 0.5f; // Radius of the hole
    public LayerMask planeLayer; // Layer mask to identify the plane
    public AudioClip collisionSound; // Sound to play on collision

    private Mesh[] planeMeshes;
    private MeshCollider[] planeColliders;
    private AudioSource audioSource;

    void Start()
    {
        // Initialize the array to store the meshes and colliders of the planes
        planeMeshes = new Mesh[planes.Length];
        planeColliders = new MeshCollider[planes.Length];

        // Ensure each plane has a MeshFilter and MeshCollider component and store them
        for (int i = 0; i < planes.Length; i++)
        {
            if (planes[i] != null)
            {
                planeMeshes[i] = planes[i].GetComponent<MeshFilter>().mesh;
                planeColliders[i] = planes[i].GetComponent<MeshCollider>();
                planeColliders[i].sharedMesh = planeMeshes[i];
            }
        }

        // Get the AudioSource component attached to the object
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Manually check for collision with each plane
        CheckForCollision();
    }

    void CheckForCollision()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, holeRadius, planeLayer);
        foreach (var hitCollider in hitColliders)
        {
            for (int i = 0; i < planes.Length; i++)
            {
                if (hitCollider.gameObject == planes[i])
                {
                    CreateMeshHole(planeMeshes[i], hitCollider.transform, transform.position, holeRadius);
                    PlayCollisionSound(); // Play sound on collision with plane
                    UpdateMeshCollider(planeColliders[i], planeMeshes[i]); // Update the mesh collider
                }
            }
        }
    }

    void CreateMeshHole(Mesh mesh, Transform planeTransform, Vector3 position, float radius)
    {
        Vector3[] vertices = mesh.vertices;
        Vector3 localPosition = planeTransform.InverseTransformPoint(position);

        for (int i = 0; i < vertices.Length; i++)
        {
            if (Vector3.Distance(vertices[i], localPosition) < radius)
            {
                vertices[i] = new Vector3(vertices[i].x, vertices[i].y - radius, vertices[i].z);
            }
        }

        mesh.vertices = vertices;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }

    void UpdateMeshCollider(MeshCollider collider, Mesh mesh)
    {
        collider.sharedMesh = null; // Clear the current mesh
        collider.sharedMesh = mesh; // Assign the updated mesh
    }

    void PlayCollisionSound()
    {
        if (collisionSound != null && audioSource != null && !audioSource.isPlaying)
        {
            audioSource.PlayOneShot(collisionSound);
        }
    }
}
