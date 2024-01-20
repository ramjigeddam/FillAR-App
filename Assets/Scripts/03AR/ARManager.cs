using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ARManager : MonoBehaviour
{
    public static ARManager aRManager;
    [SerializeField]
    public TextMeshProUGUI userInfo;

    void Start()
    {
        aRManager = this;
        userInfo.text = "Point your camera at flat surface";
    }

    void Update()
    {
        
    }

    public void TapToPlaceText()
    {
        if (userInfo != null)
        {
            userInfo.text = "Tap to place Object";
        }
    }

    public void ClearInfo()
    {
        if (userInfo != null)
        {
            userInfo.text = "";
        }
    }
}
