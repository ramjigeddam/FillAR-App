using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class LoadAudioContent : MonoBehaviour
{
	AudioClip clip;
	public static LoadAudioContent instanceAudio;

	private void Start()
    {
		instanceAudio = this;
	}
    public void GetAudioDetails(string url, string audioID, Vector3 audioPos, Vector3 audioRot, Vector3 audioScal) 
	{ 
		StartCoroutine(LoadAudio( url,  audioID,  audioPos,  audioRot,  audioScal));
	}
	IEnumerator LoadAudio(string url,string audioID, Vector3 audioPos, Vector3 audioRot, Vector3 audioScal)
	{
		
		UnityWebRequest webRequest;

		if (url != "")
		{
			webRequest = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.UNKNOWN);
			yield return webRequest.SendWebRequest();

			if (webRequest.isDone)
			{
				clip = DownloadHandlerAudioClip.GetContent(webRequest);											
			}
			webRequest.Dispose();
		}
		
		GameObject obj = (GameObject)Instantiate(Resources.Load("Audio", typeof(GameObject)));
		obj.AddComponent<AudioSource>();
		obj.name = "audio";
				
		obj.GetComponent<AudioSource>().playOnAwake = true;
		obj.GetComponent<AudioSource>().Play();			
		
		GameObject mainParent = new GameObject();
		mainParent.name = audioID;		
		GameObject parentChild = new GameObject();
		
		mainParent.transform.SetParent(GameObject.Find("TrackerParent").transform);
		mainParent.transform.localRotation = Quaternion.Euler(0, 180, 0);
		parentChild.transform.SetParent(mainParent.transform);
		parentChild.transform.localPosition = new Vector3(0, 0, 0);
		parentChild.transform.localRotation = Quaternion.Euler(audioRot.x, 0, audioRot.y);
		parentChild.transform.localScale = new Vector3(1, 1, 1);
		obj.transform.SetParent(parentChild.transform);
		obj.transform.localPosition = new Vector3(0, 0, 0);
		obj.transform.localRotation = Quaternion.Euler(0, -audioRot.z, 0);
		mainParent.transform.localPosition = audioPos;
		
		audioScal = new Vector3(audioScal.x / ContentManager.instance.scaleValue, audioScal.y / ContentManager.instance.scaleValue, audioScal.z / ContentManager.instance.scaleValue);
		obj.transform.localScale = audioScal;
		obj.GetComponent<AudioSource>().clip =clip;

		//obj.GetComponent<Renderer>().material.mainTexture = texture2D;
		
		FindObjectOfType<ContentManager>().audioCount++;
		if (FindObjectOfType<ContentManager>().audioCount < FindObjectOfType<ContentManager>().numberOfAudios)
		{
			FindObjectOfType<ContentManager>().DownlodAudio();
		}
	}
}
