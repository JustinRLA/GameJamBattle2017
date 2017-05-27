using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBulbe : MonoBehaviour {

    public bool plugged
    {
        get
        {
            return _isPlugged;
        }
        set
        {

            _isPlugged = value;
            if (_isPlugged == true)
            {
                GetComponent<Renderer>().material.color = Color.yellow;
                manager.CheckAllLightsAreLit();
                if(groupManager != null)
                {
                    groupManager.CheckAllLightsAreLit();
                }
            }
            else
            {
                GetComponent<Renderer>().material.color = Color.black;
            }
        }
    }

    public LightsManager manager;
    public LightsGroupManager groupManager;
    private bool _isPlugged = false;
}
