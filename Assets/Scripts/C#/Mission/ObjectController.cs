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
/// <summary>
/// リアルな紐を演出するのはゲーム上ではバネから作っていく事になる。
/// 計算上どうしても伸び縮みする。
/// </summary>

public class ObjectController : MonoBehaviour
{

    //パブリック
    public GameObject player;
	
    //シリアライズ
    [SerializeField]
    float fDistance;        //紐が伸び切る距離
    [SerializeField]
    float objectSpringConstant;  //バネ定数
    [SerializeField]
    float playerSpringConstant;  //バネ定数・・・通常かかるバネ係数は同一だが、ゲーム的にプレイヤーの動きを良くするため。

    //List<GameObject> players = new List<GameObject>();


    //位置記憶用
    Vector3 pos;
    Quaternion rot;


    //プライベート
    float fDistancePlayer;                  //プレイヤーとの距離
    Vector3 vecForPlayer = Vector3.zero;    //プレイヤーへの向き

    // Use this for initialization
    void Start()
    {
        //位置記憶
        pos = transform.position;
        rot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;

        ////プレイヤーからキューブを引く処理
        fDistancePlayer = Vector3.Distance(player.transform.position, transform.position);
        //紐が伸び切ってる状態。
        if (fDistancePlayer >= fDistance)
        {
            //引く力＝紐にかかる力 + 紐の力
            float pullPower = (fDistancePlayer - fDistance) * objectSpringConstant;

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

        ////キューブからプレイヤーを引く処理
        fDistancePlayer = Vector3.Distance(transform.position, player.transform.position);
        //紐が伸び切ってる状態。
        if (fDistancePlayer >= fDistance)
        {
            //引く力＝紐にかかる力 + 紐の力
            float pullPower = (fDistancePlayer - fDistance) * playerSpringConstant;

            /////進行方向を求める
            //現在のキューブへのベクトル
            vecForPlayer = transform.position - player.transform.position;

            //プレイヤーへのベクトルを正規化
            vecForPlayer = vecForPlayer.normalized;

            //物体の進行方向へ力を加える
            player.GetComponent<Rigidbody>().AddForce(new Vector3(vecForPlayer.x * pullPower, vecForPlayer.y * pullPower, vecForPlayer.z * pullPower), ForceMode.Acceleration);
        }
        //紐がたるんでからの減速処理。（適当）
        else
        {
            //加速度を半分に
            player.GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x / 2, GetComponent<Rigidbody>().velocity.y / 2, GetComponent<Rigidbody>().velocity.z / 2);
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        //オブジェクトに触ったのが操作プレイヤーなら判別方法が適当なのはわからなかったから☆
        if( collision.gameObject.name == "OfflinePlayer_Tanuki" )
        {
            player = collision.gameObject;
        }
    }

    public void Refresh()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        transform.position = pos;
        transform.rotation = rot;
        player = null;
    }

    //オブジェクトの表示
    //ネットワークで同期しているため、見えない間は位置同期のみ。よって表示と判定と物理演算をONにする。
    public void DispSwitch( bool bDisp )
    {
        GetComponent<MeshRenderer>().enabled = bDisp;
        GetComponent<BoxCollider>().enabled = bDisp;
        GetComponent<Rigidbody>().isKinematic = !bDisp;
    }
}
