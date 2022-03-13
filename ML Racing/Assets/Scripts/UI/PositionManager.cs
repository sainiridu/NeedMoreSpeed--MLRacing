using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PositionManager : MonoBehaviour
{
    public CheckpointManager playerCarCheckpointManager;
    [SerializeField]private TextMeshProUGUI position_Number;
    [SerializeField]private TextMeshProUGUI total_Car_Number;

    private GameObject[] carList;

    private int carNum;


    void Start()
    {
        carList = GameObject.FindGameObjectsWithTag("Car");
        carNum = carList.Length;
        total_Car_Number.text = carNum.ToString();
    }

    void LateUpdate()
    {
        position_Number.text = playerCarCheckpointManager.carPositionInRace.ToString();
    }
}
