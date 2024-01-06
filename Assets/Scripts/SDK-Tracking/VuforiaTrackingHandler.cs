using UnityEngine;

public class VuforiaTrackingHandler : MonoBehaviour
{
    [SerializeField]
    GameObject trackingParent;
    [SerializeField]
    GameObject contentManager;

    public void Start()
    {
        TrackingFound();
    }

    public void TrackingFound()
    {
        trackingParent.transform.SetParent(transform);
        contentManager.SetActive(true);
    }

    public void TrackingLost()
    {

    }
}
