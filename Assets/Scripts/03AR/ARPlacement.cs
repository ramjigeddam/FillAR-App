using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARPlacement : MonoBehaviour
{

    public GameObject arObjectToSpawn;
    public GameObject placementIndicator;
    public  GameObject contentObject;
    private Pose PlacementPose;
    private ARRaycastManager aRRaycastManager;
    ARPlaneManager m_ARPlaneManager;
    private bool placementPoseIsValid = false;
    private GameObject reticle;
    bool userInfo = false;
    void Start()
    {
        m_ARPlaneManager = GetComponent<ARPlaneManager>();
        aRRaycastManager = FindObjectOfType<ARRaycastManager>();

        userInfo = false;
        reticle = Instantiate(placementIndicator);
        reticle.SetActive(false);
    }

    // need to update placement indicator, placement pose and spawn 
    void Update()
    {
      
        if (placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            ARPlaceObject();
        }

        UpdatePlacementPose();
        UpdatePlacementIndicator();

    }
    void UpdatePlacementIndicator()
    {
       
      
        if (!userInfo && placementPoseIsValid)
        {

            reticle.transform.SetPositionAndRotation(PlacementPose.position, PlacementPose.rotation);
            reticle.SetActive(true);
        }
        else
        {
            reticle.SetActive(false);
            if (userInfo) return;
        }
    }

    void UpdatePlacementPose()
    {
        if (userInfo) return;

        var screenCenter = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        aRRaycastManager.Raycast(screenCenter, hits, TrackableType.Planes);

        placementPoseIsValid = hits.Count > 0;

        if (placementPoseIsValid)
        {
           
            PlacementPose = hits[0].pose;
        }
        
    }
    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();
    void ARPlaceObject()
    {      
        if (!userInfo)
        {
            contentObject.SetActive(true);  
            arObjectToSpawn.transform.localPosition = PlacementPose.position;
            arObjectToSpawn.transform.localRotation = Quaternion.Euler(0, 0, 0);
            StoptPlaneDetection();
            userInfo = true;
        }
    }

    public void StoptPlaneDetection()
    {
        foreach (var plane in m_ARPlaneManager.trackables)
        {
            plane.gameObject.SetActive(false);
        }
        GameObject.FindObjectOfType<ARPlaneManager>().enabled = false;

        ARManager.aRManager.ClearInfo();
    }
}


