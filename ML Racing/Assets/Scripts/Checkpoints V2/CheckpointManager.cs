using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CheckpointManager : MonoBehaviour
{
    public float MaxTimeToReachNextCheckpoint = 30f;
    public float TimeLeft = 30f;
    //public int numberOfLaps;
    public int currentLapNum;
    public int carPositionInRace;

    public int totalCarNum;
    public AICarAgent aiAgent;
    public Checkpoint nextCheckPointToReach;
    public TextMeshProUGUI carpositionText;
    public Checkpoints _checkpoints;
    private int CurrentCheckpointIndex;
    private List<Checkpoint> Checkpoints;
    private Checkpoint lastCheckpoint;
    
    //public event Action<Checkpoint> reachedCheckpoint;

    void Awake()
    {
        totalCarNum = GameObject.FindGameObjectsWithTag("Car").Length;
        Checkpoints = FindObjectOfType<Checkpoints>().checkPoints;
        _checkpoints = FindObjectOfType<Checkpoints>();
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
        //reachedCheckpoint?.Invoke(checkpoint);
        CurrentCheckpointIndex++;

        if (CurrentCheckpointIndex >= Checkpoints.Count)
        {
            if (currentLapNum >= _checkpoints.numberOfLaps)
            {
                _checkpoints.raceFinished = true;
                Debug.Log("Finished");
                aiAgent.AddReward(5f);

                ResetCheckpoints();
                ResetPositions();

            }
            else
            {
                Debug.Log("Lap");
                aiAgent.AddReward(2.5f);
                currentLapNum++;
                //aiAgent.EndEpisode();
                ResetCheckpoints();
                ResetPositions();
            }
        }
        else
        {
            aiAgent.AddReward((0.5f) / Checkpoints.Count);
            Debug.Log("Checkpoint");
            SetNextCheckpoint();
            Checkpoints[CurrentCheckpointIndex].UpdatePositions(this);
            //carpositionText.text = carPositionInRace.ToString();

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

    private void ResetPositions()
    {
        foreach (Checkpoint checkpoint in Checkpoints)
        {
            checkpoint.carsPassedThrough = 0;
        }
    }
}
