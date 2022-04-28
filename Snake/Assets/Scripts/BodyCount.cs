using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BodyCount : MonoBehaviour {
    int appleCount = 0;
    int maxAppleCount = 0;

    int eAppleCount = 0;
    int eMaxAppleCount = 0;

    Text text;
	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
        RestartGame.OnRestartGame += OnGameRestarted;
        Apple.OnAppleEatenBy += OnAppleEaten;
        UpdateText();
	}

    private void OnAppleEaten(GameObject eatenBy)
    {
        if(eatenBy.GetComponent<PlayerController>() != null) {
            appleCount++;
            maxAppleCount = Mathf.Max(appleCount, maxAppleCount);
        }
        else {
            eAppleCount++;
            eMaxAppleCount = Mathf.Max(eAppleCount, eMaxAppleCount);
        }
        UpdateText();

    }

    private void OnGameRestarted()
    {
        appleCount = 0;
        eAppleCount = 0;
        UpdateText();
    }

    void UpdateText()
    {
        text.text = "Apple count: " + appleCount + "\n" +
                    "Record apple count: " + maxAppleCount + "\n" +
                    "Enemy apple count: " + eAppleCount + "\n" +
                    "Enemy record apple count: " + eMaxAppleCount + "\n";
    }
    
}
