using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	// Use this for initialization
	void Start () {

        //回転固定解除
        //Screen.orientation = ScreenOrientation.AutoRotation;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AndroidSelect()
    {
        //回転を固定　縦限定
        Screen.orientation = ScreenOrientation.Portrait;

        // 縦
        Screen.autorotateToPortrait = true;
        // 左
        Screen.autorotateToLandscapeLeft = false;
        // 右
        Screen.autorotateToLandscapeRight = false;
        // 上下反転
        Screen.autorotateToPortraitUpsideDown = true;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);   //次のシーンへ
    }

    public void PcSelect()
    {
        //回転を固定　横限定
        Screen.orientation = ScreenOrientation.LandscapeLeft;

        // 縦
        Screen.autorotateToPortrait = false;
        // 左
        Screen.autorotateToLandscapeLeft = true;
        // 右
        Screen.autorotateToLandscapeRight = true;
        // 上下反転
        Screen.autorotateToPortraitUpsideDown = false;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);   //次のシーンへ
    }
}
