using System.Collections.Generic;
using UnityEngine;

public class Checkpoints : MonoBehaviour
{
    [HideInInspector] public int numberOfLaps = 1;

    public bool raceFinished;
    public List<Checkpoint> checkPoints;

    private StartManager startManager;

    private void Awake()
    {
        startManager = FindObjectOfType<StartManager>();
        numberOfLaps = startManager.totalNumberOfLaps;
        checkPoints = new List<Checkpoint>(GetComponentsInChildren<Checkpoint>());
    }
}