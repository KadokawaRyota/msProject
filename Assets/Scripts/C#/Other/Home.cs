using UnityEngine;
using UnityEngine.UI;

public class Home : MonoBehaviour {

    [SerializeField]
    Text playerName;

    void Start()
    {
        GameObject info = GameObject.Find("CharactorInfo");

        if(null != info)
        {
            playerName.text = info.GetComponent<CharactorInfo>().GetPlayerName();
        }
    }

    public void SetName(string Name)
    {
        playerName.text = Name;
    }
}
