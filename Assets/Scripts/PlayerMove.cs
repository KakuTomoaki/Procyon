using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour {

    public float speed = 15.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    private Vector3 moveDirection = Vector3.zero;

    int boostPoint;
    int boostPointMax = 1000;

    public Image gaugeImage;

    Vector3 moveSpeed;

    const float addNormalSpeed = 1;     //通常時の加速速度
    const float addBoostSpeed = 2;      //ブースト時の加速速度
    const float moveSpeedMax = 20;      //通常時の最大速度
    const float boostSpeedMax = 40;     //ブースト時の最大速度

    bool isBoost;

	// Use this for initialization
	void Start () {
        boostPoint = boostPointMax;

        moveSpeed = Vector3.zero;

        isBoost = false;
	
	}
	
	// Update is called once per frame
	void Update () {
        //プレイヤーを移動させる
        CharacterController controller = GetComponent<CharacterController>();
        /* コード実装中
        if(controller.isGrounded) {
            moveDirection.y = 0;
        }
        //ブーストボタンが押されていればフラグを立て、ブーストポイントを消費
        if(Input.GetButton("Boost") && boostPoint > 1) {
            boostPoint -= 150;
            isBoost = true; // ここに時間の経過処理が必要
        } else {
            isBoost = false;
        }
        */

        if(controller.isGrounded) {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;

            //ブーストキーによる高速移動
            if(Input.GetButton("Boost") && boostPoint > 1) {
                moveDirection.x *= 10;
                moveDirection.z *= 10;
                boostPoint -= 150;
            }

        }
        if (Input.GetButton("Jump") && boostPoint > 1) {
            if (transform.position.y > 100) {
                moveDirection.y = 0;
            } else {
                moveDirection.y += gravity * Time.deltaTime;
                boostPoint -= 20;
            }
        } else {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        if(!Input.GetButton("Boost") && !Input.GetButton("Jump")) {
            boostPoint += 40;
            boostPoint = Mathf.Clamp(boostPoint, 0, boostPointMax);
        }

        controller.Move(moveDirection * Time.deltaTime);

        //ブーストゲージの伸縮
        gaugeImage.transform.localScale = new Vector3((float)boostPoint / boostPointMax, 1, 1);
	
	}
}
