using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [HideInInspector] public int carsPassedThrough = 0;




    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CheckpointManager>() != null)
        {
            other.GetComponent<CheckpointManager>().CheckPointReached(this);

        }
    }

    public void UpdatePositions(CheckpointManager checkpointManager)
    {
        carsPassedThrough++;
        checkpointManager.carPositionInRace = carsPassedThrough;
        checkpointManager.carpositionText.text = carsPassedThrough.ToString();
    }
}
