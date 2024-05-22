using UnityEngine;

public class ActivateObjects : MonoBehaviour
{
    public GameObject[] objectsToWatch;
    public GameObject objectToDeactivate;
    public AudioClip deactivationSound;
    private bool soundPlayed = false;

    void Update()
    {
        // Check if all objects in objectsToWatch array are inactive
        bool allInactive = true;
        foreach (GameObject obj in objectsToWatch)
        {
            if (obj.activeSelf)
            {
                allInactive = false;
                break;
            }
        }

        // If all objects are inactive, deactivate objectToDeactivate
        if (allInactive)
        {
            if (objectToDeactivate != null && objectToDeactivate.activeSelf)
            {
                objectToDeactivate.SetActive(false);
                PlayDeactivationSound();
            }
        }
    }

    void PlayDeactivationSound()
    {
        if (!soundPlayed && deactivationSound != null)
        {
            AudioSource.PlayClipAtPoint(deactivationSound, objectToDeactivate.transform.position);
            soundPlayed = true;
        }
    }
}
