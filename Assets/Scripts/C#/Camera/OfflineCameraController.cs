using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfflineCameraController : MonoBehaviour
{

    public GameObject StandCameraObj;
    public float rotPitch = 0.0f;
    

	void Start()
	{
	}

	void Update()
	{

        // カメラ基準位置と角度の同期
        transform.position = StandCameraObj.transform.position;
        transform.rotation = StandCameraObj.transform.rotation;

        // カメラをX軸を中心に上下に向ける
        if (Input.GetKey(KeyCode.UpArrow))
        {
            rotPitch += Time.deltaTime * 0.10f;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            rotPitch -= Time.deltaTime * 0.10f;
        }

        // ピッチに合わせてカメラを回転させる
        this.transform.Rotate(Vector3.right * rotPitch);

    }
}
