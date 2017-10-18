using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

//ボタン各種のスクリプト
public class ButtonExecution : MonoBehaviour {

	//オンライン接続ボタン用
	public void Online() { SceneManager.LoadScene("Main"); }

	//オフライン接続ボタン用（オンライン時はオフラインに切り替え）
	public void Offline() {	SceneManager.LoadScene("Offline"); }

	//ホーム画面へ遷移
	public void Home() { SceneManager.LoadScene("Home"); }

	/*** ここからメニューバー処理 **/
	bool menuOpen = false;	//メニューバーの開閉フラグ

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

	public bool GetMenuBar()
	{
		return menuOpen;
	}
}
