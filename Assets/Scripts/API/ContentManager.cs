using LitJson;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
public class ContentManager : MonoBehaviour
{
    public static ContentManager instance;
    #region Get url and data
    //for getting content 
    private string baseUrl;
    public static JsonData contentData;
    public float scaleValue =1;
    #endregion

    #region Image data variebles
    //private List<string> image_info_message = new List<string>();
    private List<string> image_url = new List<string>();
    private List<string> image_id = new List<string>();
    private List<Vector3> image_Position = new List<Vector3>();
    private List<Vector3> image_Rotation = new List<Vector3>();
    private List<Vector3> image_Scale = new List<Vector3>();
    #endregion

    #region Video data variebles
   // private List<string> video_info_message = new List<string>();
    private List<string> video_url = new List<string>();
    private List<string> video_id = new List<string>();
    private List<string> video_type = new List<string>();
    private List<Vector3> video_Position = new List<Vector3>();
    private List<Vector3> video_Rotation = new List<Vector3>();
    private List<Vector3> video_Scale = new List<Vector3>();
    #endregion



    #region Audio data variebles
  //  private List<string> audio_info_message = new List<string>();
    private List<string> audio_url = new List<string>();
    private List<string> audio_id = new List<string>();
    private List<Vector3> audio_Position = new List<Vector3>();
    private List<Vector3> audio_Rotation = new List<Vector3>();
    private List<Vector3> audio_Scale = new List<Vector3>();
    #endregion

    #region Model data variebles
    //private List<string> model_info_message = new List<string>();
    private List<string> model_url = new List<string>();
    private List<string> model_id = new List<string>();
    private List<Vector3> model_Position = new List<Vector3>();
    private List<Vector3> model_Rotation = new List<Vector3>();
    private List<Vector3> model_Scale = new List<Vector3>();
    #endregion

    public int totalNumber_of_assets;
    public int assets_Count;
    void Start()
    {
        instance = this; 
        baseUrl = "https://fillar.in/backend-fillar/public/mobileApi/getExperienceData/CODE_exp123";
        StartCoroutine(ParseJson(baseUrl));
    }

    IEnumerator ParseJson(string url)
    {
        UnityWebRequest uwr = UnityWebRequest.Get(url);

        yield return uwr.SendWebRequest();

        if (uwr.isDone)
        {
            contentData = JsonMapper.ToObject(uwr.downloadHandler.text);
            if (contentData["status"].ToString() == "true" || contentData["status"].ToString() == "True")
            {
                GetAllAssetsData();
            }
            else
            {
                Debug.Log("Invalid data");// Enable popup data not avilable or network issue
            }
        }
    }

    public void GetAllAssetsData()
    {
        GetImageObjectData();
        GetVideoObjectData();
        //GetAudioObjectData();
        GetModelObjectData();
    }


    public int imageCount = 0;
    public int numberOfImages;
    void GetImageObjectData()
    {
       
        numberOfImages = contentData["data"]["image"].Count;
        totalNumber_of_assets = totalNumber_of_assets + numberOfImages;
        if (contentData["data"]["image"].Count > 0)
        {          
            for (int i = 0; i < contentData["data"]["image"].Count; i++)
            {
                // image_info_message.Add(contentData["data"]["images"][i]["message"].ToString());
                image_url.Add(contentData["data"]["image"][i]["asset_file_path"].ToString());
                image_id.Add (contentData["data"]["image"][i]["image_code"].ToString());

                image_Position.Add(new Vector3(float.Parse(contentData["data"]["image"][i]["position_x"].ToString()), float.Parse(contentData["data"]["image"][i]["position_y"].ToString()), float.Parse(contentData["data"]["image"][i]["position_z"].ToString())));
                image_Rotation.Add(new Vector3(float.Parse(contentData["data"]["image"][i]["rotation_x"].ToString()), float.Parse(contentData["data"]["image"][i]["rotation_y"].ToString()), float.Parse(contentData["data"]["image"][i]["rotation_z"].ToString())));
                image_Scale.Add(new Vector3(float.Parse(contentData["data"]["image"][i]["scale_x"].ToString()), float.Parse(contentData["data"]["image"][i]["scale_y"].ToString()), float.Parse(contentData["data"]["image"][i]["scale_z"].ToString())));

            }
           DownlodImage();
        }
    }
   
    public void  DownlodImage()
    {
        LoadImageContent.instanceImage.GetImageDetails(image_url[imageCount], image_id[imageCount], image_Position[imageCount], image_Rotation[imageCount], image_Scale[imageCount]);    
    }


