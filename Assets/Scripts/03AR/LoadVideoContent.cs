using System.Collections;
using UnityEngine;
using UnityEngine.Video;
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
        GameObject video = (GameObject)Instantiate(Resources.Load("Video", typeof(GameObject)));
        video.name = videoID;

        yield return new WaitForSeconds(.1f);

		if (video!= null)
		{

			video.transform.gameObject.AddComponent<VideoPlayer>();
			video.transform.GetComponent<VideoPlayer>().playOnAwake = true;
			video.transform.GetComponent<VideoPlayer>().url = url;
			video.transform.GetComponent<VideoPlayer>().time = 0;

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

            _videoRotation.y = -_videoRotation.y;
            _videoRotation.z = -_videoRotation.z;

            Quaternion qX = Quaternion.Euler(_videoRotation.x, 0, 0);
            Quaternion qY = Quaternion.Euler(0, _videoRotation.y, 0);
            Quaternion qZ = Quaternion.Euler(0, 0, _videoRotation.z);
            Quaternion finalRotation = qZ * qX * qY;

            if (scene.name == "05Vuforia")
            {
                downloadeImage.transform.localPosition = new Vector3(-_videoPosition.x, _videoPosition.y, _videoPosition.z);
            }
            else
            {
                downloadeImage.transform.localPosition = new Vector3(-_videoPosition.x, _videoPosition.y, _videoPosition.z);
            }

            downloadeImage.transform.localRotation = finalRotation;
            downloadeImage.transform.localScale = new Vector3(_videoScaling.x / 10, _videoScaling.y / 10, _videoScaling.z / 10);

            video.transform.SetParent(downloadeImage.transform);
            video.transform.localPosition = Vector3.zero;
            video.transform.localRotation = Quaternion.Euler(90, 0, 0);
            video.transform.localScale = Vector3.one;
        }

		FindObjectOfType<ContentManager>().videoCount++;
		FindObjectOfType<ContentManager>().assets_Count++;

		if (FindObjectOfType<ContentManager>().assets_Count == FindObjectOfType<ContentManager>().totalNumber_of_assets)
		{
			Debug.Log("Downloaded All Assets : ");
		}

		if (FindObjectOfType<ContentManager>().videoCount < FindObjectOfType<ContentManager>().numberOfVideos)
		{
			FindObjectOfType<ContentManager>().DownlodVideo();
		}
	}
}
