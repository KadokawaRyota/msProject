using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatImage : MonoBehaviour
{

    void FixedUpdate()
    {

        //カメラポジションの取得
        Quaternion rot = Camera.main.transform.rotation;
        if (Camera.main != null)
        {
            gameObject.transform.rotation = rot;
        }
    }
}
