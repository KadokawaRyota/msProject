using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

//ボタン各種のスクリプト
public class ButtonExecution : NetworkBehaviour {
	
	//オンライン接続ボタン用
	public void Online() { SceneManager.LoadScene("Main"); }

	//オフライン接続ボタン用（オンライン時はオフラインに切り替え）
	public void Offline() {
		Network.Disconnect();
		SceneManager.LoadScene("Offline");
	}

	//ホーム画面へ遷移
	public void Home() {
		Network.Disconnect();
		SceneManager.LoadScene("Home");
	}

}
