using UnityEngine;
using System.Collections;

public class PlayerMotion : Photon.MonoBehaviour {

    private Animator animator;
    private float boostTime;
    public static bool boostFlag;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        boostTime = 0.5f;
	
	}
	
	// Update is called once per frame
	void Update () {

        //左右移動モーション
        if(Input.GetAxis("Horizontal") < 0) {
            animator.SetInteger("Horizontal", -1);
        } else if(Input.GetAxis("Horizontal") > 0) {
                animator.SetInteger("Horizontal", 1);
        } else {
            animator.SetInteger("Horizontal", 0);
        }
        //前後移動モーション
        if (Input.GetAxis("Vertical") < 0) {
            animator.SetInteger("Vertical", -1);
        } else if (Input.GetAxis("Vertical") > 0) {
            animator.SetInteger("Vertical", 1);
        } else {
            animator.SetInteger("Vertical", 0);
        }

        //ジャンプモーション
        animator.SetBool("Jump", Input.GetButton("Jump"));

        //ブーストモーション
        if(Input.GetButtonDown("Boost")) {
            animator.SetInteger("Boost", 1);
            boostFlag = true;
            Invoke("BoostTime", boostTime);
        }
        //animator.SetBool("Boost", Input.GetButton("Boost"));
    }

    //ブーストモーション関連
    void BoostTime() {
        animator.SetInteger("Boost", 0);
        boostFlag = false;
    }
}
