using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Networking.NetworkSystem;


//ネットワーク接続に関するクラス
public class NetConnector : NetworkManager
{
    NetworkManager manager; //NetwokManager取得用

	[SerializeField]
	Image loadingImage;     //接続時に切り替え

    //サーバー切り替えフラグ
    public bool isStartAsServer = true;

    [SerializeField]
	string serverIPAdress = "192.168.13.3";

    GameObject punioconCamera;       //ぷにコンカメラの取得

	GameObject canvas;				//オンラインCanvasの取得

    //ネットワークプレハブ
    [SerializeField]
    GameObject TransportationObject;

    //プレイヤープレハブ    0:タヌキ
    //                        1:ネコ
    //                        2:キツネ
    //                        3:イヌ

    [Header("プレイヤー:タヌキ"),SerializeField]
    GameObject PlayerPrefab_0;

    [Header("プレイヤー:ネコ"), SerializeField]
    GameObject PlayerPrefab_1;

    [Header("プレイヤー:キツネ"), SerializeField]
    GameObject PlayerPrefab_2;

    [Header("プレイヤー:イヌ"), SerializeField]
    GameObject PlayerPrefab_3;

    GameObject localPlayer;

    CharactorInfo charaInfo;    //選んだキャラクター情報

    public bool createPlayer = false;  //生成フラグ

	[SerializeField]
	GameObject serverScore;

    public struct PLAYER_DICTIONARY
    {
        public NetworkConnection playerConn;
        public GameObject player;
    }

    List<PLAYER_DICTIONARY> playerDictionary = new List<PLAYER_DICTIONARY>();

	[SerializeField]
	GameObject missionTuto;

    void Awake()
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
        GameObject c = GameObject.Find("CharactorInfo");
        if (c == null)
        {

            Debug.Log("Missing : CharactorInfo");
        }
        else
        {
            charaInfo = c.GetComponent<CharactorInfo>();
        }

        //charaConnect = new CharaConnect();

        


		//接続時のローディングイメージを有効
		loadingImage.gameObject.SetActive(true);

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

				serverScore.SetActive (true);

                TransportationObject.GetComponent<NetworkTransportationScript>().CreateObject();

				Destroy (missionTuto.gameObject);
            }

            else
            {
                //仮想コントローラーの実装
                punioconCamera.SetActive(true);

                
                //charaConnect.CallCmd((int)charaInfo.GetCharaSelectData());

                manager.networkAddress = serverIPAdress;    //クライアントの時は設定したIPアドレスを代入
                manager.StartClient();                      //クライアント処理開始

                Debug.Log("Start as Client");  

				missionTuto.SetActive (true);
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

			missionTuto.SetActive (true);
        }
	}

    void Update()
    {
        /////////////////////////////////////
        //　名前のメッセージも追加する必要あり
        ////////////////////////////////////

        if (!isStartAsServer)
        {
            //プレイヤーを生成したかどうか
            if (!createPlayer)
            {
                //未生成ならサーバへメッセージを飛ばす（自分が選んだキャラクター）
                var type = new IntegerMessage((int)charaInfo.GetCharaSelectData());

                if (!ClientScene.AddPlayer(ClientScene.readyConnection, 0, type))
                {
                    return;
                }

                createPlayer = true;       //プレイヤー生成完了フラグ
            }
        }
    }
		
    //指定したプレイヤーを生成するためにオーバーライド
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader reader)
    {
        //メッセージの取得
        var type = reader.ReadMessage<IntegerMessage>();

        //メッセージの番号をintにキャスト
        int playerNum = type.value;

        GameObject obj = null;

        //生成するプレイヤーのプレハブ情報を取得
        switch((CharactorInfo.CHARA)playerNum)
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

        //生成
        NetworkServer.AddPlayerForConnection(conn, obj, playerControllerId);

        //プレイヤーを辞書に登録。
        PLAYER_DICTIONARY playerDic;
        playerDic.playerConn = conn;
        playerDic.player = obj;

        playerDictionary.Add(playerDic);
    }

    //ネットワーク終了処理
    public void NetDisconnect()
    {
        playerDictionary.Clear();
		//manager.StopClient ();
        Shutdown();
		//Destroy (this.gameObject);
    }

    //ローカルプレイヤーオブジェクトの設定
    public void SetLocalPlayer(GameObject player)
    {
        localPlayer = player;
    }

    public GameObject GetLocalPlayer()
    {
        return localPlayer;
    }

    public CharactorInfo GetCharactorInfo()
    {
        return charaInfo;
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        foreach (PLAYER_DICTIONARY playerdic in playerDictionary)
        {
            //消される予定のやつを検索
            if( playerdic.playerConn == conn )
            {
                GameObject gomi = playerdic.player.GetComponent<playerTransportationScript>().GetTransportObject();
                if (gomi != null)
                {
                    //ごみのリストから繋がってるプレイヤーを開放。
                    gomi.GetComponent<serverObjectController>().DisconnectPlayer(playerdic.player);
                    //基底クラスを呼び出す（おそらく終了処理）
                    base.OnServerDisconnect(conn);
                    return;
                }
            }
        }

        ////ゴミが誰ともつながってなかった場合、ここで終了処理をする。
        //基底クラスを呼び出す（おそらく終了処理）
        base.OnServerDisconnect(conn);
        return;
    }
}