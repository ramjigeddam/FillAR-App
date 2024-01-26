using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;


public class LoadImageContent : MonoBehaviour
{

	public static LoadImageContent instanceImage;
	GameObject image;
	Texture imageTexture;
	public bool imageLoad=false;
    private Scene scene;

    private void Start()
    {
        scene = SceneManager.GetActiveScene();
        instanceImage = this;
	}
    public void GetImageDetails(string url, string imageID, Vector3 imagePos, Vector3 imageRot, Vector3 imageScal)
	{
		StartCoroutine(LoadImages(url, imageID, imagePos, imageRot, imageScal));
	}
    public IEnumerator LoadImages(string url, string imageID, Vector3 imagePos, Vector3 imageRot, Vector3 imageScal)
	{
        image = (GameObject)Instantiate(Resources.Load("Image", typeof(GameObject)));

        if (url != "")
		{
			UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(url);
			uwr.SendWebRequest();

			while (!uwr.isDone)
			{
				yield return 1;
			}
			if (uwr.isDone)
			{
				 imageTexture = ((DownloadHandlerTexture)uwr.downloadHandler).texture;				
			}
			if (uwr.result == UnityWebRequest.Result.ConnectionError)
			{
                Debug.Log(uwr.error);
			}
			uwr.Dispose();
			image.transform.GetComponent<Renderer>().material.mainTexture = imageTexture;
		}

		image.name = imageID;

        GameObject rootObject = new GameObject();
        GameObject downloadeImage = new GameObject();
        rootObject.name = "ImageRootObject";
        downloadeImage.name = "DownloadeImage";
        rootObject.transform.SetParent(GameObject.Find("TrackerParent").transform);
        rootObject.transform.localPosition = Vector3.zero;
        rootObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
        rootObject.transform.localScale = new Vector3(1f, 1f, 1f);
        downloadeImage.transform.SetParent(rootObject.transform);

		downloadeImage.transform.localPosition = Vector3.zero;
		downloadeImage.transform.localRotation = Quaternion.Euler(Vector3.zero);
        downloadeImage.transform.localScale = Vector3.one;

        imageRot.y = -imageRot.y;
        imageRot.z = -imageRot.z;

        Quaternion qX = Quaternion.Euler(imageRot.x, 0, 0);
        Quaternion qY = Quaternion.Euler(0, imageRot.y, 0);
        Quaternion qZ = Quaternion.Euler(0, 0, imageRot.z);
        Quaternion finalRotation = qZ * qX * qY;

        if (scene.name == "05Vuforia")
        {
            downloadeImage.transform.localPosition = new Vector3(-imagePos.x, imagePos.y, imagePos.z);
        }
        else
        {
            downloadeImage.transform.localPosition = new Vector3(-imagePos.x, imagePos.y, imagePos.z);
        }

        downloadeImage.transform.localRotation = finalRotation;
        downloadeImage.transform.localScale = new Vector3(imageScal.x / 10, imageScal.y / 10, imageScal.z / 10);

        image.transform.SetParent(downloadeImage.transform);
        image.transform.localPosition = Vector3.zero;
        image.transform.localRotation = Quaternion.Euler(90,0,0);
        image.transform.localScale = Vector3.one;

        downloadeImage.AddComponent<ObjectZoomAndRotate>();

        FindObjectOfType<ContentManager>().imageCount++;
		FindObjectOfType<ContentManager>().assets_Count++;
        if (FindObjectOfType<ContentManager>().assets_Count== FindObjectOfType<ContentManager>().totalNumber_of_assets)
        {
			Debug.Log("Downloaded All Assets : ");
		}
		if (FindObjectOfType<ContentManager>().imageCount < FindObjectOfType<ContentManager>().numberOfImages)
        {
			FindObjectOfType<ContentManager>().DownlodImage();
		}
	}
}
