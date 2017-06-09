using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicController : MonoBehaviour {

	// Use this for initialization
	void Start () {

		Scene scene = SceneManager.GetActiveScene ();
		string myScene = scene.name;

		switch (myScene) 
		{
		case "scene01_LA":
			AkSoundEngine.PostEvent ("Play_AmbianceLevel01", gameObject);
			break;
		
		case "scene02_LA":
			AkSoundEngine.PostEvent ("Play_AmbianceLevel02", gameObject);
			break;

		case "scene03_LA":
			AkSoundEngine.PostEvent ("Play_AmbianceLevel03", gameObject);
			break;

		case "scene04_LA":
			AkSoundEngine.PostEvent ("Play_AmbianceLevel04", gameObject);
			break;

		case "scene05_LA":
			AkSoundEngine.PostEvent ("Play_AmbianceLevel05", gameObject);
			break;

		case "scene06_LA":
			AkSoundEngine.PostEvent ("Play_AmbianceLevel06", gameObject);
			break;

		case "scene07_LA":
			AkSoundEngine.PostEvent ("Play_AmbianceLevel07", gameObject);
			break;

		case "scene08_LA":
			AkSoundEngine.PostEvent ("Play_AmbianceLevel08", gameObject);
			break;

		case "scene09_LA":
			AkSoundEngine.PostEvent ("Play_AmbianceLevel09", gameObject);
			break;

		case "scene01_LD":
			AkSoundEngine.PostEvent ("Play_AmbianceLevel01", gameObject);
			break;

		case "scene02_LD":
			AkSoundEngine.PostEvent ("Play_AmbianceLevel02", gameObject);
			break;

		case "scene03_LD":
			AkSoundEngine.PostEvent ("Play_AmbianceLevel03", gameObject);
			break;

		case "scene04_LD":
			AkSoundEngine.PostEvent ("Play_AmbianceLevel04", gameObject);
			break;

		case "scene05_LD":
			AkSoundEngine.PostEvent ("Play_AmbianceLevel05", gameObject);
			break;

		case "scene06_LD":
			AkSoundEngine.PostEvent ("Play_AmbianceLevel06", gameObject);
			break;

		case "scene07_LD":
			AkSoundEngine.PostEvent ("Play_AmbianceLevel07", gameObject);
			break;

		case "scene08_LD":
			AkSoundEngine.PostEvent ("Play_AmbianceLevel08", gameObject);
			break;

		case "scene09_LD":
			AkSoundEngine.PostEvent ("Play_AmbianceLevel09", gameObject);
			break;

		case "Vincent_Gym":
			AkSoundEngine.PostEvent ("Play_AmbianceLevel09", gameObject);
			break;

		default:
			break;
		}
	
			
	}

}
