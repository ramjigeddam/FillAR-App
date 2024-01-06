using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SplashMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("LoadLoginMenu", 3.5f); 
    }

    public void LoadLoginMenu()
    {
        SceneManager.LoadScene(1);
    }
}
