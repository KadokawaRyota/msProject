﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class NetworkTransportationScript : NetworkBehaviour
{
    [SerializeField]
    GameObject MissionObjects;

   [SerializeField]
    GameObject missionObjectPrefab;


    //ここを配列で増やす。
    [SerializeField]
    Vector3 missionObjectPosition;
    //配列を増やしたら数も変える事
    int objectCnt = 1;

    [SerializeField]
    ParticleSystem particle;
    // Use this for initialization
    void Start()
    {
        if( SceneManager.GetActiveScene().name == "Offline" )
        {
            particle = GameObject.Find("arrivalArea").GetComponent<ParticleSystem>();
        }
        else if (SceneManager.GetActiveScene().name == "Main")
        {
            particle = GameObject.Find("NetworkarrivalArea").GetComponent<ParticleSystem>();
        }
        else
        {
            Debug.Log("シーンが見つからない。");
        }
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
            particle.Play();
        }
    }

    [ServerCallback]
    public void CreateObject()
    {
        GameObject missionObject;
        for( int cnt = 0; cnt < objectCnt; cnt++ )
        {
            missionObject = Instantiate( missionObjectPrefab , missionObjectPosition , Quaternion.identity , MissionObjects.transform );
			missionObject.GetComponent<MeshRenderer>().enabled = true;
            missionObject.GetComponent<BoxCollider>().enabled = true;

			NetworkServer.Spawn(missionObject);
			//NetworkServer.SpawnWithClientAuthority(missionObject, gameObject);
			//GetComponent<NetworkIdentity>().AssignClientAuthority(this.GetComponent<NetworkIdentity>().connectionToServer);

        }
    }
}
