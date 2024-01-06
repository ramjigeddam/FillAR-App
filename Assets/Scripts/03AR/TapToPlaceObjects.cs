using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class TapToPlaceObjects : MonoBehaviour
{
   
    public GameObject spawnedObject;
    public GameObject contentObject;

    ARPlaneManager m_ARPlaneManager;
    ARRaycastManager m_RaycastManager;

    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

    [SerializeField]
    GameObject bottomInfo;

    bool userInfo = false;
 
    void Awake()
    {
        m_ARPlaneManager = GetComponent<ARPlaneManager>();
        m_RaycastManager = GetComponent<ARRaycastManager>();
        Application.targetFrameRate = 30;

        bottomInfo.SetActive(true);

        userInfo = false;
    }
   

    void Update()
    {
        if (m_ARPlaneManager.trackables.count>1 && !userInfo)
        {
            ARManager.aRManager.TapToPlaceText();
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began && !userInfo)
            {
                if (m_RaycastManager.Raycast(touch.position, s_Hits, TrackableType.PlaneWithinPolygon))
                {
                    Pose hitPose = s_Hits[0].pose;

                    // var objectGltf = Instantiate(spawnedObject, hitPose.position, Quaternion.identity);

                    contentObject.SetActive(true);

                    spawnedObject.transform.localPosition = hitPose.position;
                    spawnedObject.transform.localRotation = hitPose.rotation;

                    StoptPlaneDetection();
                }
                userInfo = true;
                //bottomInfo.SetActive(false);
            }
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
