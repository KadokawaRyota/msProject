using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetConnector : NetworkManager
{

    NetworkManager manager;

	[SerializeField]
	Image loadingImage;

    //サーバー切り替えフラグ
    public bool isStartAsServer = true;

    [SerializeField]
	string serverIPAdress = "192.168.13.3";

    GameObject punioconCamera;       //ぷにコンカメラの取得

	GameObject canvas;				//オンラインCanvasの取得

	public void Start()
	{

		//NetworkManagerの取得
		manager = GetComponent<NetworkManager>();
		canvas = GameObject.Find("OnlineCanvas");
		punioconCamera = GameObject.Find("PuniconCamera");

		//クライアント処理
		if (!isStartAsServer)
		{
			//接続時のローディングイメージを有効
			loadingImage.gameObject.SetActive(true);
		}

		OnlineSetup();  //オンライン時の設定

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
				canvas.SetActive(false);
		
			}

			else
			{
				//仮想コントローラーの実装
				punioconCamera.SetActive(true);

				manager.networkAddress = serverIPAdress;    //クライアントの時は設定したIPアドレスを代入
				manager.StartClient();                      //クライアント処理開始
				Debug.Log("Start as Client");

				//接続時のローディングイメージを無効
				//loadingImage.gameObject.SetActive(false);
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

			//接続時のローディングイメージを無効
			//loadingImage.gameObject.SetActive(false);
		}
	}
}