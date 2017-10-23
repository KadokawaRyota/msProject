using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSManager : MonoBehaviour {

	[SerializeField]
	Text fpsText;

	[SerializeField]
	int setFPS;
	
	int frameCount;
	float prevTime;
	float fps;
	// Use this for initialization
	void Start () {
		//DontDestroyOnLoad(gameObject);
		Application.targetFrameRate = setFPS;

		frameCount = 0;
		prevTime = 0.0f;
		fps = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		++frameCount;

		float time = Time.realtimeSinceStartup - prevTime;

		if(time >= 0.5f)
		{
			fps = (frameCount / time);
			fpsText.text = fps.ToString();

			frameCount = 0;
			prevTime = Time.realtimeSinceStartup;
		}
	}
}
