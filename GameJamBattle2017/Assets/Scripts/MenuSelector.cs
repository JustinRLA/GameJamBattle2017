using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class MenuSelector : MonoBehaviour
    {

        public GameObject MainPanel;
        public GameObject WinPanel;
        public GameObject LosePanel;

        // Use this for initialization
        void Start()
        {
            if (ApplicationModel.menuState == 0)
            {
                MainPanel.SetActive(true);
                WinPanel.SetActive(false);
                LosePanel.SetActive(false);
            }
            else if (ApplicationModel.menuState == 1)
            {
                MainPanel.SetActive(false);
                WinPanel.SetActive(true);
                LosePanel.SetActive(false);
            }
            else if (ApplicationModel.menuState == 2)
            {
                MainPanel.SetActive(false);
                WinPanel.SetActive(false);
                LosePanel.SetActive(true);
            }
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
