using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerName : MonoBehaviour {

    [SerializeField]
    Text nameText;      //NameTextUI

	// Use this for initialization
	void Start () {

        CharactorInfo charaInfo = GameObject.Find("CharactorInfo").GetComponent<CharactorInfo>();

        //名前設定
        if (null != charaInfo)
        {
            nameText.text = charaInfo.GetPlayerName();
        }
        else
        {
            nameText.text = "None";
        }
		
	}
	
	// Update is called once per frame
	void Update () {
		if(null != Camera.main)
       	 transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
    }

    public void SetNameText(string name)
    {
        nameText.text = name;
    }
}
