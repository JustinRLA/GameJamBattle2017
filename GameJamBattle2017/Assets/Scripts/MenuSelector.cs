using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSelector : MonoBehaviour {

    public GameObject MainPanel;
    public GameObject WinPanel;
    public GameObject LosePanel;

    public int menuState = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void SetMenu(int menuState)
    {
        if (menuState == 0)
        {
            MainPanel.SetActive(true);
            WinPanel.SetActive(false);
            LosePanel.SetActive(false);
        }
        else if (menuState == 1)
        {
            MainPanel.SetActive(false);
            WinPanel.SetActive(false);
            LosePanel.SetActive(false);
        }
        else if (menuState == 2)
        {
            MainPanel.SetActive(false);
            WinPanel.SetActive(false);
            LosePanel.SetActive(false);
        }
    }
}
