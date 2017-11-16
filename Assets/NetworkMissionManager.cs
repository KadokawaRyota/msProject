using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkMissionManager : MonoBehaviour
{

    bool missionFlg = false;     //フラグの初期化

    [SerializeField]
    GameObject MissionCanvas;

    //配列化して、ミッションを入れる。今は運搬が入りっぱなし。
    [SerializeField]
    GameObject MissionType;

    // Use this for initialization
    void Start()
    {
        missionFlg = false;     //フラグの初期化
    }

    // Update is called once per frame
    void Update()
    {
        //ミッションを決めたりする処理をここに入れる予定
    }

    public void StartMission()
    {
        missionFlg = true;
        //ミッションキャンバスを表示して移動
        MissionCanvas.GetComponent<MissionCanvasScript>().ImgFlgSwitch(true);
        //オブジェクトの表示
        MissionType.GetComponent<NetworkTransportationScript>().dispMission();
    }
}
