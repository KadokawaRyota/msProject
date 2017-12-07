using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


/*紐の長さが短いなら引っ張らない
 * //仮のオブジェクトを引っ張るためのオブジェクト
 * //必要無くなったぽい。。。
 * 
//現在の距離 - fDistance > 0;
{
  伸びた量 * 加える力の量・・・addforce・・・物体の動く向き      
}

    3人や4人で引っ張る場合は他のプレイヤーにAddForceで力を加えるべき。

    箱にプレイヤーが引っ張られるのを表現したい・・・AddForceでプレイヤーが引っ張られる事を検証。
*/
/// <summary>
/// リアルな紐を演出するのはゲーム上ではバネから作っていく事になる。
/// 計算上どうしても伸び縮みする。
/// </summary>

public class playerTransportationScript : NetworkBehaviour
{
    //シリアライズ
    [SerializeField]
    private float fDistance;        //紐が伸び切る距離
    [SerializeField]
    private float playerSpringConstant;  //バネ定数・・・通常かかるバネ係数は同一だが、ゲーム的にプレイヤーの動きを良くするため。

    private float fDistancePlayer;          //プレイヤーとの距離
    Vector3 vecForPlayer = Vector3.zero;    //プレイヤーへの向き


    [SyncVar]
    bool SyncbPullListAdd = false;

    [SerializeField , SyncVar]
    GameObject transportObject = null; //プレイヤーが運ぶオブジェクト

    bool oldPullListAdd = false;

    [SyncVar , SerializeField]
    bool SyncbRunTimeTransport = false; //トランスポートミッション中ならtrue

    bool runTimeTransportOld = false; //トランスポートミッション中ならtrue

    // Use this for initialization
    void Start()
    {
        SyncbPullListAdd = false;
        SyncbRunTimeTransport = false;
    }

    // Update is called once per frame
    void Update()
    {
        //サーバー側でプレイヤーをトランスポート状態でミッションオブジェクトにぶつかるようにするため。レイヤー変更
        if( !runTimeTransportOld && SyncbRunTimeTransport )
        {
            this.gameObject.layer = 10;
        }

        //プレイヤーをサーバー側のオブジェクトに紐付けて良い時
        if (SyncbPullListAdd && !oldPullListAdd)
        {
            //サーバー側でリストにプレイヤーを追加;
            playerWithObject();
        }
        //ローカルプレイヤー側の処理
        if( isLocalPlayer ) pullPlayer();

        //プレイヤーから見たオブジェクトへの紐付けを解除する。
        if( transportObject != null )
        {
            if( transportObject.GetComponent<serverObjectController>().GetbGoal() )
            {
                CmdProvidebPullToServer(false, null);
            }
        }


        if( transportObject != null )
        {
            //引っ張ってるオブジェクトがエリアに入った事を検知させる
            if ( transportObject.GetComponent<serverObjectController>().GetbGoal() )
            {
                transportObject = null;
            }
        }

        //前回の状態として保存
        if ( oldPullListAdd != SyncbPullListAdd)
        oldPullListAdd = SyncbPullListAdd;
        //ミッション中だったかどうかの判別のため。
        runTimeTransportOld = SyncbRunTimeTransport;


    }

    [Client]
    void pullPlayer()
    {
        //引っ張るオブジェクト無かったら入らない。操作出来るプレイヤーじゃなきゃ入らない(プレイヤーが動く処理)
        if (transportObject == null || !isLocalPlayer ) return;

        fDistancePlayer = Vector3.Distance(transform.position, transportObject.transform.position);
        //紐が伸び切ってる状態。
        if (fDistancePlayer >= fDistance)
        {
            //引く力＝紐にかかる力 + 紐の力
            float pullPower = (fDistancePlayer - fDistance) * playerSpringConstant;

            /////進行方向を求める
            //現在のキューブの方向へのベクトル
            vecForPlayer = transportObject.transform.position - transform.position;

            //プレイヤーへのベクトルを正規化
            vecForPlayer = vecForPlayer.normalized;

            //物体の進行方向へ力を加える
            GetComponent<Rigidbody>().AddForce(new Vector3(vecForPlayer.x * pullPower, vecForPlayer.y * pullPower, vecForPlayer.z * pullPower), ForceMode.Acceleration);
        }
        //紐がたるんでからの減速処理。（適当）
        else
        {
            //加速度を半分に
            GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x / 2, GetComponent<Rigidbody>().velocity.y / 2, GetComponent<Rigidbody>().velocity.z / 2);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        //オブジェクトに触った。オブジェクトを引っ張ってなかった。自分が操作可能プレイヤーである。
        if (collision.gameObject.tag == "transportObject" && transportObject == null && isLocalPlayer == true)
        {
            //サーバー側のオブジェクトにプレイヤーを追加して欲しいメッセージとプレイヤーにオブジェクトを紐付け
            CmdProvidebPullToServer(true , collision.gameObject);

            serverObjectController serverObjectControllerScript = collision.gameObject.GetComponent<serverObjectController>();

            ////オブジェクトを引っ張るステータス設定。
            fDistance = serverObjectControllerScript.GetfDistance();
            playerSpringConstant = serverObjectControllerScript.GetPlayerSpringConstant();
        }
    }

    //サーバー側のオブジェクトにリストを追加してほしいメッセージ
    //bool:オブジェクトのプレイヤーリストに追加要求
    //GameObjec:プレイヤーのtransportObjectに追加要求
    [Command]
    public void CmdProvidebPullToServer(bool bPull , GameObject transportObj)
    {
        transportObject = transportObj;
        SyncbPullListAdd = bPull;
    }

    //プレイヤーがトランスポートのミッション中かどうか伝えるため
    [Command]
    public void CmdProvidebRunTimeToServer(bool bRuntime)
    {
        SyncbRunTimeTransport = bRuntime;
    }

    [Server]
    public bool GetRuntime()
    {
        return SyncbRunTimeTransport;
    }

    //オブジェクトにプレイヤーを紐付けるために、オブジェクトの関数を呼ぶ処理
    [Server]
    void playerWithObject()
    {
        transportObject.GetComponent<serverObjectController>().PlayerWithObject(this.gameObject);
    }

    public GameObject GetTransportObject()
    {
        return transportObject;
    }
}