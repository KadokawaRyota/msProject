using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class arrivalAreaScript : MonoBehaviour {
    // Use this for initialization

    GameObject player;

	void Start () {
        player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider collider)
    {
        if (SceneManager.GetActiveScene().name == ("Offline"))
        {
            //オブジェクトに自分が紐付いていたら((プレイヤーを判別する要素が欲しい））
            if (collider.gameObject.tag == "transportObject")
            {
                collider.GetComponent<ObjectController>().Refresh();
                //加点する
            }
        }
        else if(SceneManager.GetActiveScene().name == ("Main"))
        {
            /*オブジェクトを綺麗に確実に他プレイヤーとの紐付けを切る手順
            1:サーバー側でオブジェクトの判定を取り、全プレイヤーにオブジェクトがゴールエリアに入った事を通知する。
            2：ゴールエリアに入ってるかどうかをローカルのプレイヤー側で判別後、プレイヤー側のオブジェクトとの紐付けを解除する。
            2.5：ここでプレイヤーに加点する。
            3：サーバー側でリストに入ってる全てのプレイヤーが紐付けを切った事を確認し、サーバー側のリストを解放する。
            4：サーバー側でエリアに対応した街に加点する。*/

            if (collider.gameObject.tag == "transportObject")
            {
                //サーバー側でゴールした事を繋がってるプレイヤーに通知する。
                collider.GetComponent<serverObjectController>().RpcInGoalArea(true);

                //オブジェクトを元の場所に戻す。オブジェクト側のプレイヤーを削除
                //collider.GetComponent<serverObjectController>().Refresh();

                //加点する
            }
        }
    }
}
