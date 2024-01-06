using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScreenShotManager : MonoBehaviour
{
    [SerializeField]
    private GameObject mainPanel,bottomPanel;

    [SerializeField]
    private Image screenShot;
    void Start()
    {
        
    }

   // public Sprite sprite;
    public void TakeScreenShot()
    {
        StartCoroutine(TakeScreenshotAndSave());
    }

    private IEnumerator TakeScreenshotAndSave()
    {
        yield return new WaitForEndOfFrame();

        mainPanel.SetActive(false);
        bottomPanel.SetActive(false);

        Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        ss.Apply();

        //var bytes = ss.EncodeToPNG();
        //sprite = Sprite.Create(ss, new Rect(0, 0, ss.width, ss.height), new Vector2(ss.width / 2, ss.height / 2));
        //screenShot.GetComponent<Image>().sprite = sprite;

        // Save the screenshot to Gallery/Photos
        NativeGallery.Permission permission = NativeGallery.SaveImageToGallery(ss, "FillAR", "Image.png", (success, path) => Debug.Log("Media save result: " + success + " " + path));

        // To avoid memory leaks
        Destroy(ss);

        yield return new WaitForSeconds(2);
        mainPanel.SetActive(true);
        bottomPanel.SetActive(true);
    }

}
