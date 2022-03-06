using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;


public class AICarAgent : Agent
{
    [SerializeField] private Transform carSphereTransform;

    public CheckpointManager _checkpointManager;

    private CarController carController;

    private float horizontalInput;
    private float verticalInput;

    public int currentActiveCheckpointIndex;
    private Transform currentActiveCheckpoint;

    private Vector3 defaultSpherePos;
    private Quaternion defaultSphereRot;

    private void Awake()
    {
        carController = GetComponent<CarController>();
        defaultSpherePos = carSphereTransform.localPosition;
        defaultSphereRot = this.transform.localRotation;

    }

    public override void OnEpisodeBegin()
    {

        carSphereTransform.localPosition = defaultSpherePos;
        this.transform.localRotation = defaultSphereRot;
        _checkpointManager.ResetCheckpoints();


    }
    public override void CollectObservations(VectorSensor sensor)
    {

        Vector3 diff = _checkpointManager.nextCheckPointToReach.transform.position - transform.position;
        sensor.AddObservation(diff / 20f);
        //sensor.AddObservation(currentActiveCheckpoint.position);
        AddReward(-0.001f);
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        verticalInput = actions.ContinuousActions[0];
        horizontalInput = actions.ContinuousActions[1];

        if(verticalInput > 0)
        verticalInput = 1;
        else if(verticalInput < 0)
        verticalInput = -1 ;
        carController.SetInputs(verticalInput, horizontalInput);

    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxis("Vertical");
        continuousActions[1] = Input.GetAxis("Horizontal");
    }


    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.TryGetComponent<Checkpoint>(out Checkpoint checkpoint))
    //     {
    //         if (other.transform == currentActiveCheckpoint)

    //             if (this.gameObject == trackCheckPoints.collidedCar && checkpoint.gameObject == trackCheckPoints.collidedCheckpoint)
    //             {
    //                 currentActiveCheckpoint = trackCheckPoints.GetNextCheckpoint(this);
    //                 AddReward(+0.1f);
    //                 //Debug.Log(other.transform.name + " " + currentActiveCheckpoint.name + " " + this.transform.name);
    //                 Debug.Log("Checkpoint");
    //             }
    //             //  else if (this.gameObject == trackCheckPoints.collidedCar && !checkpoint.gameObject == trackCheckPoints.collidedCheckpoint)
    //             //  {
    //             //      AddReward(-0.1f);
    //             //  }

    //     }


    //     else if (other.TryGetComponent<FinishLine>(out FinishLine finishLine1))
    //     {

    //         trackCheckPoints.ResetCheckpoints(this);
    //         //carController.StopCompletely();
    //         Debug.Log("Finish" + this.transform.name);
    //         AddReward(+5f);
    //         EndEpisode();
    //     }

    // }
    // private void OnCollisionEnter(Collision collision)
    // {

    //     if (collision.gameObject.TryGetComponent<Wall>(out Wall wall))
    //     {
    //         Debug.Log("Wall");
    //         AddReward(-0.1f);
    //         //EndEpisode();
    //     }
    // }

    // // private void OnCollisionStay(Collision collisionInfo)
    // // {
    // //     if (collisionInfo.gameObject.TryGetComponent<Wall>(out Wall wall))
    // //     {
    // //         Debug.Log("WallStay");
    // //         AddReward(-0.0001f);
    // //         //EndEpisode();
    // //     }
    // // }
}
