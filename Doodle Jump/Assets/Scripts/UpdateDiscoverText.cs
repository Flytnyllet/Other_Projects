using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateDiscoverText : MonoBehaviour
{

    [SerializeField]
    string PartialObjectName;

    Text text;
    int numDiscovered;

    void Awake()
    {
        text = GetComponent<Text>();
        Platform.OnPlatformCreated += CheckPlatform;
    }

    private void CheckPlatform(GameObject obj)
    {
        if (!obj.name.Contains(PartialObjectName))
            return;
        numDiscovered++;
  
        text.text = "X " + numDiscovered;
    }
}
