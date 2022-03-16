using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableDisablePanel : MonoBehaviour
{
    public GameObject[] panels;

    public void ActivatePanel(int panelIndex)
    {
        foreach (GameObject panel in panels)
        {
            if (panels[panelIndex] == panel)
            {
                panel.SetActive(true);
            }
            else
            {
                panel.SetActive(false);
            }

        }
    }
}
