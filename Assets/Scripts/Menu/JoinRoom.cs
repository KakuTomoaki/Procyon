using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class JoinRoom : Photon.MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseUp()
    {
        PhotonNetwork.JoinRoom(transform.name);
    }
}
