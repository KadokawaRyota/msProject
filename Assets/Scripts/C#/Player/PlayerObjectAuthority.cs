using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerObjectAuthority : NetworkBehaviour {

	//[SyncVar]
	public GameObject hasAuthority;

	//クライアントからホストへ、権限情報を送る
	/*[Command]
	void CmdProvideAuthorityToServer(GameObject obj)
	{
		//サーバーが受け取る値
		hasAuthority = obj;
	}

	//クライアントのみ実行される
	[ClientCallback]
	//位置情報を送るメソッド
	void TransmitAuthority()
	{
		CmdProvideAuthorityToServer(hasAuthority);
	}
	*/
	//引数のオブジェクトの権限を得る
	public void AssingAuthority(GameObject obj){
		hasAuthority = obj;
	}

	//所持している権限を破棄
	public void RemoveAuthority()
	{
		hasAuthority = null;
	}
}
