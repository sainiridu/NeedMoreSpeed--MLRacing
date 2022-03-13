using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishManager : MonoBehaviour
{
    private Checkpoints checkpoints;
    void Start()
    {
        checkpoints = FindObjectOfType<Checkpoints>();
    }
    void Update()
    {

    }
}