using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	[SerializeField]
	int audioNum;

	AudioSource source;

	[SerializeField]
	AudioClip audioClip001,audioClip002,audioClip003;

	int randomBgm;

	// Use this for initialization
	void Start () {

		source = GetComponent<AudioSource> ();
		int audioNum = Random.Range (0, 2);
		switch (audioNum) {
		case 0:
			source.clip = audioClip001;
			break;

		case 1:
			source.clip = audioClip002;
			break;

		case 2:
			source.clip = audioClip003;
			break;
		}

		source.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

