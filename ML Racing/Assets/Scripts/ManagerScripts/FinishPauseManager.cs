using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinishPauseManager : MonoBehaviour
{
    private Checkpoints checkpoints;

    private PositionManager positionManager;

    private CarEngineSound[] carEngineSounds;

    [SerializeField] private TextMeshProUGUI headerText;
    [SerializeField] private TextMeshProUGUI paragraphText;

    [SerializeField] private GameObject raceFinishCanvas;

    private bool menuVisible = false;
    private bool isPaused = false;

    void Start()
    {
        checkpoints = FindObjectOfType<Checkpoints>();
        positionManager = FindObjectOfType<PositionManager>();
        carEngineSounds = FindObjectsOfType<CarEngineSound>();
    }
    void Update()
    {
        if (!menuVisible && checkpoints.raceFinished)
        {
            headerText.text = "FINISHED";
            paragraphText.text = "YOUR POSITION \n " + positionManager.playerCarCheckpointManager.carPositionInRace.ToString();
            raceFinishCanvas.SetActive(true);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            
            menuVisible = true;
            Time.timeScale = 0.01f;
            foreach (CarEngineSound carEngineSound in carEngineSounds)
            {
                carEngineSound.audioSource.Stop();
            }

        }

        if (Input.GetKeyDown(KeyCode.Escape) && !checkpoints.raceFinished)
        {
            isPaused = !isPaused;
        }

        if (isPaused && !menuVisible && !checkpoints.raceFinished)
        {
            headerText.text = "PAUSED";
            paragraphText.text = " ";
            raceFinishCanvas.SetActive(true);
            menuVisible = true;
            Time.timeScale = 0.0f;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if (!isPaused && menuVisible && !checkpoints.raceFinished)
        {
            raceFinishCanvas.SetActive(false);
            menuVisible = false;
            Time.timeScale = 1.0f;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}