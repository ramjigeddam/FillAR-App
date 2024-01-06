using Lean.Touch;
using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    private Renderer modelRenderer;
    private void Start()
    {
        modelRenderer = GetComponent<Renderer>();
    }

    public void OnSelect(LeanFinger finger)
    {
        //Change the color of the model when it's selected
        modelRenderer.material.color = Color.red;
        Debug.Log("1");
    }

    private void OnSelectUpdate(LeanFinger finger)
    {
        if (finger.IsActive)
        {
            modelRenderer.material.color = Color.green;
        }
    }

    public void OnDisSelect(LeanFinger finger)
    {
        //Change the color of the model when it's selected
        modelRenderer.material.color = Color.blue;
        Debug.Log("2");
    }

    public void HHHH()
    {
        Debug.Log("2");
    }

    public void HHHsH()
    {
        Debug.Log("3");
    }
}
