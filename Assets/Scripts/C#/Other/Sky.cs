using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sky : MonoBehaviour {

	[SerializeField]
	float scrollSpeedX = 0.1f;

	void Start()
	{
		GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", Vector2.zero);
	}
	// Update is called once per frame
	void Update () {

		float x = Time.deltaTime * scrollSpeedX;

		Vector2 offset = new Vector2(x, 0f);

		//カメラの回転角度を取得
		Vector2 camRot = new Vector2(Camera.main.transform.localRotation.y,0.0f);
		offset = offset + camRot;

		//ShaderからOffsetの位置をずらす
		GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", offset);
	}
}
