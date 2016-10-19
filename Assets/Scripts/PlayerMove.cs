using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour {

    public float speed = 15.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    private Vector3 moveDirection = Vector3.zero;

    int boostPoint;
    public int boostPointMax = 3000;

    public Image gaugeImage;

    Vector3 moveSpeed;

    const float addNormalSpeed = 1;     //通常時の加速速度
    const float addBoostSpeed = 2;      //ブースト時の加速速度
    const float moveSpeedMax = 20;      //通常時の最大速度
    const float boostSpeedMax = 40;     //ブースト時の最大速度

    private float Boost_CD = 0;
    bool isBoost;
    bool isBoost_CD;

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
        
        if(controller.isGrounded) {
            moveDirection.y = 0;
        }
        //ブーストボタンが押されていればフラグを立て、ブーストポイントを消費
        if(Input.GetButton("Boost") && boostPoint > 20 && isBoost_CD == false) {
            isBoost = true;
            //isBoost_CD = true;
            //Boost_CD += Time.deltaTime;
            //PlayerHp.armorPoint -= 10;
            boostPoint -= 20;
            //moveDirection.x *= 20;
            //moveDirection.z *= 1000;
            Debug.Log("ブースト開始");
        } else {
            isBoost = false;
            /*
            if (Boost_CD > 1.0f) {
                Debug.Log("ブーストクールダウン開始");
                Boost_CD = 0;
                isBoost_CD = false;
                Debug.Log("ブーストクールダウン終了+フラグオフ");
            }
            */
        }

        Vector3 targetSpeed = Vector3.zero;     //目標速度
        Vector3 addSpeed = Vector3.zero;        //加速速度

        //左右移動時の目標速度と加速速度
        if(Input.GetAxis("Horizontal") == 0) {

            //推していないときは目標速度を0にする
            targetSpeed.x = 0;

            //設置しているときと空中にいる時は減速値を変える
            if(controller.isGrounded) {
                addSpeed.x = addNormalSpeed;
            } else {
                addSpeed.x = addNormalSpeed / 4;
            }
        } else {
            //通常時とブースト時で変化
            if(isBoost) {
                targetSpeed.x = boostSpeedMax;
                addSpeed.x = addBoostSpeed;
            } else {
                targetSpeed.x = moveSpeedMax;
                addSpeed.x = addNormalSpeed;
            }

            targetSpeed.x *= Mathf.Sign(Input.GetAxis("Horizontal"));
        }
        //左右移動の速度
        moveSpeed.x = Mathf.MoveTowards(moveSpeed.x, targetSpeed.x, addSpeed.x);
        moveDirection.x = moveSpeed.x;

        //前後移動の目標速度と加速速度
        if(Input.GetAxis("Vertical") == 0) {
            targetSpeed.z = 0;

            if(controller.isGrounded) {
                addSpeed.z = addNormalSpeed;
            } else {
                addSpeed.z = addNormalSpeed / 4;
            }
        } else {
            if(isBoost) {
                targetSpeed.z = boostSpeedMax;
                addSpeed.z = addBoostSpeed;
            } else {
                targetSpeed.z = moveSpeedMax;
                addSpeed.z = addNormalSpeed;
            }
            targetSpeed.z *= Mathf.Sign(Input.GetAxis("Vertical"));
        }

        //水平移動の速度
        moveSpeed.z = Mathf.MoveTowards(moveSpeed.z, targetSpeed.z, addSpeed.z);
        moveDirection.z = moveSpeed.z;

        moveDirection = transform.TransformDirection(moveDirection);
        

        /*
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
        */

        if (Input.GetButton("Jump") && boostPoint > 1) {
            if (transform.position.y > 100) {
                moveDirection.y = 0;
            } else {
                moveDirection.y += gravity * Time.deltaTime;
                boostPoint -= 10;
            }
        } else {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        if(!Input.GetButton("Boost") && !Input.GetButton("Jump")) {
            boostPoint += 20;
            boostPoint = Mathf.Clamp(boostPoint, 0, boostPointMax);
        }

        controller.Move(moveDirection * Time.deltaTime);

        //ブーストゲージの伸縮
        gaugeImage.transform.localScale = new Vector3((float)boostPoint / boostPointMax, 1, 1);
	
	}
}
