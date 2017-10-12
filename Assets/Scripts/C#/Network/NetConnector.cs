using UnityEngine;
using UnityEngine.Networking;

public class NetConnector : NetworkBehaviour
{

    NetworkManager manager;

    //サーバー切り替えフラグ
    public bool isStartAsHost = true;

    public string serverIPAdress = "(対象のPCのIPAdressをいれる　指定しないとlocalhostっぽい)";

    public GameObject punioconCamera;
    // Use this for initialization
    void Start()
    {
        //NetworkManagerの取得
        manager = GetComponent<NetworkManager>();

        //PCアプリケーション起動時処理
        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
        {
            if (isStartAsHost)
            {
                manager.networkAddress = "localhost";       //ホストの時はlocalhost
                manager.StartHost();                        //ホスト処理開始
                Debug.Log("Start as Host");
				punioconCamera.SetActive(false);


            }

            else
            {
                manager.networkAddress = serverIPAdress;    //クライアントの時は設定したIPアドレスを代入
                manager.StartClient();                      //クライアント処理開始
                Debug.Log("Start as Client");
				punioconCamera.SetActive(true);
            }

        }

        //アンドロイドアプリケーション起動時処理
        else if (Application.platform == RuntimePlatform.Android)
        {
			//アンドロイドのみ仮想コントローラーの実装
			punioconCamera.SetActive(true);

            //アンドロイドでは常にクライアント（ホストにはならない）
            manager.networkAddress = serverIPAdress;
            manager.StartClient();
            Debug.Log("Start as Client");
        }
    }
}