using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfflineCameraStand : MonoBehaviour {

    public OfflinePostureController offlinePostureController;

    public GameObject targetObj;                        // プレイヤー情報
    public Vector3 localPos;                            // カメラローカル位置
    public float mouseInputX;
    public float mouseInputY;
    private Vector2 inputStart;
    private Vector2 inputEnd;
    private float inputLength;

    private Vector3 targetPos;                          // プレイヤー位置情報
    private Quaternion targetRot;                       // プレイヤー角度情報
    private Vector3 cameraDir;                          // カメラの進行方向射影ベクトル
    private Vector3 playerToCamera;                     // プレイヤーからカメラへのベクトル
    private Quaternion rotCamera;                       // カメラの傾き情報

    public Vector3 GetCameraDirection
    {
        get { return this.cameraDir; }
    }

    Scr_ControllerManager controllerManager;    //コントローラのマネージャ


    void Start () {
        cameraDir = Vector3.Scale(transform.forward, new Vector3(1, 1, 1)).normalized;

        //コントロールマネージャの取得
        if (GameObject.Find("PuniconCamera/ControllerManager") != null)
        {
            controllerManager = GameObject.Find("PuniconCamera/ControllerManager").GetComponent<Scr_ControllerManager>();
        }
    }
	
	void Update () {

        // 2点タッチ入力の取得
        inputStart = new Vector2(controllerManager.CameraMoveLength_Start.x, controllerManager.CameraMoveLength_Start.y);
        inputEnd = new Vector2(controllerManager.CameraMoveLength_End.x, controllerManager.CameraMoveLength_End.y);
        inputLength = controllerManager.fCameraMoveLength;

        //inputLength /= 1080.0f;
        //inputLength = 0.10f;

        // プレイヤー位置の取得
        targetPos = targetObj.transform.position;

        // プレイヤー回転量の取得
        targetRot = targetObj.transform.rotation;
        targetRot.y = 0.0f;

        // カメラの姿勢をプレイヤーと同期
        cameraDir = Vector3.ProjectOnPlane(cameraDir, offlinePostureController.GetsurfaceNormal);
        //transform.rotation = Quaternion.LookRotation(cameraDir, postureController.GetsurfaceNormal);
        rotCamera = Quaternion.LookRotation(cameraDir, offlinePostureController.GetsurfaceNormal);

        // 射影ベクトルで求めたカメラの傾きを退避
        Quaternion rotCameraEvac = rotCamera;

        // カメラのピッチ量に合わせて傾きを変更
        //rotCamera.x += rotPitch;
        transform.rotation = rotCamera;

        // カメラ位置の調整
        transform.position = targetObj.transform.position + rotCameraEvac * localPos;
        //transform.position = targetObj.transform.position + rotCamera * localPos;

        // マウスの右クリックを押している間
        if (Input.GetMouseButton(1))
        {
            // マウスの移動量
            mouseInputX = Input.GetAxis("Mouse X");
            mouseInputY = Input.GetAxis("Mouse Y");

            // targetの位置のY軸を中心に、回転（公転）する
            // playerToCamera = transform.position - targetPos;
            // Vector3 proj;                                      // playerToCameraの法線に対する射影ベクトル

            // proj = Vector3.Project(playerToCamera, postureController.GetsurfaceNormal);
            // proj += targetPos;

            //transform.RotateAround(targetPos, postureController.GetsurfaceNormal, mouseInputX * Time.deltaTime * 200f);
            transform.RotateAround(targetPos, offlinePostureController.GetsurfaceNormal, mouseInputX * Time.deltaTime * 200f);
            cameraDir = Vector3.ProjectOnPlane(targetPos - transform.position, offlinePostureController.GetsurfaceNormal);


        }

        if(inputLength > 0.0f)
        {
            transform.RotateAround(targetPos, offlinePostureController.GetsurfaceNormal, inputLength * Time.deltaTime * 200f);
            cameraDir = Vector3.ProjectOnPlane(targetPos - transform.position, offlinePostureController.GetsurfaceNormal);
        }

    }
}
