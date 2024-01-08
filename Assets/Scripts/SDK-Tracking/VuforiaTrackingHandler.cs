using UnityEngine;
using Vuforia;
using UnityEngine.UI;

public class VuforiaTrackingHandler : DefaultObserverEventHandler
{
    [SerializeField]
    GameObject trackingParent;
    //[SerializeField]
   // GameObject contentManager;
   
    public Text sampleText;
    private void Awake()
    {
        Debug.Log("here Added");
        
        sampleText = GameObject.Find("DebugText").gameObject.GetComponent<Text>();

        trackingParent= GameObject.Find("TrackerParent").gameObject;
       // contentManager= GameObject.Find("ContentManager").gameObject;
    }
    protected override void OnTrackingFound()
    {
        // SetAugmentationRendering(true);
        OnTargetFound?.Invoke();

        Debug.Log("Found");

       
        sampleText.text = "Found...";

        trackingParent.transform.SetParent(transform);
        trackingParent.transform.localPosition = Vector3.zero;
        trackingParent.transform.rotation = Quaternion.Euler(Vector3.zero);
        trackingParent.transform.localScale = Vector3.one;

        FindObjectOfType<AreaTargetLoader>().contentManager.SetActive(true);

    }

    protected override void OnTrackingLost()
    {
        // SetAugmentationRendering(false);
        OnTargetLost?.Invoke();

        Debug.Log("Loss");

        //cube.SetActive(false);
       
        sampleText.text = "Loss...";

       
    }
}
