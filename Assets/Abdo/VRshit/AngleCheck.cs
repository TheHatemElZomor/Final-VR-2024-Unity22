using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialManager : MonoBehaviour
{
    public DialInteractable[] dialsToMonitor;

    void Update()
    {
        bool allAtTargetAngles = true;

        // Check if all dials are at their target angles
        foreach (var dial in dialsToMonitor)
        {
            if (!Mathf.Approximately(dial.CurrentAngle, dial.targetAngle))
            {
                allAtTargetAngles = false;
                break; // Exit loop early if any dial is not at target angle
            }
        }

        // Print result
        if (allAtTargetAngles)
        {
            Debug.Log("true");
        }
        else
        {
            Debug.Log("false");
        }
    }
}
