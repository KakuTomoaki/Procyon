using UnityEngine;
using System.Collections;

public class BoostEffect : Photon.MonoBehaviour {

    GameObject boostLight;

    // Use this for initialization
    void Start () {
        boostLight = GameObject.Find("BoostLight");
        boostLight.SetActive(false);

    }
	
	// Update is called once per frame
	void Update () {
        bool flgBoost = false;

        //ブーストorジャンプ
        if(PlayerMotion.boostFlag == true || Input.GetButton("Jump")) {
            flgBoost = true;
        }
        boostLight.SetActive(flgBoost);
    }
}
