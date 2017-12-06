using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerNetworkSetup : NetworkBehaviour
{
    [SerializeField]
    public Camera PlayerCamera;

    [SerializeField]
    public AudioListener audioListener;

    // Use this for initialization
    void Start()
	{
		//自分が操作するオブジェクトに限定する
		if (isLocalPlayer)
		{
            //自分のプレイヤーの名前変更
            name = GameObject.Find("CharactorInfo").GetComponent<CharactorInfo>().GetPlayerName();
            name = "Player";

            //ローディングイメージのアクティブを切る
            if (GameObject.Find("OnlineCanvas/LoadingImage") != null)
            {
                GameObject.Find("OnlineCanvas/LoadingImage").SetActive(false);
            }

            //PlayerCameraを使うため、Scene Cameraを非アクティブ化
            GameObject.Find("Scene Camera").SetActive(false);

			//Camera,AudioListenerの各コンポーネントをアクティブ化
			PlayerCamera.GetComponent<Camera>().enabled = true;
			audioListener.GetComponent<AudioListener>().enabled = true;

			//LocalPlayerのAnimatorパラメータを自動的に送る
			GetComponent<NetworkAnimator>().SetParameterAutoSend(0, true);

            //カメラの取得等があるため、ここでPostureControllerのスクリプトをOnにしてStartメソッド呼び出し。
            GetComponent<OfflinePostureController>().enabled = true;

            //ミッションマネージャに自分がスポーンした事を知らせる。
            GameObject.Find("NetworkMissionManager").GetComponent<NetworkMissionManager>().SetPlayer(this.gameObject);

        }
		else
		{
			//自分以外の移動スクリプトを切る
			GetComponent<OfflinePostureController>().enabled = false;
            PlayerCamera.GetComponent<Camera>().enabled = false;
            audioListener.GetComponent<AudioListener>().enabled = false;
        }
        
	}

	public override void PreStartClient()
	{
		//ClientのAnimatorパラメータを自動的に送る
		GetComponent<NetworkAnimator>().SetParameterAutoSend(0, true);
	}
}
