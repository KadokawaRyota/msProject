using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetConnector : NetworkBehaviour
{

    NetworkManager manager;

	[SerializeField]
	Image loadingImage;

	//ローカル切り替えフラグ	ture時はOnline,false時はOffline
	[SerializeField]
	bool isOnlinePlay = false;

    //サーバー切り替えフラグ
    public bool isStartAsServer = true;

    [SerializeField]
	string serverIPAdress = "192.168.13.3";

    public GameObject punioconCamera;       //ぷにコンカメラの取得

	// Use this for initialization
	/* void Start()
	 {
		 //NetworkManagerの取得
		 manager = GetComponent<NetworkManager>();

		 if (isOnlinePlay)
		 {
			 OnlineSetup();	//オンライン時の設定
		 }
		 else
		 {
			 isStartAsServer = true;	//オフライン時はホストになる
			 OfflineSetup();	//オフライン時の設定
		 }
	 }*/


	public void Start()
	{
		if(SceneManager.GetActiveScene().name == "Main")
		{
			isOnlinePlay = true;
		}
		else if (SceneManager.GetActiveScene().name == "Offline")
		{
			isOnlinePlay = false;
		}

		//NetworkManagerの取得
		manager = GetComponent<NetworkManager>();
		//punioconCamera = GameObject.Find("PuniconCamera");

		
		if (isOnlinePlay)
		{
			OnlineSetup();  //オンライン時の設定
		}
		else
		{
			isStartAsServer = true; //オフライン時はホストになる
			OfflineSetup(); //オフライン時の設定
		}
		
		//クライアント処理
		if (!isStartAsServer)
		{
			//接続時のローディングイメージを有効
			loadingImage.gameObject.SetActive(true);
		}
	}

	public bool GetOnline()
	{
		return isOnlinePlay;
	}

	//オンラインセットアップ関数
	void OnlineSetup()
	{
		
		//PCアプリケーション起動時処理
		if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
		{
			if (isStartAsServer)
			{
				loadingImage.gameObject.SetActive(false);
				manager.networkAddress = "localhost";       //ホストの時はlocalhost
				manager.StartHost();                        //ホスト処理開始
				Debug.Log("Start as Server");
				punioconCamera.SetActive(false);

			}

			else
			{
				//仮想コントローラーの実装
				punioconCamera.SetActive(true);

				manager.networkAddress = serverIPAdress;    //クライアントの時は設定したIPアドレスを代入
				manager.StartClient();                      //クライアント処理開始
				Debug.Log("Start as Client");
			}

		}

		//アンドロイドアプリケーション起動時処理
		else if (Application.platform == RuntimePlatform.Android)
		{
			isStartAsServer = false;

			//仮想コントローラーの実装
			punioconCamera.SetActive(true);

			//アンドロイドでは常にクライアント（ホストにはならない）
			manager.networkAddress = serverIPAdress;
			manager.StartClient();
			Debug.Log("Start as Client");
		}
	}

	//オフラインセットアップ関数
	void OfflineSetup()
	{
		//PCアプリケーション起動時処理
		if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
		{
			//仮想コントローラーの実装
			punioconCamera.SetActive(true);

			manager.networkAddress = "localhost";       //ホストの時はlocalhost
			//manager.networkAddress = serverIPAdress;
			manager.StartHost();                        //ホスト処理開始
			Debug.Log("Start as Host");
		}

		//アンドロイドアプリケーション起動時処理
		else if (Application.platform == RuntimePlatform.Android)
		{
			//仮想コントローラーの実装
			punioconCamera.SetActive(true);

			//オフライン時はホストになる
			serverIPAdress = "localhost";
			//manager.networkAddress = serverIPAdress;
			manager.StartHost();
			Debug.Log("Start as Host");
		}
	}
}