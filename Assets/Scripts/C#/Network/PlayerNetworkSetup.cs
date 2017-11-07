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
		//ローディングイメージのアクティブを切る
		GameObject.Find("OnlineCanvas/LoadingImage").SetActive(false);

		//自分が操作するオブジェクトに限定する
		if (isLocalPlayer)
		{
			//PlayerCameraを使うため、Scene Cameraを非アクティブ化
			GameObject.Find("Scene Camera").SetActive(false);

			//Camera,AudioListenerの各コンポーネントをアクティブ化
			PlayerCamera.GetComponent<Camera>().enabled = true;
			audioListener.GetComponent<AudioListener>().enabled = true;

			//LocalPlayerのAnimatorパラメータを自動的に送る
			GetComponent<NetworkAnimator>().SetParameterAutoSend(0, true);
		}
		else
		{
			//自分以外の移動スクリプトを切る
			GetComponent<PostureController>().enabled = false;
		}
        
	}

	public override void PreStartClient()
	{
		//ClientのAnimatorパラメータを自動的に送る
		GetComponent<NetworkAnimator>().SetParameterAutoSend(0, true);
	}
}
