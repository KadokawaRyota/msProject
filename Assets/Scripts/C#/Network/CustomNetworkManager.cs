using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CustomNetworkManager : NetworkManager {

	[SerializeField]
	string IPAddress = "接続先IPアドレスを入れる";

	[SerializeField]
	int PortAddress = 25000;

	NetworkManager manager;
	GameObject punioconCamera;       //ぷにコンカメラの取得
	GameObject canvas;              //オンラインCanvasの取得

	private void Start()
	{
		NetworkManager manager = gameObject.GetComponent<NetworkManager>();
		manager.networkAddress = IPAddress;
		manager.networkPort = PortAddress;

		manager = GetComponent<NetworkManager>();
		canvas = GameObject.Find("OnlineCanvas");
		punioconCamera = GameObject.Find("PuniconCamera");

	}

	void OnlineSetUp()
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			//仮想コントローラーの実装
			punioconCamera.SetActive(true);

			//アンドロイドでは常にクライアント（ホストにはならない）
			manager.networkAddress = IPAddress;
			manager.StartClient();
			Debug.Log("Start as Client");
		}
	}

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
		NetworkManager.singleton.networkAddress = "192.168.13.2";
	}

	//ポートの設定
	void SetPort()
	{
		NetworkManager.singleton.networkPort = 7777;
	}
}
