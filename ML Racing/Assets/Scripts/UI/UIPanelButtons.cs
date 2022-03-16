using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanelButtons : MonoBehaviour
{
    public void OpenTwitter()
    {
        Application.OpenURL("https://twitter.com/sainiridu");
    }
    public void OpenDisocrd()
    {
        Application.OpenURL("https://discord.gg/59mDZjxwgk");
    }
    public void OpenYouTube()
    {
        Application.OpenURL("https://www.youtube.com/c/GAMELAPSE");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
