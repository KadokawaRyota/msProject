using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

class CharaConnect : NetworkBehaviour
{
    [SyncVar]
    public int chara;

    [ClientCallback]
    public void CallCmd(int c)
    {
        Debug.Log("CallCmd OK");
        CmdSetCharactor(c);
    }

    //[Commond]が通らない
    [Command]
    public void CmdSetCharactor(int c)
    {
        Debug.Log("CmdSetCharactor OK");
        chara = c;
    }
}
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

    [SerializeField]
    GameObject PlayerPrefab_0;

    [SerializeField]
    GameObject PlayerPrefab_1;

    [SerializeField]
    GameObject PlayerPrefab_2;

    [SerializeField]
    GameObject PlayerPrefab_3;

    CharactorInfo charaInfo;
    CharaConnect charaConnect;

    void Start()
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
        //chara = charaInfo.GetComponent<CharactorInfo>().GetCharaSelectData();
       // int c = (int)charaInfo.GetComponent<CharactorInfo>().GetCharaSelectData();
        //CmdSetCharactor(c);


        charaInfo.SetCharaSelectData(CharactorInfo.CHARA.DOG);

        charaConnect = new CharaConnect();



        //クライアント処理
        if (!isStartAsServer)
		{
			//接続時のローディングイメージを有効
			loadingImage.gameObject.SetActive(true);
		}

		OnlineSetup();  //オンライン時の設定

	}

    private void Update()
    {
        Debug.Log(charaConnect.chara);
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

                charaConnect.CallCmd((int)charaInfo.GetCharaSelectData());

                Debug.Log("CharaConnect更新後");
                Debug.Log((CharactorInfo.CHARA)charaConnect.chara);

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
        if(isStartAsServer)
        {
            charaConnect.chara = (int)charaInfo.GetCharaSelectData();
        }
        GameObject obj = null;

        switch ((CharactorInfo.CHARA)charaConnect.chara)
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
        //manager.StopClient();
        //manager.OnClientDisconnect(GetComponent<NetworkIdentity>().connectionToClient);
        Shutdown();
    }
}