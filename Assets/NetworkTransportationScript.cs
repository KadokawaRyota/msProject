using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

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

    [Server]
    public void CreateObject()
    {
        GameObject missionObject;
        for( int cnt = 0; cnt < objectCnt; cnt++ )
        {
            missionObject = Instantiate( missionObjectPrefab , missionObjectPosition , Quaternion.identity , MissionObjects.transform );
			missionObject.GetComponent<MeshRenderer>().enabled = true;
            NetworkServer.Spawn(missionObject);
        }
    }
}
