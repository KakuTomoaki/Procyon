using UnityEngine;
using System.Collections;

public class RoomButton : MonoBehaviour {

    private GameObject CanvasMenu;

    // Use this for initialization
    void Start () {
        CanvasMenu = GameObject.Find("CanvasMenu");
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom("user1");
    }

}
