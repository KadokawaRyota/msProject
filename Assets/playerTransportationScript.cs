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


public class playerTransportationScript : MonoBehaviour
{
    //シリアライズ
    [SerializeField]
    float fDistance;        //紐が伸び切る距離
    [SerializeField]
    float objectSpringConstant;  //バネ定数
    [SerializeField]
    float playerSpringConstant;  //バネ定数・・・通常かかるバネ係数は同一だが、ゲーム的にプレイヤーの動きを良くするため。

    [SerializeField]
    GameObject transportObject = null; //プレイヤーが運ぶオブジェクト

    //位置記憶用
    Vector3 pos;
    Quaternion rot;


    //プライベート
    float fDistancePlayer;                  //プレイヤーとの距離
    Vector3 vecForPlayer = Vector3.zero;    //プレイヤーへの向き

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //引っ張るオブジェクト無かったら入らない。
        if (transportObject == null) return;

        ////プレイヤーからキューブを引く処理
        fDistancePlayer = Vector3.Distance(transform.position, transportObject.transform.position);
        //紐が伸び切ってる状態。
        if (fDistancePlayer >= fDistance)
        {
            //引く力＝紐にかかる力 + 紐の力
            float pullPower = (fDistancePlayer - fDistance) * objectSpringConstant;

            /////進行方向を求める
            //現在のプレイヤーへのベクトル
            vecForPlayer = transform.position - transportObject.transform.position;

            //プレイヤーへのベクトルを正規化
            vecForPlayer = vecForPlayer.normalized;

            //物体の進行方向へ力を加える
            transportObject.GetComponent<Rigidbody>().AddForce(new Vector3(vecForPlayer.x * pullPower, vecForPlayer.y * pullPower, vecForPlayer.z * pullPower), ForceMode.Acceleration);
        }
        //紐がたるんでからの減速処理。（適当）
        else
        {
            //加速度を半分に
            transportObject.GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x / 2, GetComponent<Rigidbody>().velocity.y / 2, GetComponent<Rigidbody>().velocity.z / 2);
        }

        ////キューブを引く時にかかるプレイヤーへの逆ベクトルの処理（通常は同じ力がかかるが、今回はプレイヤーの踏ん張りが無いのでキューブとは別に力を加える事が可能）
        fDistancePlayer = Vector3.Distance(transportObject.transform.position, transform.position);
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

    //プレイヤーが引っ張るオブジェクトを決める。
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "transportObject" && transportObject == null)
        {
            transportObject = collision.gameObject;
        }
    }
}