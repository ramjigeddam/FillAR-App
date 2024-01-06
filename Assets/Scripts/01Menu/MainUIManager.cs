using UnityEngine;
using UnityEngine.SceneManagement;

public class MainUIManager : MonoBehaviour
{
    void Start()
    {
        
    }
    public void SubmitButton()
    {
        SceneManager.LoadScene(2);
    }
}
