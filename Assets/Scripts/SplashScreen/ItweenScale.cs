using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItweenScale : MonoBehaviour
{

    private void Awake()
    {
      //  transform.localScale = Vector3.zero;
    }
    void Start()
    {
        iTween.ScaleTo(gameObject, iTween.Hash("scale", new Vector3(2, 2, 2), "speed", 3, "easeType", iTween.EaseType.linear));

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
