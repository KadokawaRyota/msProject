using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour {

    //パブリック

    //シリアライズ
    [SerializeField]
    float fDistance;

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
            //if (fDistancePlayer >= fDistance)
            //{
                //現在のプレイヤーの移動量受け取り
                playerMoveVec = player.GetComponent<OfflinePostureController>().GetmoveVec();
                //プレイヤーのいる位置の地面の法線受け取り
                playerOnSurfaceNormal = player.GetComponent<OfflinePostureController>().GetSurfaceNormal();

                /////進行方向を求める
                //現在のプレイヤーへのベクトル
                vecForPlayer = transform.position - player.transform.position;
                //プレイヤーへのベクトルを正規化
                vecForPlayer = vecForPlayer / vecForPlayer.magnitude;
                //プレイヤーの地面の法線とプレイヤーの向きを内積し、移動量を求める。
                moveValue = Vector3.Dot(vecForPlayer, playerOnSurfaceNormal);

                transform.position += new Vector3( vecForPlayer.x * moveValue , vecForPlayer.y * moveValue , vecForPlayer.z * moveValue );

                //適当なやつ
                //this.transform.position += player.gameObject.GetComponent<OfflinePostureController>().GetmoveVec();

                Debug.Log(moveValue);
            //}
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
