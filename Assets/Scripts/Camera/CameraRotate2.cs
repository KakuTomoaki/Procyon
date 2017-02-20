using UnityEngine;
using System.Collections;

public class CameraRotate2 : Photon.MonoBehaviour {
    
    public float RotationSensitivity = 100f;    // 感度
                                            
    // Use this for initialization
    void Start () {
    }

    // Update is called once per frame
    void Update() {
        
        GameObject CameraParent = Camera.main.transform.parent.gameObject;

        var rotX = Input.GetAxis("Mouse X") * Time.deltaTime * RotationSensitivity;
        var rotY = Input.GetAxis("Mouse Y") * Time.deltaTime * RotationSensitivity;
        
        
        //PlayerRotateでX軸を回転してるので↓はコメントアウト
        //CameraParent.transform.Rotate(Vector3.up, rotX, 0);
        CameraParent.transform.Rotate(rotY, 0, 0);

        
        
    }        
}
