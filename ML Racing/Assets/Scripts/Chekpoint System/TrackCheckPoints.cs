using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCheckPoints : MonoBehaviour
{
    [SerializeField] private Transform[] Checkpoints;

    private int currentCheckpoint;

    [HideInInspector] public GameObject collidedCar;
    [HideInInspector] public GameObject collidedCheckpoint;

    

    public Transform GetFirstCheckpoint()
    {
        return Checkpoints[0];
    }
    public Transform GetNextCheckpoint(AICarAgent aICar)
    {
        
        //++currentCheckpoint;
        currentCheckpoint = ++aICar.currentActiveCheckpointIndex;
        if (currentCheckpoint < (Checkpoints.Length - 1))
        {
            // if (Checkpoints[currentCheckpoint].gameObject.activeSelf == true)
            return Checkpoints[currentCheckpoint];
            // else
            //     return GetNextCheckpoint();
        }
        else
        {
            return Checkpoints[Checkpoints.Length - 1];
        }

    }
    public Transform GetCurrentCheckpoint()
    {
        return Checkpoints[currentCheckpoint];
    }

    public void ResetCheckpoints(AICarAgent aICar)
    {
        currentCheckpoint = aICar.currentActiveCheckpointIndex = 0;
        // foreach (Transform checkpoint in Checkpoints)
        // {
        //     checkpoint.gameObject.SetActive(true);
        // }
        
    }

    public void SetColObject(GameObject colObject, GameObject checkpointObj)
    {
        collidedCar = colObject;
        collidedCheckpoint = checkpointObj;

    }

}
