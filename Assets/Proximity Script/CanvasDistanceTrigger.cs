using System.Diagnostics;
using UnityEngine;

public class CanvasDistanceTrigger : MonoBehaviour
{
    public GameObject canvas;
    public float activationDistance = 3f;

    private bool canvasActive = false;
    public GameObject player;

    void Start()
    {
        if (canvas == null)
        {
            UnityEngine.Debug.LogError("Canvas is not assigned in the inspector!");
            return;
        }

        canvas.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= activationDistance)
        {
            //UnityEngine.Debug.Log("Dis: " + Vector3.Distance(transform.position, player.transform.position));
            if (!canvasActive)
            {
                canvas.gameObject.SetActive(true);
                canvasActive = true;
            }
        }
        else
        {
            if (canvasActive)
            {
                canvas.gameObject.SetActive(false);
                canvasActive = false;
            }
        }
    }
}
