using UnityEngine;
using System.Collections;

public class CameraRotate2 : MonoBehaviour {
    
    public float RotationSensitivity = 100f;// 感度
    GameObject target_P;
                                            // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update() {

        GameObject CameraParent = Camera.main.transform.parent.gameObject;
        target_P = GameObject.Find("azalea");

        var rotX = Input.GetAxis("Mouse X") * Time.deltaTime * RotationSensitivity;
        var rotY = Input.GetAxis("Mouse Y") * Time.deltaTime * RotationSensitivity;
        
        
        //PlayerRotateでX軸を回転してるので↓はコメントアウト
        //CameraParent.transform.Rotate(Vector3.up, rotX, 0);
        CameraParent.transform.Rotate(rotY, 0, 0);

        
        // カメラがプレイヤーの真上や真下にあるときにそれ以上回転させないようにする
        if (CameraParent.transform.forward.y > 0.8f && rotY < 0) {
            rotY = 0;
        }
        if (CameraParent.transform.forward.y < -0.8f && rotY > 0) {
            rotY = 0;
        }
        
    }        
}
