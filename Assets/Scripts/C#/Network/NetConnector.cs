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

    [SerializeField]
    GameObject TransportationObject;

    GameObject charaInfo;

    [SerializeField]
    GameObject PlayerPrefab_0;

    [SerializeField]
    GameObject PlayerPrefab_1;

    [SerializeField]
    GameObject PlayerPrefab_2;

    [SerializeField]
    GameObject PlayerPrefab_3;

    CharactorInfo.CHARA chara;

    public void Start()
	{

		//NetworkManagerの取得
		manager = GetComponent<NetworkManager>();

        //OnlineCanvasの取得
		canvas = GameObject.Find("OnlineCanvas");
        if (canvas == null)
        {
            Debug.Log("Missing : OnlineCanvas");
        }

        //PuniconCameraの取得
        punioconCamera = GameObject.Find("PuniconCamera");
        if (punioconCamera == null)
        {
            Debug.Log("Missing : PunioconCamera");
        }

        //CharactorInfoの取得
        charaInfo = GameObject.Find("CharactorInfo");
        if(charaInfo == null)
        {
            Debug.Log("Missing : CharactorInfo");
        }
        chara = charaInfo.GetComponent<CharactorInfo>().GetCharaSelectData();

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
                manager.StartServer();                        //ホスト処理開始
                Debug.Log("Start as Server");

                punioconCamera.SetActive(false);
                canvas.SetActive(false);

                TransportationObject.GetComponent<NetworkTransportationScript>().CreateObject();
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

    //指定したプレイヤーを生成するためにオーバーライド
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        GameObject obj = null;
        switch (chara)
        {
            case CharactorInfo.CHARA.TANUKI:
                obj = (GameObject)Instantiate(PlayerPrefab_0, new Vector3(0f, 25.5f, 0f), Quaternion.identity);
                break;

            case CharactorInfo.CHARA.CAT:
                obj = (GameObject)Instantiate(PlayerPrefab_1, new Vector3(0f, 25.5f, 0f), Quaternion.identity);
                break;

            case CharactorInfo.CHARA.FOX:
                obj = (GameObject)Instantiate(PlayerPrefab_2, new Vector3(0f, 25.5f, 0f), Quaternion.identity);
                break;

            case CharactorInfo.CHARA.DOG:
                obj = (GameObject)Instantiate(PlayerPrefab_3, new Vector3(0f, 25.5f, 0f), Quaternion.identity);
                break;
        }
        NetworkServer.AddPlayerForConnection(conn, obj, playerControllerId);
    }

    public void Disconnect()
    {
        manager.StopClient();
        manager.OnClientDisconnect(GetComponent<NetworkIdentity>().connectionToClient);
        
    }
}