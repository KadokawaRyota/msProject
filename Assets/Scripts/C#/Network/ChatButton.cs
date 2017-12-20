using UnityEngine;

public class ChatButton : MonoBehaviour {

    [SerializeField]
    NetConnector netConnector;

    //LocalPlayerのチャット再生
	public void Play()
    {
        GameObject localPlayer = netConnector.GetLocalPlayer();

        if(null != localPlayer)
        {
            //チャット再生フラグの切り替え
            localPlayer.GetComponent<PlayerSyncChat>().SendChatFlg(true);
        }
    }
}
