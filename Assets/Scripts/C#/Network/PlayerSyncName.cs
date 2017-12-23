using UnityEngine;
using UnityEngine.Networking;

public class PlayerSyncName : NetworkBehaviour {

    //SyncVar: ホストサーバーからクライアントへ送られる
    [SerializeField]
    PlayerNetworkSetup netSet;

    //プレイヤーの名前
    [SyncVar]
    string syncPlayerName;

    [SerializeField]
    PlayerName playerName;

    void FixedUpdate()
    {
        //クライアント側のPlayerの角度を取得
        TransmitName();

    }

    //クライアントからホストへ送られる
    [Command]
    void CmdProvideNameToServer(string name)
    {
        syncPlayerName = name;
    }

    //クライアント側だけが実行できるメソッド
    [ClientCallback]
    void TransmitName()
    {
        if (isLocalPlayer)
        {
            CmdProvideNameToServer(netSet.GetCharaInfo().GetPlayerName());
        }
        //自プレイヤー以外のPlayerの時
        else
        {
            playerName.SetNameText(syncPlayerName);
        }
    }
}