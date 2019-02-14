using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public Text studentDisplay;

    public void Start()
    {
        
    }

    public void GoToRegister()
    {
        //Go to Register Screen.
        //1 corrispond to the build order of the scenes, which can be found in File > BuildSettings
        SceneManager.LoadScene(1);
    }

    public void GoToLogin()
    {
        SceneManager.LoadScene(2);
    }
}
