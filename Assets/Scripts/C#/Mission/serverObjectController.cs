using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

//紐に対して複数のプレイヤーが紐づく処理
//サーバーのみで引っ張る時用。

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

    //※未実装だが、こちらのスクリプトを使用する場合。プレイヤーが他のオブジェクトと紐付いていないかステート管理し。
    //プレイヤーが引っ張っている状態なら、オブジェクトと紐付かないようにしなければ、複数のオブジェクトを引っ張ってしまうので注意。


public class serverObjectController : NetworkBehaviour {


    //シリアライズ
    [SerializeField]
    float fDistance;            //紐が伸び切る距離
    [SerializeField]
    float objectSpringConstant;  //バネ定数
    [SerializeField]
    float playerSpringConstant;  //バネ定数・・・通常かかるバネ係数は同一だが、ゲーム的にプレイヤーの動きを良くするため。
    [SerializeField]
    int transportNum;           //最大の力で引っ張れる人数。

    [SerializeField]
    List<GameObject> players = new List<GameObject>();

	[SerializeField]
	GameObject fukidashi;
	bool fukidashiFlg = false;

	[SerializeField]
	Text numText;

	NetConnector netConnector;

	float length = 0;

    private const float RESPAWN_COUNT = 5.0f;   //リスポーンするまでの時間

    //位置記憶用
    Vector3 pos;
    Quaternion rot;


    //プライベート
    float fDistancePlayer;                  //プレイヤーとの距離
    Vector3 vecForPlayer = Vector3.zero;    //プレイヤーへの向き
    Vector3 vecForObject = Vector3.zero;    //オブジェクトへの向き

    [SyncVar]
    bool SyncbGoal = false;
    
    bool bGoal = false;

    public int Score;

	[SyncVar]
	int syncSetPlayerNum;

    // Use this for initialization
    void Start()
    {
        SyncbGoal = false;

        //__________デバッグ用にキネマティック解除
        GetComponent<Rigidbody>().isKinematic = false;

        //スポーンしたオブジェクトをMissionObject階層へ移動させる。
        GameObject parent = GameObject.Find("NetworkMissionManager/NetworkTransportation/MissionObject");
        transform.parent = parent.transform;

        if( transportNum < 1)
        {
            transportNum = 1;
        }

        if( isServer )
        {
            ServerStart();
        }

		//NetConnectorの取得
		GameObject netcon = GameObject.Find ("NetConnector");
		if (null != netcon) {
			netConnector = netcon.GetComponent<NetConnector> ();
		}
    }
    [Server]
    void ServerStart()
    {
        //位置記憶(運び終わったり、ミッションが終了した時にオブジェクトが戻る定位置)
        pos = transform.position;
        rot = transform.rotation;
    }

    void Update()
    {
        //アトリビュート[Server]はクライアント側でも呼び出されるため。アトリビュートを付ける意味はクライアントで呼び出された時にWarningを吐きだす
		if (isServer) {
			ServerUpdate ();
		} else {
			
			int num = 0;
			num = transportNum - syncSetPlayerNum;
			numText.text = num.ToString ();

			if(null != netConnector.GetLocalPlayer ())
			{
				Vector3 playerPos = netConnector.GetLocalPlayer ().gameObject.transform.localPosition;
				length = Vector3.Distance (playerPos, gameObject.transform.localPosition);

				if (length < fDistance * 3) {

					if (!fukidashi.gameObject.activeSelf)
						fukidashi.gameObject.SetActive (true);
				}

				if(length >= fDistance || num <= 0) {
					if (fukidashi.gameObject.activeSelf)
						fukidashi.gameObject.SetActive (false);
				}
			}
				
		}
    }


