using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CameraController : MonoBehaviour {

    public GameObject targetObj;
    private Vector3 targetPos;
    private Quaternion targetRot;
    public PostureController postureController;
    private Vector3 cameraDir;
    private Vector3 playerToCamera;
    static Vector3 localPos;

    void Start()
    {
        cameraDir = Vector3.Scale(transform.forward, new Vector3(1, 1, 1)).normalized;

        // 球面座標系中心点からプレイヤーまでの距離
        localPos = new Vector3(0.0f, 1.5f, -2.5f);
    }

    void Update()
    {
        // プレイヤー位置の取得
        targetPos = targetObj.transform.position;

        // プレイヤー回転量の取得
        targetRot = targetObj.transform.rotation;
        targetRot.y = 0.0f;

        // カメラの姿勢をプレイヤーと同期
        cameraDir = Vector3.ProjectOnPlane(cameraDir, postureController.GetsurfaceNormal);
        transform.rotation = Quaternion.LookRotation(cameraDir, postureController.GetsurfaceNormal);



        transform.position = targetObj.transform.position + transform.rotation * localPos;

        // マウスの右クリックを押している間
        if (Input.GetMouseButton(1))
        {
            // マウスの移動量
            float mouseInputX = Input.GetAxis("Mouse X");
            float mouseInputY = Input.GetAxis("Mouse Y");

            // targetの位置のY軸を中心に、回転（公転）する
            transform.RotateAround(targetPos, postureController.GetsurfaceNormal, mouseInputX * Time.deltaTime * 200f);
            cameraDir = Vector3.ProjectOnPlane(targetPos - transform.position, postureController.GetsurfaceNormal);

        }
    }
}
