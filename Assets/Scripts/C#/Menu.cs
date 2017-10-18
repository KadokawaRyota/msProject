using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Menu : MonoBehaviour {

	[SerializeField]
	MenuButton button;

	float startPos;     //開始時の位置を記憶

	public float moveSpeed;		//開閉のスピード

	void Start () {
		button = GetComponent<MenuButton>();
		startPos = gameObject.transform.localPosition.x;
    }
	
	// Update is called once per frame
	void Update () {

		if(button.menuOpen)
		{
			if(gameObject.transform.localPosition.x < Screen.width * 0.4f)
			{
				gameObject.transform.localPosition += new Vector3(moveSpeed, 0.0f,0.0f);
			}
		}
		else
		{
			if (gameObject.transform.localPosition.x > startPos)
			{
				gameObject.transform.localPosition -= new Vector3(moveSpeed, 0.0f, 0.0f);
			}
		}

	}

}
