using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour {

	AsyncOperation async;
	public GameObject loadingUI;
	public Slider slider;

	bool flg = false;
	public int waitTime = 60;
	int cnt = 0;
	public void LoadNextScene(string name)
	{
		loadingUI.SetActive (true);	//ロード画面UIをアクティブ
		StartCoroutine (LoadScene(name));	//コルーチンを開始
	}

	IEnumerator LoadScene(string sceneName)
	{
		//シーン読み込み
		async = SceneManager.LoadSceneAsync (sceneName);

		//読み込みが終わるまで進捗状況をスライダーの値に反映させる
		while (!async.isDone) {

			slider.value = async.progress;
			yield return null;
		}

	}
}
