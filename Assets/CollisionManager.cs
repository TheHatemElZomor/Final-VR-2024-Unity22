using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    public GameObject[] platforms;
    public GameObject[] books;
    private int booksOnCorrectPlatforms = 0;
    private bool doorUnlocked = false;

    void Update()
    {
        // If all books are on correct platforms, print the message
        if (!doorUnlocked && booksOnCorrectPlatforms == books.Length)
        {
            Debug.Log("DOOR UNLOCKED");
            doorUnlocked = true; // Ensure the message is printed only once
        }

        // Debug log to track the number of books on correct platforms
        Debug.Log("Books on correct platforms: " + booksOnCorrectPlatforms);
    }

    void OnTriggerEnter(Collider other)
    {
        // Check trigger enter between platforms and books
        for (int i = 0; i < platforms.Length; i++)
        {
            for (int j = 0; j < books.Length; j++)
            {
                if (other.gameObject == platforms[i] && other.gameObject == books[j])
                {
                    booksOnCorrectPlatforms++;
                    Debug.Log("Book " + (j + 1) + " is on the correct platform.");
                    break;
                }
            }
        }
    }
}
