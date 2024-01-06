using System.Collections;
using System.IO;
using UnityEngine;
using Vuforia;

public class AreaTargetLoader : MonoBehaviour
{
    GameObject cube;
  
    string dataSetPath = Application.streamingAssetsPath + "Vuforia/my-home.xml";
    string targetName = "my-home";
    string datasetXmlPath;

    // Start is called before the first frame update
    void Start()
    {
        VuforiaApplication.Instance.OnVuforiaInitialized += OnVuforiaInitialized;
    }

    void OnVuforiaInitialized(VuforiaInitError error)
    {


        string datasetPath = Application.persistentDataPath + "/";
        FileInfo[] files = new DirectoryInfo(datasetPath).GetFiles("*.xml");

        if (files.Length > 0)
        {
            datasetXmlPath = Path.Combine(datasetPath, files[0].Name);
        }
        else
        {
            Debug.LogError("No XML files found in the Dataset folder.");
        }



        if (error == VuforiaInitError.NONE)
            OnVuforiaStarted();
    }

    // Load and activate a data set at the given path.
    void OnVuforiaStarted()
    {
        // Create an Image Target from the database.
        var mImageTarget = VuforiaBehaviour.Instance.ObserverFactory.CreateAreaTarget(
            datasetXmlPath,
            targetName);
        mImageTarget.OnTargetStatusChanged += OnTargetStatusChanged;
    }

    void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus status)
    {
        Debug.Log("target status:::::::::"+status.Status+" ::: "+behaviour);
    }


}
