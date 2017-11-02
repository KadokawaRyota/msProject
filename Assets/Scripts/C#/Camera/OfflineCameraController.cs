using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfflineCameraController : MonoBehaviour
{
	public GameObject targetObj;                        // プレイヤー情報
    public OfflinePostureController postureController;  // プレイヤー姿勢の
    public Vector3 localPos;                            // カメラローカル位置

    private Vector3 targetPos;                          // プレイヤー位置情報
	private Quaternion targetRot;                       // プレイヤー角度情報
	private Vector3 cameraDir;                          // カメラの進行方向射影ベクトル
	private Vector3 playerToCamera;                     // プレイヤーからカメラへのベクトル
    private Quaternion rotCamera;                       // カメラの傾き情報
    public float rotPitch;                              // カメラのピッチ量

    public Vector3 GetCameraDirection
    {
        get { return this.cameraDir; }
    }

	void Start()
	{
		cameraDir = Vector3.Scale(transform.forward, new Vector3(1, 1, 1)).normalized;
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
        //transform.rotation = Quaternion.LookRotation(cameraDir, postureController.GetsurfaceNormal);
        rotCamera = Quaternion.LookRotation(cameraDir, postureController.GetsurfaceNormal);

        // 射影ベクトルで求めたカメラの傾きを退避
        Quaternion rotCameraEvac = rotCamera;

        // カメラのピッチ量に合わせて傾きを変更
        rotCamera.x += rotPitch;
        transform.rotation = rotCamera;

        // カメラ位置の調整
        transform.position = targetObj.transform.position + rotCameraEvac * localPos;
        //transform.position = targetObj.transform.position + rotCamera * localPos;

        // カメラをX軸を中心に上下に向ける
        if (Input.GetKey(KeyCode.UpArrow))
        {
            rotPitch += Time.deltaTime * 0.10f;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            rotPitch -= Time.deltaTime * 0.10f;
        }

        // マウスの右クリックを押している間
        if (Input.GetMouseButton(1))
		{
			// マウスの移動量
			float mouseInputX = Input.GetAxis("Mouse X");
			float mouseInputY = Input.GetAxis("Mouse Y");

            // targetの位置のY軸を中心に、回転（公転）する
            // playerToCamera = transform.position - targetPos;
            // Vector3 proj;                                      // playerToCameraの法線に対する射影ベクトル

            // proj = Vector3.Project(playerToCamera, postureController.GetsurfaceNormal);
            // proj += targetPos;

            //transform.RotateAround(targetPos, postureController.GetsurfaceNormal, mouseInputX * Time.deltaTime * 200f);
            transform.RotateAround(targetPos, postureController.GetsurfaceNormal, mouseInputX * Time.deltaTime * 200f);
            cameraDir = Vector3.ProjectOnPlane(targetPos - transform.position, postureController.GetsurfaceNormal);

            if (rotPitch > 1.0f)
            {
                rotPitch = 1.0f;
            }
            else if (rotPitch < -1.0f)
            {
                rotPitch = -1.0f;
            }

        }
	}
}
