using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour {

	//オンライン接続ボタン用
	public void Online() { SceneManager.LoadScene("Main"); }

	//オンライン切断ボタン用
	public void OnlineExit() { SceneManager.LoadScene("Offline"); }

	public bool menuOpen = false;	//メニューバーの開閉フラグ

	//メニューバーの処理
	public void Menu()
	{
		if(menuOpen)
		{
			menuOpen = false;
		}
		else
		{
			menuOpen = true;
		}
	}
}
