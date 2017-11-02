using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CustomNetworkManager : NetworkManager {

	//ButtonStartHostボタンを押した時に実行
	//IPポートを設定し、ホストとして接続
	public void StartupHost()
	{
		SetPort();
		NetworkManager.singleton.StartHost();
	}

	//ButtonJoinGameボタンを押した時に実行
	//IPアドレスとポートを設定し、クライアントとして接続
	public void JoinGame()
	{
		SetIPAddress();
		SetPort();
		NetworkManager.singleton.StartClient();

	}

	void SetIPAddress()
	{
		//Input Fieldに記入されたIPアドレスを取得し、接続する
		//string ipAddress = GameObject.Find("InputFieldIPAddress").transform.FindChild("Text").GetComponent<Text>().text;
		NetworkManager.singleton.networkAddress = "192.168.13.3";
	}

	//ポートの設定
	void SetPort()
	{
		NetworkManager.singleton.networkPort = 7777;
	}
}
