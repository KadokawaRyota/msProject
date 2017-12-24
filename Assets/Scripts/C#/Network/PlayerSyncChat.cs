using UnityEngine;
using UnityEngine.Networking;

public class PlayerSyncChat : NetworkBehaviour {

    [SyncVar]
    bool syncPlayFlg = false;   //同期用チャット再生フラグ

    bool playFlg = false;

    [SerializeField]
    GameObject chatImage;       //再生するチャットイメージ

	AudioManager audioManager;

	void Start()
	{
		audioManager = GameObject.Find ("AudioManager").GetComponent<AudioManager> ();
	}
    //更新
    void FixedUpdate()
    {
        //LocalPlayerは再生フラグがtrueなら再生
       if(isLocalPlayer)
        {
            if(playFlg)
            {
                PlayChat();

                //SendChatFlg(false);
            }
            else
            {
                SendChatFlg(false);
            }
        }

       //リモートはフラグに合わせて再生する
        else
        {
            if (syncPlayFlg)
            {
                PlayChat();
            }
        }
    }


    //再生
    public void PlayChat()
    {
        chatImage.GetComponent<Animator>().SetTrigger("open");

        playFlg = false;

		//チャット再生SE
		audioManager.Play_SE (AudioManager.SE.OpenChat);
    }

    //サーバー送信
    [Command]
    void CmdChatFlgToServer(bool flg)
    {
        syncPlayFlg = flg;
    }

    //クライアントがサーバーへ送るためのラッピング関数
    [ClientCallback]
    public void SendChatFlg(bool flg)
    {
        if(isLocalPlayer)
        {
            playFlg = flg;
            CmdChatFlgToServer(flg);
        }
    }
}
