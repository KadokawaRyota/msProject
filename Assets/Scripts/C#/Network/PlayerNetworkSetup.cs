﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerNetworkSetup : NetworkBehaviour
{
    [SerializeField]
    public Camera PlayerCamera;

    [SerializeField]
    public AudioListener audioListener;

	[SerializeField]
	NetConnector netConnector;
	
    // Use this for initialization
    void Start()
	{
		netConnector = GameObject.Find("NetConnector").GetComponent<NetConnector>();
		if (netConnector.GetOnline())
		{
			
			if (!isServer)
			{
				//自分が操作するオブジェクトに限定する
				if (isLocalPlayer)
				{

					//PlayerCameraを使うため、Scene Cameraを非アクティブ化
					GameObject.Find("Scene Camera").SetActive(false);

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
		else
		{
			//自分が操作するオブジェクトに限定する
			if (isLocalPlayer)
			{

				//PlayerCameraを使うため、Scene Cameraを非アクティブ化
				GameObject.Find("Scene Camera").SetActive(false);

				//FirstPersonCharacterの各コンポーネントをアクティブ化
				PlayerCamera.GetComponent<Camera>().enabled = true;
				audioListener.GetComponent<AudioListener>().enabled = true;

			}

			//同期するスクリプトを無効
			GetComponent<PlayerSyncPosition>().enabled = false;
			GetComponent<PlayerSyncRotation>().enabled = false;
		}
        
	}

	
}
