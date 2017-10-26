using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

public class ObjectController : MonoBehaviour {

    //パブリック

    //シリアライズ
    [SerializeField]
    float fDistance;        //紐が伸び切る距離
    [SerializeField]
    float playerPullPower;  //プレイヤーが引く力

    //プライベート
    GameObject player = null;

    float fDistancePlayer;  //プレイヤーとの距離
    Vector3 vecForPlayer = Vector3.zero;    //プレイヤーへの向き

    Vector3 playerOnSurfaceNormal;
    Vector3 playerMoveVec;
    Vector3 moveVec;
    float moveValue;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (player)            //紐で引っ張られるような処理
        {
            fDistancePlayer = Vector3.Distance(player.transform.position, transform.position);
            //紐が伸び切ってる状態。
            if (fDistancePlayer >= fDistance)
            {
                //紐にかかる力 + プレイヤーの引く力
                float pullPower = ( fDistancePlayer - fDistance ) * playerPullPower;
                //現在のプレイヤーの移動量受け取り
                //playerMoveVec = player.GetComponent<OfflinePostureController>().GetmoveVec();
                //プレイヤーのいる位置の地面の法線受け取り
                //playerOnSurfaceNormal = player.GetComponent<OfflinePostureController>().GetSurfaceNormal();

                /////進行方向を求める
                //現在のプレイヤーへのベクトル
                vecForPlayer = player.transform.position - transform.position;

                //プレイヤーへのベクトルを正規化
                vecForPlayer = vecForPlayer.normalized;

                //物体の進行方向
                GetComponent<Rigidbody>().AddForce( new Vector3(vecForPlayer.x * pullPower , vecForPlayer.y * pullPower, vecForPlayer.z * pullPower) , ForceMode.Acceleration );

                Debug.Log(vecForPlayer);
            }

            if( fDistancePlayer < fDistance )
            {
                GetComponent<Rigidbody>().velocity = new Vector3 ( GetComponent<Rigidbody>().velocity.x / 2 , GetComponent<Rigidbody>().velocity.y / 2 , GetComponent<Rigidbody>().velocity.z / 2);
            }

        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if(player == null)
        if (collision.gameObject.name == "OfflinePlayer")
        {
            //collision.transform.parent = transform;
            player = collision.gameObject;
        }
    }
}
