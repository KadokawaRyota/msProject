using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class OnlineButton : NetworkBehaviour {

	public void Online()
	{
		SceneManager.LoadScene("Main");
	}
}
