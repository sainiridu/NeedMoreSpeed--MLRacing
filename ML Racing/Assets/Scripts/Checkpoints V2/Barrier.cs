using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
   private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<CheckpointManager>(out CheckpointManager checkpointManager))
        {
            checkpointManager.HitBarrier();
        }
    }
}
