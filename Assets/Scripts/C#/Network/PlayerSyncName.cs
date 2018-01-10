using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PlayerSyncName : NetworkBehaviour {

    //SyncVar: ホストサーバーからクライアントへ送られる
    [SerializeField]
    PlayerNetworkSetup netSet;

    //プレイヤーの名前
    [SyncVar]
    string syncPlayerName;

	[SyncVar]
	int syncPlayerType;

    [SerializeField]
    PlayerName playerName;

	[SerializeField]
	Text nameText;

	int playerType;

	void Start()
	{
		//仕様キャラ
		if(isLocalPlayer)
		{
			GameObject charaInfo = GameObject.Find ("CharactorInfo");

			if (null != charaInfo) {
				playerType = (int)charaInfo.GetComponent<CharactorInfo> ().GetCharaSelectData ();
			}

			switch (charaInfo.GetComponent<CharactorInfo> ().GetCharaSelectData ()) {

			case CharactorInfo.CHARA.TANUKI:
				nameText.color = new Color (100f / 255f, 255f / 255f, 100f / 255f);
				break;

			case CharactorInfo.CHARA.CAT:
				nameText.color = new Color (255f / 255f, 150f / 255f, 200f / 255f);
				break;

			case CharactorInfo.CHARA.FOX:
				nameText.color = new Color (255f / 255f, 80f / 255f, 0f / 255f);
				break;

			case CharactorInfo.CHARA.DOG:
				nameText.color = new Color (0f / 255f, 170f / 255f, 255f / 255f);
				break;
			}
		}

	}

    void FixedUpdate()
    {
        //クライアント側のPlayerの角度を取得
        TransmitName();

    }

    //クライアントからホストへ送られる
    [Command]
	void CmdProvideNameToServer(string name,int type)
    {
        syncPlayerName = name;
		syncPlayerType = type;
    }

    //クライアント側だけが実行できるメソッド
    [ClientCallback]
    void TransmitName()
    {
        if (isLocalPlayer)
        {
			CmdProvideNameToServer(netSet.GetCharaInfo().GetPlayerName(),playerType);

        }
        //自プレイヤー以外のPlayerの時
        else
        {
            playerName.SetNameText(syncPlayerName);

			switch ((CharactorInfo.CHARA)syncPlayerType) {

			case CharactorInfo.CHARA.TANUKI:
				nameText.color = new Color (100f / 255f, 255f / 255f, 100f / 255f);
				break;

			case CharactorInfo.CHARA.CAT:
				nameText.color = new Color (255f / 255f, 150f / 255f, 200f / 255f);
				break;

			case CharactorInfo.CHARA.FOX:
				nameText.color = new Color (255f / 255f, 80f / 255f, 0f / 255f);
				break;

			case CharactorInfo.CHARA.DOG:
				nameText.color = new Color (0f / 255f, 170f / 255f, 255f / 255f);
				break;
			}
        }
    }
}