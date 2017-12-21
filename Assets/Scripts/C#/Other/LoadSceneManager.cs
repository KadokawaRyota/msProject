using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//ローディング遷移
public class LoadSceneManager : MonoBehaviour {

    public static string nowSceneName { get; private set; }

    AsyncOperation async;
	public GameObject loadingUI;
	public Slider slider;

    //シーン遷移
    public void LoadNextScene(string name)
	{
		loadingUI.SetActive (true);	//ロード画面UIをアクティブ
		StartCoroutine (LoadScene(name));   //コルーチンを開始

        //シーン遷移時にBGMを止める
        AudioManager audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        if (audioManager != null)
        {
            audioManager.Stop_BGM();

            int random = 0;

            //取得したシーンのBGMを再生
            switch (name)
            {
                case "Title":
                    audioManager.Play_BGM(AudioManager.BGM.Title);
                    break;

                case "Home":
                    audioManager.Play_BGM(AudioManager.BGM.Home);
                    break;

                case "Offline":
                    random = Random.Range(0, 3);
                    switch (random)
                    {
                        case 0:
                            audioManager.Play_BGM(AudioManager.BGM.Game001);
                            break;

                        case 1:
                            audioManager.Play_BGM(AudioManager.BGM.Game002);
                            break;

                        case 2:
                            audioManager.Play_BGM(AudioManager.BGM.Game003);
                            break;
                    }
                    break;
                case "Main":
                    random = Random.Range(0, 3);
                    switch (random)
                    {
                        case 0:
                            audioManager.Play_BGM(AudioManager.BGM.Game001);
                            break;

                        case 1:
                            audioManager.Play_BGM(AudioManager.BGM.Game002);
                            break;

                        case 2:
                            audioManager.Play_BGM(AudioManager.BGM.Game003);
                            break;
                    }
                    break;
            }

            //現在のシーン名を設定
            nowSceneName = name;
        }
    }

    //ローディングコルーチン
	IEnumerator LoadScene(string sceneName)
	{
		//シーン読み込み
		async = SceneManager.LoadSceneAsync (sceneName);

		//読み込みが終わるまで進捗状況をスライダーの値に反映させる
		while (!async.isDone) {
			
			slider.value = async.progress;
			yield return null;
		}

        Debug.Log("コルーチン終了");
	}
}