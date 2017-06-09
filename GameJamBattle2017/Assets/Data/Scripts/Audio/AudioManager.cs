
// Auteur : Vincent Dargis
// Edite : 30 mai 2017


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	private Transform avatarTr;
	private Animator animator;
	private bool crouch;
	private bool running;

	// Use this for initialization
	void Start () 
	{
		animator = GetComponent<Animator> ();

	}
	
	// Update is called once per frame
	void Update ()
	{
		//crouch = MainManager.Instance.GetAvatarController.IsCrouch;    //Get le bon path pour l'avatar.
		//running = MainManager.Instance.GetAvatarController.IsRunning;  //Same

		if(crouch)
		{
			AkSoundEngine.SetState ("Speed_State", "Crouched");
		}
		if(running)
		{
			AkSoundEngine.SetState ("Speed_State", "Running");
		}
		if(!running && !crouch)
		{
			AkSoundEngine.SetState ("Speed_State", "Walking");	
		}

	}

	void PlaySound(string input)
	{
		AkSoundEngine.PostEvent (input, gameObject);
	}
}
