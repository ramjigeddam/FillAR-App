using UnityEngine;
using GLTFast;
using UnityEngine.SceneManagement;
public class LoadModelContent : MonoBehaviour
{
	GameObject model;
	public static LoadModelContent instanceModel;

    private Scene scene;

    private void Start()
	{
        scene = SceneManager.GetActiveScene();
        instanceModel = this;
	}
	
    public 	void GetModel_Info(string path,string modelID, Vector3 _position, Vector3 _rotation, Vector3 _scale)
	{				
		LoadModelFast(path,  modelID, _position, _rotation, _scale);
		
	}

	async void LoadModelFast(string path, string modelID, Vector3 _position, Vector3 _rotation, Vector3 _scale)
	{
		model = new GameObject();
		model.name = "Downloade Mesh";
		model.transform.localScale = Vector3.one;
        model.AddComponent<GltfAsset>();
		var success = await model.GetComponent<GltfAsset>().Load(path);

		if (success)
		{
            GameObject rootObject = new GameObject();
            rootObject.name = "rootObject";
            rootObject.transform.SetParent(GameObject.Find("TrackerParent").transform);
            rootObject.transform.localPosition = Vector3.zero;
            rootObject.transform.localRotation = Quaternion.Euler(0,0,0);
            rootObject.transform.localScale = new Vector3(1f, 1f, 1f);

            model.transform.SetParent(rootObject.transform);

            _rotation.y = -_rotation.y;
            _rotation.z = -_rotation.z;

            Quaternion qZ = Quaternion.Euler(0, 0, _rotation.z);
            Quaternion qX = Quaternion.Euler(_rotation.x, 0, 0); 
            Quaternion qY = Quaternion.Euler(0, _rotation.y, 0);
            Quaternion finalRotation = qZ * qX * qY;

			if (scene.name == "05Vuforia")
			{
                model.transform.localPosition = new Vector3(-_position.x, _position.y, _position.z);
            }
			else
			{
                model.transform.localPosition = new Vector3(_position.x, _position.y, _position.z);
            }
            model.transform.localRotation = finalRotation;
			model.transform.localScale = new Vector3(_scale.x, _scale.y, _scale.z);

        }
		else
		{
			Debug.LogError("Loading glTF failed!");
		}

		FindObjectOfType<ContentManager>().modelCount++;
		FindObjectOfType<ContentManager>().assets_Count++;

		if (FindObjectOfType<ContentManager>().assets_Count == FindObjectOfType<ContentManager>().totalNumber_of_assets)
		{
			Debug.Log("Downloaded All Assets : ");
			//GameObject.Find("TrackerParent").gameObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
		}
		if (FindObjectOfType<ContentManager>().modelCount < FindObjectOfType<ContentManager>().numberOfModels)
		{
			FindObjectOfType<ContentManager>().DownlodModel();
		}
	}

}
