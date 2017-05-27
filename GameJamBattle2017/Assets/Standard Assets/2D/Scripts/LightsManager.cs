using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsManager : MonoBehaviour {

    public List<LightBulbe> lights = new List<LightBulbe>();

    // Use this for initialization
    void Start () {
        var temps = FindObjectsOfType<LightBulbe>();
        foreach (var temp in temps)
        {
            lights.Add(temp.GetComponent<LightBulbe>());
            lights[lights.Count - 1].manager = this;
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
        Debug.Log("All are lit!");
    }
}
