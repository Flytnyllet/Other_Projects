using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestartGame : MonoBehaviour {
    Text text;
    public delegate void RestartGameEvent();
    public static event RestartGameEvent OnRestartGame;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
        DieOnCollisionWithBodypart.OnDeath += OnPlayerDeath;
	}

    private void OnPlayerDeath(GameObject obj)
    {
        text.enabled = true;
    }

    // Update is called once per frame
    void Update () {
		if(text.enabled && Input.GetKey(KeyCode.R)) {
            text.enabled = false;
            if (OnRestartGame != null)
                OnRestartGame();
        }
	}
}
