﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

//運ぶオブジェクトと到達地点の作成

public class NetworkTransportationScript : NetworkBehaviour
{
    [SerializeField]
    GameObject MissionObjects;
    [SerializeField]
    GameObject ArrivalAreas;

   [SerializeField]
    GameObject missionObjectPrefab;

    //ここを配列で増やす。
    [SerializeField]
    Vector3 missionObjectPosition;

    //配列を増やしたら数も変える事
    int objectCnt = 1;

    [SerializeField]
    GameObject arrivalAreaPrefab;
    [SerializeField]
    Vector3 arrivalAreaPosition;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void dispMission()
    {
        //ミッションに関係あるオブジェクト表示
        foreach (Transform child in MissionObjects.transform)
        {
            child.gameObject.GetComponent<NetworkObjectController>().DispSwitch(true);
        }
        //アライバルエリアを全て表示
        foreach (Transform child in ArrivalAreas.transform)
        {
            child.gameObject.GetComponent<ParticleSystem>().Play();
        }
    }

    [ServerCallback]
    public void CreateObject()
    {
        //ミッションオブジェクトの生成
        GameObject missionObject;
        GameObject arrivalArea;
        for( int cnt = 0; cnt < objectCnt; cnt++ )
        {
            missionObject = Instantiate( missionObjectPrefab , missionObjectPosition , Quaternion.identity , MissionObjects.transform );
			missionObject.GetComponent<MeshRenderer>().enabled = true;
            missionObject.GetComponent<SphereCollider>().enabled = true;

			NetworkServer.Spawn(missionObject);
			//NetworkServer.SpawnWithClientAuthority(missionObject, gameObject);
			//GetComponent<NetworkIdentity>().AssignClientAuthority(this.GetComponent<NetworkIdentity>().connectionToServer);

        }

        //arraivalAreaの生成
        arrivalArea = Instantiate(arrivalAreaPrefab, arrivalAreaPosition, arrivalAreaPrefab.transform.localRotation, MissionObjects.transform.parent);
        NetworkServer.Spawn(arrivalArea);
    }
}
