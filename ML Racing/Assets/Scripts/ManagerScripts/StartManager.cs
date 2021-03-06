using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartManager : MonoBehaviour
{
    [SerializeField] private float maxTimeToStart;
    //[SerializeField]private float timeLeftToStart;
    [SerializeField] private AICarAgent[] allCarsInScene;

    [SerializeField] private TextMeshProUGUI screen_Center_Text;

    [Range(1, 50)] public int totalNumberOfLaps;

    private UILookatScript[] uILookatScripts;

    private AudioSource startCoundownAudioSource;
    public bool raceStarted;

    void Awake()
    {
        Time.timeScale = 1.0f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }



    void Start()
    {
        startCoundownAudioSource = GetComponent<AudioSource>();

        uILookatScripts = FindObjectsOfType<UILookatScript>();
        foreach (UILookatScript overheadUI in uILookatScripts)
        {
            overheadUI.gameObject.SetActive(false);
        }
        screen_Center_Text.fontSize = 38;
        screen_Center_Text.text = "RACE STARTING SOON";
        StartCoroutine(StartRace());
    }

    IEnumerator StartRace()
    {
        // suspend execution for 5 seconds
        yield return new WaitForSeconds(maxTimeToStart - 3);
        startCoundownAudioSource.Play();
        screen_Center_Text.fontSize = 264;
        screen_Center_Text.text = "3";

        yield return new WaitForSeconds(1);

        screen_Center_Text.text = "2";

        yield return new WaitForSeconds(1);

        screen_Center_Text.text = "1";

        yield return new WaitForSeconds(2);

        screen_Center_Text.text = "GO";
        foreach (AICarAgent allCars in allCarsInScene)
        {
            allCars.enabled = true;
        }
        raceStarted = true;

        yield return new WaitForSeconds(2);
        screen_Center_Text.text = " ";
        foreach (UILookatScript overheadUI in uILookatScripts)
        {
            overheadUI.gameObject.SetActive(true);
        }
    }
}
