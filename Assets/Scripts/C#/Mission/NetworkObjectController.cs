using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkObjectController : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
    }

    //オブジェクトの表示
    //ネットワークで同期しているため、見えない間は位置同期のみ。よって表示と判定と物理演算をONにする。
    public void DispSwitch(bool bDisp)
    {
        GetComponent<MeshRenderer>().enabled = bDisp;
        GetComponent<BoxCollider>().enabled = bDisp;
        GetComponent<Rigidbody>().isKinematic = !bDisp;
    }

}