using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loading : MonoBehaviour {

    [SerializeField]
    Text loadTimeText;

    [SerializeField]
    int addTime = 30;

    int cnt = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (cnt == addTime)
        {
            loadTimeText.text = ".";
        }

        else if (cnt == addTime * 2)
        {
            loadTimeText.text = "..";
        }

        else if (cnt == addTime * 3)
        {
            loadTimeText.text = "...";
        }

        else if(cnt == addTime * 4)
        {
            loadTimeText.text = "";
            cnt = 0;
            return;
        }

        cnt ++;
	}
}
