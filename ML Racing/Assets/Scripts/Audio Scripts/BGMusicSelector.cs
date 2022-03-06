using UnityEngine;

public class BGMusicSelector : MonoBehaviour
{
    public AudioClip[] tracks;

    private AudioSource audioSource;

    public int trackNum;
    public int TrackHistory;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        trackNum = Random.Range(0, tracks.Length);
        TrackHistory = trackNum;

        audioSource.clip = tracks[trackNum];
        audioSource.Play();

        Invoke("PlayNextSong", audioSource.clip.length + 1f);
    }

    public void PlayNextSong()
    {
        trackNum = Random.Range(0, tracks.Length);

        if (trackNum != TrackHistory)
        {
            audioSource.clip = tracks[trackNum];
            audioSource.Play();
            TrackHistory = trackNum;
            Invoke("PlayNextSong", audioSource.clip.length + 1f);
        }

        else
        {
            PlayNextSong();
        }
    }
}
