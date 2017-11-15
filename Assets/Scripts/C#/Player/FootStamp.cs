using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStamp : MonoBehaviour {

	[SerializeField] int stepCnt;
	int cnt = 0;

	void Update () {

		GetComponent<MeshRenderer> ().material.color = new Color (1, 1, 1, 1 - 1.0f * cnt / stepCnt);
		cnt++;

		if (GetComponent<MeshRenderer> ().material.color.a <= 0) {	
			Destroy (gameObject);
		}

	}
}
