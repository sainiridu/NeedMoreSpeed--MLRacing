using System.Collections.Generic;
using UnityEngine;

public class Checkpoints : MonoBehaviour
{
    public int numberOfLaps = 1;

    public bool raceFinished;
    public List<Checkpoint> checkPoints;

    private void Awake()
    {
        checkPoints = new List<Checkpoint>(GetComponentsInChildren<Checkpoint>());
    }
}