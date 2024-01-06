using Lean.Touch;
using UnityEngine;
using Lean.Common;
public class RuntimeSelection : MonoBehaviour
{
    private void Update()
    { 
        if (Input.GetMouseButtonDown(0)) 
        {
            HandleSelection();
        }
    }

    private void HandleSelection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            LeanSelectable[] allLeanTouch = FindObjectsOfType<LeanSelectable>();

            LeanSelectable leanSelectable = hit.collider.GetComponent<LeanSelectable>();

           
            foreach (LeanSelectable obj in allLeanTouch)
            {               
                obj.SelfSelected = false;
            }

            if (leanSelectable != null)
            {
                if (!leanSelectable.IsSelected)
                {
                    leanSelectable.SelfSelected = true;
                }
                else
                {
                    leanSelectable.SelfSelected = false;
                   // leanSelectable.Deselect();
                }
            }
        }
    }
}
