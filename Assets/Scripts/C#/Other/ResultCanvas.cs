using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultCanvas : MonoBehaviour {

	float count = 0f;

	[SerializeField]
	float nextSceneTime = 5f;

	LoadSceneManager loadSceneManager;

	//削除フラグ
	bool charaInfoDeleteFlg = false;

	bool tutoManaDeleteFlg = false;

	AudioManager audioManager;

	// Use this for initialization
	void Start () {
		
		GameObject manager = GameObject.Find ("LoadSceneManager");

		if (null != manager) {
		
			loadSceneManager = manager.GetComponent<LoadSceneManager> ();
		}

		//チューとリアルマネージャの削除
		GameObject tutoMana = GameObject.Find ("TutorialManager");
		if (null != tutoMana) {
			tutoMana.GetComponent<TutorialManager> ().ClearFlg ();
			Destroy (tutoMana.gameObject);
			tutoManaDeleteFlg = true;
		}

		//キャラクターインフォの削除
		GameObject charaInfo = GameObject.Find ("CharactorInfo");
		if (null != charaInfo) {
			charaInfo.GetComponent<CharactorInfo> ().ClearFlg ();
			Destroy (charaInfo.gameObject);
			charaInfoDeleteFlg = true;
		}

		GameObject audio = GameObject.Find ("AudioManager");

		if (null != audio) {
			audioManager = audio.GetComponent<AudioManager> ();
		}

		audioManager.Play_SE (AudioManager.SE.Ending);
	}
	
	// Update is called once per frame
	void Update () {
		
		if (!tutoManaDeleteFlg) {
			//チューとリアルマネージャの削除
			GameObject tutoMana = GameObject.Find ("TutorialManager");
			if (null != tutoMana) {
				tutoMana.GetComponent<TutorialManager> ().ClearFlg ();
				Destroy (tutoMana.gameObject);
				tutoManaDeleteFlg = true;
			}
		}


		if (!charaInfoDeleteFlg) {
			//キャラクターインフォの削除
			GameObject charaInfo = GameObject.Find ("CharactorInfo");
			if (null != charaInfo) {
				charaInfo.GetComponent<CharactorInfo> ().ClearFlg ();
				Destroy (charaInfo.gameObject);
				charaInfoDeleteFlg = true;
			}

		}


		if (count >= nextSceneTime) {

			loadSceneManager.LoadNextScene ("Title");
		} else {

			count += Time.deltaTime;
		}
	}
}
