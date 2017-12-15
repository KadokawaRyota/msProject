using UnityEngine;
using UnityEngine.Networking;

public class PlayerSyncChat : NetworkBehaviour {

    [SerializeField]
    GameObject chatImage;

    public void PlayChat()
    {
        chatImage.GetComponent<Animator>().SetTrigger("open");

    }
}
