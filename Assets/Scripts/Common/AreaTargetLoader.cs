using System.Collections;
using System.IO;
using UnityEngine;
using Vuforia;
using System.Collections.Generic;
using System.Reflection;
public class AreaTargetLoader : MonoBehaviour
{
    
    string targetName = "mydesk";//my-home//plugxr//Office
    

    
    public GameObject contentManager;

    private void Awake()
    {
        Caching.ClearCache();
    }

    // Start is called before the first frame update
    void Start()
    {     
        VuforiaApplication.Instance.OnVuforiaInitialized += OnVuforiaInitialized;
        Debug.Log("path :: "+Application.persistentDataPath);
    }

    void OnVuforiaInitialized(VuforiaInitError error)
    {
       /* string datasetPath = Application.persistentDataPath + "/";
        FileInfo[] files = new DirectoryInfo(datasetPath).GetFiles("*.xml");

        if (files.Length > 0)
        {
            datasetXmlPath = Path.Combine(datasetPath, files[0].Name);
        }
        else
        {
            Debug.LogError("No XML files found in the Dataset folder.");
        }*/
       
        if (error == VuforiaInitError.NONE)
            OnVuforiaStarted();
    }

    // Load and activate a data set at the given path.
    void OnVuforiaStarted()
    {
        
      /*  var mImageTarget = VuforiaBehaviour.Instance.ObserverFactory.CreateAreaTarget(
          datasetXmlPath,
          targetName);
        mImageTarget.OnTargetStatusChanged += OnTargetStatusChanged;*/


        
        string datasetXmlPath =  Application.persistentDataPath + "/" + targetName + ".xml";       
         var mImageTarget = VuforiaBehaviour.Instance.ObserverFactory.CreateAreaTarget(
          datasetXmlPath,
           targetName);     
        if (mImageTarget != null)
        {
            mImageTarget.OnTargetStatusChanged += OnTargetStatusChanged;
            Debug.Log(" here Area Target loaded successfully.");
        }
        else
        {
            Debug.LogError(" here Failed to load Area Target.");
        }
    }

    void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus status)
    {
        Debug.Log("here target status:::::::::"+status.Status+" ::: "+behaviour);
        GameObject.Find(targetName).AddComponent<VuforiaTrackingHandler>();
    }


    

}
