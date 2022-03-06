using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEngineSound : MonoBehaviour
{
    [SerializeField] private AudioClip accelerateClip;
    [SerializeField] private float minPitch = 0.75f;
    [SerializeField] private float maxPitch = 1f;
    [SerializeField] private CarController carController;
    private AudioSource audioSource;




    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = accelerateClip;
        audioSource.pitch = minPitch;
        audioSource.Play();
    }


    void Update()
    {
        if (carController.GetAccelerationInput() != 0 && audioSource.pitch <= maxPitch)
            audioSource.pitch += 0.01f;

        else if (carController.GetAccelerationInput() == 0 && audioSource.pitch >= minPitch)
            audioSource.pitch -= 0.01f;

    }
}
