using UnityEngine;
using System.Collections;

public class QuitOnClick : MonoBehaviour
{
        public void Quit()
    {
#if     UNITY_EDITOR
        AkSoundEngine.PostEvent("UI_Cancel", gameObject);
        UnityEditor.EditorApplication.isPlaying = false;


#else
        AkSoundEngine.PostEvent("UI_Cancel", gameObject);
        Application.Quit ();
#endif
    }

}