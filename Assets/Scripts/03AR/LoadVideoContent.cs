using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class LoadVideoContent : MonoBehaviour
{
	Texture2D texture2D;
	public static LoadVideoContent instanceVideo;
    private Scene scene;
    private void Start()
    {
        scene = SceneManager.GetActiveScene();
        instanceVideo = this;
	}
    public void GetVideoDetails(string url, string videoID, Vector3 _videoPosition, Vector3 _videoRotation, Vector3 _videoScaling)
    {
		StartCoroutine(LoadingVideo( url,  videoID,    _videoPosition,  _videoRotation,  _videoScaling));
	}
	
	IEnumerator LoadingVideo(string url, string videoID, Vector3 _videoPosition, Vector3 _videoRotation, Vector3 _videoScaling)
	{
       

        GameObject video = (GameObject)Instantiate(Resources.Load("VideoMainParent", typeof(GameObject)));
        if (scene.name == "05Vuforia")
            video.transform.localScale = new Vector3(1f, 1f, 1f);
        else
            video.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

        yield return new WaitForSeconds(.1f);

		if (video.transform.GetChild(0).transform.GetChild(0) != null)
		{

			video.transform.GetChild(0).transform.GetChild(0).gameObject.AddComponent<VideoPlayer>();
			video.transform.GetChild(0).transform.GetChild(0).GetComponent<VideoPlayer>().playOnAwake = true;
			video.transform.GetChild(0).transform.GetChild(0).GetComponent<VideoPlayer>().url = url;
			video.transform.GetChild(0).transform.GetChild(0).GetComponent<VideoPlayer>().time = 0;

            //_videoScaling = new Vector3(_videoScaling.x / ContentManager.instance.scaleValue, _videoScaling.z / ContentManager.instance.scaleValue, _videoScaling.y / ContentManager.instance.scaleValue);
            //_videoScaling = new Vector3(_videoScaling.x / ContentManager.instance.scaleValue, _videoScaling.z / ContentManager.instance.scaleValue, _videoScaling.y / ContentManager.instance.scaleValue);

            //video.transform.GetChild(0).transform.GetChild(0).transform.localPosition = _videoPosition;

			video.name = videoID;
            video.transform.SetParent(GameObject.Find("TrackerParent").transform);
			video.transform.localPosition = new Vector3(-_videoPosition.x, _videoPosition.y, _videoPosition.z);
			video.transform.localRotation = Quaternion.Euler(0, 180, 0);

			video.transform.GetChild(0).transform.localPosition = Vector3.zero;
			video.transform.GetChild(0).transform.localRotation = Quaternion.Euler(_videoRotation.x+90, 0, _videoRotation.y);
			video.transform.GetChild(0).transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
		
			video.transform.GetChild(0).transform.GetChild(0).transform.localPosition = Vector3.zero;
			video.transform.GetChild(0).transform.GetChild(0).transform.localRotation = Quaternion.Euler(0, _videoRotation.z, 0);
			video.transform.GetChild(0).transform.GetChild(0).transform.localScale = new Vector3(_videoScaling.x,_videoScaling.z,_videoScaling.y);		
			video.transform.GetChild(0).transform.GetChild(0).GetComponent<Renderer>().material.mainTexture = texture2D;	
			
		}

		FindObjectOfType<ContentManager>().videoCount++;
		FindObjectOfType<ContentManager>().assets_Count++;

		if (FindObjectOfType<ContentManager>().assets_Count == FindObjectOfType<ContentManager>().totalNumber_of_assets)
		{
			Debug.Log("Downloaded All Assets : ");
			//GameObject.Find("TrackerParent").gameObject.transform.localScale = new Vector3(0.1f,0.1f,0.1f);
		}

		if (FindObjectOfType<ContentManager>().videoCount < FindObjectOfType<ContentManager>().numberOfVideos)
		{
			FindObjectOfType<ContentManager>().DownlodVideo();
		}
	}
}
