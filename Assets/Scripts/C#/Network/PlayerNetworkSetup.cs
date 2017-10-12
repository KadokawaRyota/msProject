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
        if (!isServer)
		{
			//自分が操作するオブジェクトに限定する
			if (isLocalPlayer)
			{

                //FPSCharacterCameraを使うため、Scene Cameraを非アクティブ化
                GameObject.Find("Scene Camera").SetActive(false);
                //GetComponent<CharacterController>().enabled = true; //Character Controllerをアクティブ化

                //FirstPersonControllerをアクティブ化
                //GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = true;

                //FirstPersonCharacterの各コンポーネントをアクティブ化
                PlayerCamera.GetComponent<Camera>().enabled = true;
				audioListener.GetComponent<AudioListener>().enabled = true;

			}
			else
			{
				GetComponent<PostureController>().enabled = false;
			}
		}
        
		else
		{
			//サーバーの時はプレイヤーを生成しない
			if (isLocalPlayer)
			{
                //camera.SetActive(true);

                Destroy(this.gameObject);
			}
		}
        
	}
}
