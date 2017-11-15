using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransportationScript : MonoBehaviour {

    [SerializeField]
    GameObject MissionObjects;
    [SerializeField]
    ParticleSystem particle;
    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
    }

    public void DispMissionObject()
    {
        //ミッションに関係あるオブジェクトを全て表示
        foreach (Transform child in MissionObjects.transform)
        {
            child.gameObject.GetComponent<ObjectController>().DispSwitch(true);
            particle.Play();
        }
    }
}
