using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManagerScript : MonoBehaviour {

    bool m_missionFlg = false;

    [SerializeField]
    GameObject MissionType;

	// Use this for initialization
	void Start () {
        m_missionFlg = false;
    }
	
	// Update is called once per frame
	void Update () {
	}

    public void ExecMission()
    {
        if( MissionType.gameObject.name == "Transportation")
        MissionType.GetComponent<TransportationScript>().DispMissionObject();
    }
}
