using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartManager : MonoBehaviour
{
    [SerializeField] private float maxTimeToStart;
    //[SerializeField]private float timeLeftToStart;
    [SerializeField] private AICarAgent[] aiCars;

    private bool raceStarted;
    void Start()
    {
        StartCoroutine(StartRace());
    }

    IEnumerator StartRace()
    {
        // suspend execution for 5 seconds
        yield return new WaitForSeconds(maxTimeToStart - 3);

        yield return new WaitForSeconds(1);
        
        yield return new WaitForSeconds(1);
        
        yield return new WaitForSeconds(1);
        foreach(AICarAgent aicars in aiCars)
        {
            aicars.enabled = true;
        }
    }
}