    [Server]
    void ServerUpdate()
    {
        float pullPower = 0.0f;

        //オブジェクトがゴールしていたら、プレイヤー側の接続が切れているか確認する
        if (bGoal)
        {
            foreach (GameObject player in players)
            {
                if (player.GetComponent<playerTransportationScript>().GetTransportObject() != null)
                {
                    return;
                }
            }
            //forのチェックを抜けたら、全てのプレイヤーが接続を切ったと言う事になるので、オブジェクト側のリストを全て解放し、加点して初期位置へ。
            //memo....急ぎで作ったため。要チェック
            GetComponent<serverObjectController>().SetGoal(false);
            Refresh();
        }

		RpcSetPlayerNum (players.Count);

        //リストに何も入ってない！
        if (players.Count <= 0) return;

        foreach (GameObject player in players)
        {
            //オブジェクトがゴールしていない時。
            if (!bGoal)
            {
                //プレイヤーが引っ張るオブジェクトとの繋がりを切っていた場合。
                if (player.GetComponent<playerTransportationScript>().GetTransportObject() == null)
                {
                    players.Remove(player);
                    continue;
                }
            }

            //現在の紐付いているプレイヤーからプレイヤーが引く力を調節
            int playerNum = players.Count / transportNum;
            if (playerNum > 1) playerNum = 1;

            ////紐の長さ
            fDistancePlayer = Vector3.Distance(player.transform.position, transform.position);
            //紐が伸び切ってる状態。
            if (fDistancePlayer >= fDistance && !(player.GetComponent<Rigidbody>().velocity.Equals(Vector3.zero)))
            {
                //引く力＝紐にかかる力 + 紐の力 * 運ぶプレイヤーの人数が少なかったら、足りない分、減らす。
                pullPower = (fDistancePlayer - fDistance) * objectSpringConstant * playerNum;

                /////進行方向を求める
                //現在のプレイヤーへのベクトル
                vecForPlayer = player.transform.position - transform.position;

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
    }

    [Server]
    public void PlayerWithObject(GameObject player )
    {
        Vector3 pos;
        pos = gameObject.transform.position;

        //プレイヤーがミッション中なら
        if (player.GetComponent<playerTransportationScript>().GetRuntime() )

        //同じプレイヤーがいないか検索。
            foreach (GameObject listPlayer in players)
        {
            if(listPlayer == player ) return;
        }
        //リストにプレイヤーを追加
        players.Add(player);
    }

    //運び終わったら発動させる
    //定位置に戻す処理。
    [Server]
    public void Refresh()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        transform.position = pos;
        transform.rotation = rot;

        //リストを空に
        players.Clear();
    }

    //オブジェクトの表示
    //ネットワークで同期しているため、見えない間は位置同期のみ。よって表示と判定と物理演算をONにする。
    public void DispSwitch(bool bDisp)
    {
        GetComponent<MeshRenderer>().enabled = bDisp;
        GetComponent<BoxCollider>().enabled = bDisp;
        GetComponent<Rigidbody>().isKinematic = !bDisp;
    }

    public float GetfDistance()
    {
        return fDistance;            //紐が伸び切る距離
    }

    public float GetPlayerSpringConstant()
    {
        return playerSpringConstant;
    }

    //値の書き換え
    public void SetGoal( bool bgoal )
    {
        bGoal = bgoal;
        //クライアントへ送信
        RpcInGoalArea(bGoal);
    }

    //値をクライアントに送信
    [ClientRpc]
    public void RpcInGoalArea(bool bgoal)
    {
        SyncbGoal = bgoal;
    }

    public bool GetbGoal()
    {
        return SyncbGoal;
    }


    private static float count = 0.0f;

    [ServerCallback]
    public bool GoalCounter(bool bCount)
    {
        if (!isServer) return false;

        if (!bCount)
        {
            count = 0;
        }
        else
        {
            count += Time.deltaTime;
            if( count > RESPAWN_COUNT)
            {
                count = 0;
                return true;
            }
        }

        return false;
    }

	[ClientRpc]
	void RpcSetPlayerNum(int num)
	{
		syncSetPlayerNum = num;
	}
}