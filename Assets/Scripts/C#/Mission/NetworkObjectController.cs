using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkObjectController : NetworkBehaviour
{
    [SerializeField]
    GameObject stencil;

    // Use this for initialization
    void Start()
    {
    }

    //オブジェクトの表示
    //ネットワークで同期しているため、見えない間は位置同期のみ。よって表示と判定と物理演算をONにする。
    public void DispSwitch(bool bDisp)
    {
        GetComponent<MeshRenderer>().enabled = bDisp;

        if (GetComponent<BoxCollider>() != null)
        {
            GetComponent<BoxCollider>().enabled = bDisp;
        }
        else if (GetComponent<SphereCollider>() != null)
        {
            GetComponent<SphereCollider>().enabled = bDisp;
        }

        GetComponent<Rigidbody>().isKinematic = !bDisp;

        stencil.SetActive(true);
    }

}