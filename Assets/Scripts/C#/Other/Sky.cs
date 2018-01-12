using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sky : MonoBehaviour {

	//[SerializeField]
	float scrollSpeedX = 0.5f;

    [SerializeField]
    OfflineCameraStand standCam;

	void Start()
	{
		GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", Vector2.zero);
	}
	// Update is called once per frame
	void Update () {

		//float x =  Mathf.Repeat (Time.deltaTime * scrollSpeedX,1);

		//Vector2 offset = new Vector2(x, 0f);

		//カメラの回転角度を取得
		//Vector2 camRot = new Vector2(1 / standCam.gameObject.transform.rotation.y * Mathf.Rad2Deg,0.0f);
		//offset = camRot;

		//ShaderからOffsetの位置をずらす
		//GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", offset);
	}
}
