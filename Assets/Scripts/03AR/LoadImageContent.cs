using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Lean.Touch;
using Lean.Common;
using UnityEngine.UIElements;
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
        image = (GameObject)Instantiate(Resources.Load("ImageMainParent", typeof(GameObject)));
        if (scene.name == "05Vuforia")
            image.transform.localScale = new Vector3(1f, 1f, 1f);
        else
            image.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);


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
				Debug.Log(uwr.error); // Popup notfication image not downloaded
			}
			uwr.Dispose();
			image.transform.GetChild(0).transform.GetChild(0).GetComponent<Renderer>().material.mainTexture = imageTexture;
		}

		image.name = imageID;
		image.transform.GetChild(0).transform.GetChild(0).transform.localPosition = imagePos;// new Vector3(imagePos.x, imagePos.y, -imagePos.z);
       

        image.transform.SetParent(GameObject.Find("TrackerParent").transform);
		image.transform.localPosition = new Vector3(-imagePos.x, imagePos.y, imagePos.z);
		image.transform.localRotation = Quaternion.Euler(0, 180, 0);

		image.transform.GetChild(0).transform.localPosition = Vector3.zero;
		image.transform.GetChild(0).transform.localRotation = Quaternion.Euler(imageRot.x+90, 0, imageRot.y);
		image.transform.GetChild(0).transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

        image.transform.GetChild(0).transform.GetChild(0).transform.localPosition = Vector3.zero;
		image.transform.GetChild(0).transform.GetChild(0).transform.localRotation = Quaternion.Euler(0, -imageRot.z, 0);
        image.transform.GetChild(0).transform.GetChild(0).transform.localScale = new Vector3(imageScal.x, imageScal.z, imageScal.y);



		FindObjectOfType<ContentManager>().imageCount++;
		FindObjectOfType<ContentManager>().assets_Count++;
        if (FindObjectOfType<ContentManager>().assets_Count== FindObjectOfType<ContentManager>().totalNumber_of_assets)
        {
			Debug.Log("Downloaded All Assets : ");
			//GameObject.Find("TrackerParent").gameObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
		}
		if (FindObjectOfType<ContentManager>().imageCount < FindObjectOfType<ContentManager>().numberOfImages)
        {
			FindObjectOfType<ContentManager>().DownlodImage();
		}
	}
}
