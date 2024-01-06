using UnityEngine;
using UnityEngine.SceneManagement;

public class ARUIManager : MonoBehaviour
{
    int sceneNumber;
    [SerializeField]
    private GameObject exitMenu;
    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        DisableExitMenu();
    }
   
    public void Back()
    {
        //SceneManager.LoadScene(2);
        sceneNumber = 2;
        EnableExitMenu();
    }

    public void EnterUserMenu()
    {
        //SceneManager.LoadScene(4);
        sceneNumber = 4;
        EnableExitMenu();
    }

    public void Refresh()
    {    
        SceneManager.LoadScene(3);
    }

    public void EnableExitMenu()
    {
        exitMenu.SetActive(true);
    }

    public void DisableExitMenu()
    {
        exitMenu.SetActive(false);
    }

    public void EnterExit()
    {
        SceneManager.LoadScene(sceneNumber);
    }
}
