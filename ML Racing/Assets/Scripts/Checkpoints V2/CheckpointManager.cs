using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public float MaxTimeToReachNextCheckpoint = 30f;
    public float TimeLeft = 30f;
    public int numberOfLaps;
    public int currentLapNum;
    public AICarAgent aiAgent;
    public Checkpoint nextCheckPointToReach;

    private int CurrentCheckpointIndex;
    private List<Checkpoint> Checkpoints;
    private Checkpoint lastCheckpoint;

    public event Action<Checkpoint> reachedCheckpoint;

    void Awake()
    {
        Checkpoints = FindObjectOfType<Checkpoints>().checkPoints;
        currentLapNum = 1;
        ResetCheckpoints();
    }

    public void ResetCheckpoints()
    {
        CurrentCheckpointIndex = 0;
        TimeLeft = MaxTimeToReachNextCheckpoint;

        SetNextCheckpoint();
    }

    private void Update()
    {
        TimeLeft -= Time.deltaTime;

        if (TimeLeft < 0f)
        {
            aiAgent.AddReward(-1f);
            aiAgent.EndEpisode();
        }
    }

    public void CheckPointReached(Checkpoint checkpoint)
    {
        if (nextCheckPointToReach != checkpoint) return;

        lastCheckpoint = Checkpoints[CurrentCheckpointIndex];
        reachedCheckpoint?.Invoke(checkpoint);
        CurrentCheckpointIndex++;

        if (CurrentCheckpointIndex >= Checkpoints.Count)
        {
            Debug.Log("Finish");
            aiAgent.AddReward(2.5f);
            //aiAgent.EndEpisode();
            ResetCheckpoints();
        }
        else
        {
            aiAgent.AddReward((0.5f) / Checkpoints.Count);
            Debug.Log("Checkpoint");
            SetNextCheckpoint();
        }
    }

    public void HitBarrier()
    {
        Debug.Log("Barrier");
        aiAgent.AddReward(-0.001f);
    }
    private void SetNextCheckpoint()
    {
        if (Checkpoints.Count > 0)
        {
            TimeLeft = MaxTimeToReachNextCheckpoint;
            nextCheckPointToReach = Checkpoints[CurrentCheckpointIndex];

        }
    }
}
