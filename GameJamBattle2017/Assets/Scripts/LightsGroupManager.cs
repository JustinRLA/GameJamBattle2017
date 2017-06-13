using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsGroupManager : MonoBehaviour {

    public List<LightBulbe> lights;
    public List<MovingPlatform> platforms;

    // Use this for initialization
    void Start()
    {
        if(lights == null || lights.Count <= 0)
        {
            return;
        }

        foreach (var light in lights)
        {
            light.groupManager = this;
        }

    }

    public void CheckAllLightsAreLit()
    {
        foreach (var light in lights)
        {
            if (!light.plugged)
            {
                return;
            }
        }
        Debug.Log("All group is lit!");
        allLit = true;

        foreach (var platform in platforms)
        {
            platform.useWaypoint = true;
        }
    }

    public bool allLit = false;
}
