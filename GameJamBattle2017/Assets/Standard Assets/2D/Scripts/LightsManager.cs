using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LightsManager : MonoBehaviour {

    public List<LightBulbe> lights = new List<LightBulbe>();
    public int nextSceneIndex;

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
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        Debug.Log(SceneManager.sceneCount);
        if (SceneManager.sceneCountInBuildSettings > nextSceneIndex)
        {
            Debug.Log("Next scene");
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            ApplicationModel.menuState = 1;
            SceneManager.LoadScene("MainMenu");
        }
    }
}
