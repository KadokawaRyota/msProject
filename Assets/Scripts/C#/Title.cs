using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour {

    public bool isHost = true;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void NextScene()
    {
        //PCアプリケーション起動時処理
        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
        {
            if (isHost)
            {
                SceneManager.LoadScene("Main");     //メインシーンへ
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);   //次のシーンへ
            }
        }

        //アンドロイドアプリケーション起動時処理
        else if (Application.platform == RuntimePlatform.Android)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
