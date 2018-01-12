using System.Collections;
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
    GameObject transObj_S;
    [SerializeField]
    GameObject transObj_M;
    [SerializeField]
    GameObject transObj_L;

	[SerializeField]
	GameObject arrivalAreaPrefab;

    [System.Serializable]
    public class SpawnObject
    {
        public char sizeName;                  //S、M、Lのどれかしか入らない。それ以外はエラー
        public Vector3 missionObjectPosition;
    }

	[System.Serializable]
	public class ArrivalArea
	{
		public Vector3 position;
		public Quaternion rotation;
	}

    //ここを配列で増やす。
    [SerializeField]
    SpawnObject[] spawnObjectData = new SpawnObject[2];


    //ゴール地点
	[SerializeField]
	ArrivalArea[] arrivalAreaData = new ArrivalArea[4];

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
			//パーティクルなし
            //child.gameObject.GetComponent<ParticleSystem>().Play();
        }
    }

    [ServerCallback]
    public void CreateObject()
    {
        //ミッションオブジェクトの生成
        GameObject missionObject;

        for( int cnt = 0; cnt < spawnObjectData.Length; cnt++ )
        {
            switch(spawnObjectData[cnt].sizeName)
            {
                case 's':
                case 'S':
                {
                    missionObject = Instantiate(transObj_S, spawnObjectData[cnt].missionObjectPosition, Quaternion.identity, MissionObjects.transform);
                    missionObject.GetComponent<MeshRenderer>().enabled = true;
                    missionObject.GetComponent<SphereCollider>().enabled = true;

                    NetworkServer.Spawn(missionObject);
                    break;
                }
                case 'm':
                case 'M':
                {
                        missionObject = Instantiate(transObj_M, spawnObjectData[cnt].missionObjectPosition, Quaternion.identity, MissionObjects.transform);
                        missionObject.GetComponent<MeshRenderer>().enabled = true;
                        missionObject.GetComponent<SphereCollider>().enabled = true;

                        NetworkServer.Spawn(missionObject);
                        break;
                }
                case 'l':
                case 'L':
                {
                        missionObject = Instantiate(transObj_L, spawnObjectData[cnt].missionObjectPosition, Quaternion.identity, MissionObjects.transform);
                        missionObject.GetComponent<MeshRenderer>().enabled = true;
                        missionObject.GetComponent<SphereCollider>().enabled = true;

                        NetworkServer.Spawn(missionObject);
                        break;
                }
                default:
                {
                        Debug.Log("-error---規定外のオブジェクトが置かれようとしました。");
                        break;
                }
            }
        }

		for (int cnt = 0; cnt < 1; cnt++) {
        //arraivalAreaの生成
        //arrivalArea = Instantiate(arrivalAreaPrefab, arrivalAreaPosition, arrivalAreaPrefab.transform.localRotation, MissionObjects.transform.parent);
        //NetworkServer.Spawn(arrivalArea);

			//arrivalArea = Instantiate(arrivalAreaPrefab, arrivalAreaData[cnt].position, Quaternion.identity, MissionObjects.transform.parent);
			//arrivalArea.transform.rotation = arrivalAreaData [cnt].rotation;
			//NetworkServer.Spawn(arrivalArea);
		}
    }
}