    public int videoCount = 0;
    public int numberOfVideos;
    void GetVideoObjectData()
    {
        numberOfVideos = contentData["data"]["video"].Count;
        totalNumber_of_assets = totalNumber_of_assets + numberOfVideos;
        if (contentData["data"]["video"].Count > 0)
        {         
            for (int i = 0; i < contentData["data"]["video"].Count; i++)
            {
                //video_info_message.Add(contentData["assets"]["videos"][i]["message"].ToString());
                video_url.Add(contentData["data"]["video"][i]["asset_file_path"].ToString());
                video_id.Add(contentData["data"]["video"][i]["video_code"].ToString());
                video_Position.Add( new Vector3(float.Parse(contentData["data"]["video"][i]["position_x"].ToString()), float.Parse(contentData["data"]["video"][i]["position_y"].ToString()), float.Parse(contentData["data"]["video"][i]["position_z"].ToString())));
                video_Rotation.Add( new Vector3(float.Parse(contentData["data"]["video"][i]["rotation_x"].ToString()), float.Parse(contentData["data"]["video"][i]["rotation_y"].ToString()), float.Parse(contentData["data"]["video"][i]["rotation_z"].ToString())));
                video_Scale.Add( new Vector3(float.Parse(contentData["data"]["video"][i]["scale_x"].ToString()), float.Parse(contentData["data"]["video"][i]["scale_y"].ToString()), float.Parse(contentData["data"]["video"][i]["scale_z"].ToString())));         
            }
            DownlodVideo();
        }
    }

    public void DownlodVideo()
    {
        LoadVideoContent.instanceVideo.GetVideoDetails(video_url[videoCount], video_id[videoCount], video_Position[videoCount], video_Rotation[videoCount], video_Scale[videoCount]);
    }


    public int audioCount = 0;
    public int numberOfAudios;
    void GetAudioObjectData()
    {

        numberOfAudios = contentData["data"]["audio"].Count;
        totalNumber_of_assets = totalNumber_of_assets + numberOfAudios;
        if (contentData["data"]["audio"].Count > 0)
        {         
            for (int i = 0; i < contentData["data"]["audio"].Count; i++)
            {
                //audio_info_message.Add(contentData["assets"]["audios"][i]["message"].ToString());
                audio_url.Add(contentData["data"]["audio"][i]["asset_file_path"].ToString());
                audio_id.Add(contentData["data"]["audio"][i]["audio_code"].ToString());
              
                audio_Position.Add(new Vector3(float.Parse(contentData["data"]["audio"][i]["position_x"].ToString()), float.Parse(contentData["data"]["audio"][i]["position_y"].ToString()), float.Parse(contentData["data"]["audio"][i]["position_z"].ToString())));
                audio_Rotation.Add(new Vector3(float.Parse(contentData["data"]["audio"][i]["rotation_x"].ToString()), float.Parse(contentData["data"]["audio"][i]["rotation_y"].ToString()), float.Parse(contentData["data"]["audio"][i]["rotation_z"].ToString())));
                audio_Scale.Add(new Vector3(float.Parse(contentData["data"]["audio"][i]["scale_x"].ToString()), float.Parse(contentData["data"]["audio"][i]["scale_y"].ToString()), float.Parse(contentData["data"]["audio"][i]["scale_z"].ToString())));                       
            }
            DownlodAudio();
        }
    }

    public void DownlodAudio()
    {
        LoadAudioContent.instanceAudio.GetAudioDetails(audio_url[audioCount], audio_id[audioCount], audio_Position[audioCount], audio_Rotation[audioCount], audio_Scale[audioCount]);
    }


    public int modelCount = 0;
    public int numberOfModels;
    void GetModelObjectData()
    {
        numberOfModels = contentData["data"]["mesh"].Count;
        totalNumber_of_assets = totalNumber_of_assets + numberOfModels;
        if (contentData["data"]["mesh"].Count > 0)
        {
            for (int i = 0; i < contentData["data"]["mesh"].Count; i++)
            {
                //model_info_message.Add(contentData["assets"]["mesh"][i]["message"].ToString());
                model_url.Add(contentData["data"]["mesh"][i]["asset_file_path"].ToString());
                model_id.Add(contentData["data"]["mesh"][i]["mesh_code"].ToString());

                model_Position.Add(new Vector3(float.Parse(contentData["data"]["mesh"][i]["position_x"].ToString()), float.Parse(contentData["data"]["mesh"][i]["position_y"].ToString()), float.Parse(contentData["data"]["mesh"][i]["position_z"].ToString())));
                model_Rotation.Add(new Vector3(float.Parse(contentData["data"]["mesh"][i]["rotation_x"].ToString()), float.Parse(contentData["data"]["mesh"][i]["rotation_y"].ToString()), float.Parse(contentData["data"]["mesh"][i]["rotation_z"].ToString())));
                model_Scale.Add(new Vector3(float.Parse(contentData["data"]["mesh"][i]["scale_x"].ToString()), float.Parse(contentData["data"]["mesh"][i]["scale_y"].ToString()), float.Parse(contentData["data"]["mesh"][i]["scale_z"].ToString())));
               
            }
            DownlodModel();
        }
    }

    public void DownlodModel()
    {
        LoadModelContent.instanceModel.GetModel_Info(model_url[modelCount], model_id[modelCount], model_Position[modelCount], model_Rotation[modelCount], model_Scale[modelCount]);
    }
}
